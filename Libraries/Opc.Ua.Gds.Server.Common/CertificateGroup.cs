/* ========================================================================
 * Copyright (c) 2005-2020 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua.Security;
using Opc.Ua.Security.Certificates;

namespace Opc.Ua.Gds.Server
{
    public class CertificateGroup : ICertificateGroup
    {
        #region Public Fields
        public NodeId Id { get; set; }
        public NodeId CertificateType { get; set; }
        public CertificateGroupConfiguration Configuration { get; }
        public X509Certificate2 Certificate { get; set; }
        public TrustListState DefaultTrustList { get; set; }
        public bool UpdateRequired { get; set; }
        public CertificateStoreIdentifier AuthoritiesStore { get; }
        public CertificateStoreIdentifier IssuerCertificatesStore { get; }
        #endregion

        public CertificateGroup()
        {
            UpdateRequired = false;
        }

        protected CertificateGroup(
            string authoritiesStorePath,
            CertificateGroupConfiguration certificateGroupConfiguration,
            [Optional] string trustedIssuerCertificatesStorePath
            )
        {
            AuthoritiesStore = new CertificateStoreIdentifier(authoritiesStorePath, false);
            Configuration = certificateGroupConfiguration;
            if (trustedIssuerCertificatesStorePath != null)
            {
                IssuerCertificatesStore = new CertificateStoreIdentifier(trustedIssuerCertificatesStorePath);
            }
            SubjectName = Configuration.SubjectName.Replace("localhost", Utils.GetHostName());
        }

        #region ICertificateGroupProvider
        public virtual async Task Init()
        {
            Utils.LogInfo("InitializeCertificateGroup: {0}", SubjectName);

            ICertificateStore store = AuthoritiesStore.OpenStore();
            try
            {
                X509Certificate2Collection certificates = await store.Enumerate().ConfigureAwait(false);
                foreach (var certificate in certificates)
                {
                    if (X509Utils.CompareDistinguishedName(certificate.Subject, SubjectName))
                    {
                        if (X509Utils.GetRSAPublicKeySize(certificate) != Configuration.CACertificateKeySize)
                        {
                            continue;
                        }
                        // TODO check hash size

                        if (Certificate != null)
                        {
                            // always use latest issued cert in store
                            if (certificate.NotBefore > DateTime.UtcNow ||
                                Certificate.NotBefore > certificate.NotBefore)
                            {
                                continue;
                            }
                        }
                        Certificate = certificate;
                    }
                }
            }
            finally
            {
                store?.Close();
            }

            if (Certificate == null)
            {
                Utils.LogInfo(Utils.TraceMasks.Security,
                    "Create new CA Certificate: {0}, KeySize: {1}, HashSize: {2}, LifeTime: {3} months",
                    SubjectName,
                    Configuration.CACertificateKeySize,
                    Configuration.CACertificateHashSize,
                    Configuration.CACertificateLifetime
                    );
                X509Certificate2 newCertificate = await CreateCACertificateAsync(SubjectName).ConfigureAwait(false);
                Certificate = X509CertificateLoader.LoadCertificate(newCertificate.RawData);
                Utils.LogCertificate(Utils.TraceMasks.Security, "Created CA certificate: ", Certificate);
            }
        }

        public virtual ICertificateGroup Create(
            string authoritiesStorePath,
            CertificateGroupConfiguration certificateGroupConfiguration,
            [Optional] string trustedIssuerCertificatesStorePath)
        {
            return new CertificateGroup(authoritiesStorePath, certificateGroupConfiguration, trustedIssuerCertificatesStorePath);
        }

        /// <summary>
        /// Create a certificate with a new key pair signed by the CA of the cert group.
        /// </summary>
        /// <param name="application">The application record.</param>
        /// <param name="subjectName">The subject of the certificate.</param>
        /// <param name="domainNames">The domain names for the subject alt name extension.</param>
        /// <param name="privateKeyFormat">The private key format as PFX or PEM.</param>
        /// <param name="privateKeyPassword">A password for the private key.</param>
        public virtual async Task<X509Certificate2KeyPair> NewKeyPairRequestAsync(
            ApplicationRecordDataType application,
            string subjectName,
            string[] domainNames,
            string privateKeyFormat,
            string privateKeyPassword)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            if (application.ApplicationUri == null) throw new ArgumentNullException(nameof(application.ApplicationUri));
            if (application.ApplicationNames == null) throw new ArgumentNullException(nameof(application.ApplicationNames));

            using (var signingKey = await LoadSigningKeyAsync(Certificate, null).ConfigureAwait(false))
            using (var certificate = CertificateFactory.CreateCertificate(
                application.ApplicationUri,
                application.ApplicationNames.Count > 0 ? application.ApplicationNames[0].Text : "ApplicationName",
                subjectName,
                domainNames)
                .SetIssuer(signingKey)
                .CreateForRSA())
            {
                byte[] privateKey;
                if (privateKeyFormat == "PFX")
                {
                    privateKey = certificate.Export(X509ContentType.Pfx, privateKeyPassword);
                }
                else if (privateKeyFormat == "PEM")
                {
                    privateKey = PEMWriter.ExportPrivateKeyAsPEM(certificate, privateKeyPassword);
                }
                else
                {
                    throw new ServiceResultException(StatusCodes.BadInvalidArgument, "Invalid private key format");
                }
                return new X509Certificate2KeyPair(X509CertificateLoader.LoadCertificate(certificate.RawData), privateKeyFormat, privateKey);
            }
        }

        public virtual async Task<X509CRL> RevokeCertificateAsync(
            X509Certificate2 certificate)
        {
            X509CRL crl = await RevokeCertificateAsync(
                AuthoritiesStore,
                certificate,
                null).ConfigureAwait(false);

            // Also update TrustedList CRL so registerd Applications can get the new CRL
            if (crl != null)
            {
                var certificateStoreIdentifier = new CertificateStoreIdentifier(Configuration.TrustedListPath);
                await UpdateAuthorityCertInCertificateStore(certificateStoreIdentifier).ConfigureAwait(false);

                //Also update TrustedIssuerCertificates Store
                if (IssuerCertificatesStore != null)
                {
                    await UpdateAuthorityCertInCertificateStore(IssuerCertificatesStore).ConfigureAwait(false);
                }
            }

            // return crl
            return crl;
        }

        public virtual Task VerifySigningRequestAsync(
            ApplicationRecordDataType application,
            byte[] certificateRequest)
        {
            try
            {
                var pkcs10CertificationRequest = new Org.BouncyCastle.Pkcs.Pkcs10CertificationRequest(certificateRequest);

                if (!pkcs10CertificationRequest.Verify())
                {
                    throw new ServiceResultException(StatusCodes.BadInvalidArgument, "CSR signature invalid.");
                }

                var info = pkcs10CertificationRequest.GetCertificationRequestInfo();
                var altNameExtension = GetAltNameExtensionFromCSRInfo(info);
                if (altNameExtension != null)
                {
                    if (altNameExtension.Uris.Count > 0)
                    {
                        if (!altNameExtension.Uris.Contains(application.ApplicationUri))
                        {
                            throw new ServiceResultException(StatusCodes.BadCertificateUriInvalid,
                                "CSR AltNameExtension does not match " + application.ApplicationUri);
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                if (ex is ServiceResultException)
                {
                    throw;
                }
                throw new ServiceResultException(StatusCodes.BadInvalidArgument, ex.Message);
            }
        }


        public virtual async Task<X509Certificate2> SigningRequestAsync(
            ApplicationRecordDataType application,
            string[] domainNames,
            byte[] certificateRequest)
        {
            try
            {
                var pkcs10CertificationRequest = new Org.BouncyCastle.Pkcs.Pkcs10CertificationRequest(certificateRequest);

                if (!pkcs10CertificationRequest.Verify())
                {
                    throw new ServiceResultException(StatusCodes.BadInvalidArgument, "CSR signature invalid.");
                }

                var info = pkcs10CertificationRequest.GetCertificationRequestInfo();
                var altNameExtension = GetAltNameExtensionFromCSRInfo(info);
                if (altNameExtension != null)
                {
                    if (altNameExtension.Uris.Count > 0)
                    {
                        if (!altNameExtension.Uris.Contains(application.ApplicationUri))
                        {
                            var applicationUriMissing = new StringBuilder();
                            applicationUriMissing.AppendLine("Expected AltNameExtension (ApplicationUri):");
                            applicationUriMissing.AppendLine(application.ApplicationUri);
                            applicationUriMissing.AppendLine("CSR AltNameExtensions found:");
                            foreach (string uri in altNameExtension.Uris)
                            {
                                applicationUriMissing.AppendLine(uri);
                            }
                            throw new ServiceResultException(StatusCodes.BadCertificateUriInvalid,
                                applicationUriMissing.ToString());
                        }
                    }

                    if (altNameExtension.IPAddresses.Count > 0 || altNameExtension.DomainNames.Count > 0)
                    {
                        var domainNameList = new List<string>();
                        domainNameList.AddRange(altNameExtension.DomainNames);
                        domainNameList.AddRange(altNameExtension.IPAddresses);
                        domainNames = domainNameList.ToArray();
                    }
                }

                DateTime yesterday = DateTime.Today.AddDays(-1);
                using (var signingKey = await LoadSigningKeyAsync(Certificate, null).ConfigureAwait(false))
                {
                    X500DistinguishedName subjectName = new X500DistinguishedName(info.Subject.GetEncoded());
                    return CertificateBuilder.Create(subjectName)
                        .AddExtension(new X509SubjectAltNameExtension(application.ApplicationUri, domainNames))
                        .SetNotBefore(yesterday)
                        .SetLifeTime(Configuration.DefaultCertificateLifetime)
                        .SetHashAlgorithm(X509Utils.GetRSAHashAlgorithmName(Configuration.DefaultCertificateHashSize))
                        .SetIssuer(signingKey)
                        .SetRSAPublicKey(info.SubjectPublicKeyInfo.GetEncoded())
                        .CreateForRSA();
                }
            }
            catch (Exception ex)
            {
                if (ex is ServiceResultException)
                {
                    throw;
                }
                throw new ServiceResultException(StatusCodes.BadInvalidArgument, ex.Message);
            }

        }

        public virtual async Task<X509Certificate2> CreateCACertificateAsync(
            string subjectName
            )
        {
            // validate new subjectName matches the previous subject
            // TODO: An issuer may modify the subject of the CA certificate,
            // but then the configuration must be updated too!
            // NOTE: not a strict requirement here for ASN.1 byte compare
            if (!X509Utils.CompareDistinguishedName(subjectName, SubjectName))
            {
                throw new ArgumentException("SubjectName provided does not match the SubjectName property of the CertificateGroup \n" +
                    "CA Certificate is not created until the subjectName " + SubjectName + " is provided", subjectName);
            }

            DateTime yesterday = DateTime.Today.AddDays(-1);
            using (X509Certificate2 newCertificate = await CertificateFactory.CreateCertificate(subjectName)
                .SetNotBefore(yesterday)
                .SetLifeTime(Configuration.CACertificateLifetime)
                .SetHashAlgorithm(X509Utils.GetRSAHashAlgorithmName(Configuration.CACertificateHashSize))
                .SetCAConstraint()
                .SetRSAKeySize(Configuration.CACertificateKeySize)
                .CreateForRSA()
                .AddToStoreAsync(AuthoritiesStore).ConfigureAwait(false))
            {

                // save only public key
                Certificate = X509CertificateLoader.LoadCertificate(newCertificate.RawData);

                // initialize revocation list
                X509CRL crl = await RevokeCertificateAsync(AuthoritiesStore, newCertificate, null).ConfigureAwait(false);

                //Update TrustedList Store
                if (crl != null)
                {
                    // TODO: make CA trust selectable
                    var certificateStoreIdentifier = new CertificateStoreIdentifier(Configuration.TrustedListPath);
                    await UpdateAuthorityCertInCertificateStore(certificateStoreIdentifier).ConfigureAwait(false);

                    // Update TrustedIssuerCertificates Store
                    if (IssuerCertificatesStore != null)
                    {
                        await UpdateAuthorityCertInCertificateStore(IssuerCertificatesStore).ConfigureAwait(false);
                    }
                }
                return Certificate;
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// load the authority signing key.
        /// </summary>
        public virtual async Task<X509Certificate2> LoadSigningKeyAsync(X509Certificate2 signingCertificate, char[] signingKeyPassword)
        {
            CertificateIdentifier certIdentifier = new CertificateIdentifier(signingCertificate) {
                StorePath = AuthoritiesStore.StorePath,
                StoreType = AuthoritiesStore.StoreType
            };
            return await certIdentifier.LoadPrivateKey(signingKeyPassword).ConfigureAwait(false);
        }

        /// <summary>
        /// Revoke the CA signed certificate. 
        /// The issuer CA public key, the private key and the crl reside in the storepath.
        /// The CRL number is increased by one and existing CRL for the issuer are deleted from the store.
        /// </summary>
        public static async Task<X509CRL> RevokeCertificateAsync(
            CertificateStoreIdentifier storeIdentifier,
            X509Certificate2 certificate,
            char[] issuerKeyFilePassword = null
            )
        {
            X509CRL updatedCRL = null;
            X500DistinguishedName subjectName = certificate.IssuerName;
            string keyId = null;
            string serialNumber = null;

            // caller may want to create empty CRL using the CA cert itself
            bool isCACert = X509Utils.IsCertificateAuthority(certificate);

            // find the authority key identifier.
            var authority = X509Extensions.FindExtension<Ua.Security.Certificates.X509AuthorityKeyIdentifierExtension>(certificate);
            if (authority != null)
            {
                keyId = authority.KeyIdentifier;
                serialNumber = authority.SerialNumber;
            }
            else
            {
                throw new ArgumentException("Certificate does not contain an Authority Key");
            }

            if (!isCACert)
            {
                if (serialNumber == certificate.SerialNumber ||
                    X509Utils.IsSelfSigned(certificate))
                {
                    throw new ServiceResultException(StatusCodes.BadCertificateInvalid, "Cannot revoke self signed certificates");
                }
            }

            X509Certificate2 certCA = null;
            ICertificateStore store = storeIdentifier.OpenStore();
            try
            {
                if (store == null)
                {
                    throw new ArgumentException("Invalid store path/type");
                }
                certCA = await X509Utils.FindIssuerCABySerialNumberAsync(store, certificate.IssuerName, serialNumber).ConfigureAwait(false);

                if (certCA == null)
                {
                    throw new ServiceResultException(StatusCodes.BadCertificateInvalid, "Cannot find issuer certificate in store.");
                }

                CertificateIdentifier certCAIdentifier = new CertificateIdentifier(certCA) {
                    StorePath = store.StorePath,
                    StoreType = store.StoreType
                };
                X509Certificate2 certCAWithPrivateKey = await certCAIdentifier.LoadPrivateKey(issuerKeyFilePassword).ConfigureAwait(false);

                if (certCAWithPrivateKey == null)
                {
                    throw new ServiceResultException(StatusCodes.BadCertificateInvalid, "Failed to load issuer private key. Is the password correct?");
                }

                if (!certCAWithPrivateKey.HasPrivateKey)
                {
                    throw new ServiceResultException(StatusCodes.BadCertificateInvalid, "Issuer certificate has no private key, cannot revoke certificate.");
                }

                var certCACrl = await store.EnumerateCRLs(certCA, false).ConfigureAwait(false);

                var certificateCollection = new X509Certificate2Collection();
                if (!isCACert)
                {
                    certificateCollection.Add(certificate);
                }
                updatedCRL = CertificateFactory.RevokeCertificate(certCAWithPrivateKey, certCACrl, certificateCollection);

                await store.AddCRL(updatedCRL).ConfigureAwait(false);

                // delete outdated CRLs from store
                foreach (X509CRL caCrl in certCACrl)
                {
                    await store.DeleteCRL(caCrl).ConfigureAwait(false);
                }
            }
            finally
            {
                store.Close();
            }
            return updatedCRL;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the certificate authority certificate and CRL in the provided CertificateStore
        /// </summary>
        /// <param name="trustedOrIssuerStoreIdentifier">The store which contains the authority ceritificate. (trusted or issuer)</param>
        /// <returns></returns>
        protected async Task UpdateAuthorityCertInCertificateStore(CertificateStoreIdentifier trustedOrIssuerStoreIdentifier)
        {
            ICertificateStore authorityStore = AuthoritiesStore.OpenStore();
            ICertificateStore trustedOrIssuerStore = trustedOrIssuerStoreIdentifier.OpenStore();
            try
            {
                if (authorityStore == null || trustedOrIssuerStore == null)
                {
                    throw new ServiceResultException("Unable to update authority certificate in stores");
                }

                X509Certificate2Collection certificates = await authorityStore.Enumerate().ConfigureAwait(false);
                foreach (var certificate in certificates)
                {
                    if (X509Utils.CompareDistinguishedName(certificate.Subject, SubjectName))
                    {
                        X509Certificate2Collection certs = await trustedOrIssuerStore.FindByThumbprint(certificate.Thumbprint).ConfigureAwait(false);
                        if (certs.Count == 0)
                        {
                            using (var x509 = X509CertificateLoader.LoadCertificate(certificate.RawData))
                            {
                                await trustedOrIssuerStore.Add(x509).ConfigureAwait(false);
                            }
                        }

                        // delete existing CRL in trusted list
                        foreach (var crl in await trustedOrIssuerStore.EnumerateCRLs(certificate, false).ConfigureAwait(false))
                        {
                            if (crl.VerifySignature(certificate, false))
                            {
                                await trustedOrIssuerStore.DeleteCRL(crl).ConfigureAwait(false);
                            }
                        }

                        // copy latest CRL to trusted list
                        foreach (var crl in await authorityStore.EnumerateCRLs(certificate, true).ConfigureAwait(false))
                        {
                            await trustedOrIssuerStore.AddCRL(crl).ConfigureAwait(false);
                        }
                    }
                }
            }
            finally
            {
                authorityStore?.Close();
                trustedOrIssuerStore?.Close();
            }
        }

        protected X509SubjectAltNameExtension GetAltNameExtensionFromCSRInfo(Org.BouncyCastle.Asn1.Pkcs.CertificationRequestInfo info)
        {
            try
            {
                for (int i = 0; i < info.Attributes.Count; i++)
                {
                    var sequence = Org.BouncyCastle.Asn1.Asn1Sequence.GetInstance(info.Attributes[i].ToAsn1Object());
                    var oid = Org.BouncyCastle.Asn1.DerObjectIdentifier.GetInstance(sequence[0].ToAsn1Object());
                    if (oid.Equals(Org.BouncyCastle.Asn1.Pkcs.PkcsObjectIdentifiers.Pkcs9AtExtensionRequest))
                    {
                        var extensionInstance = Org.BouncyCastle.Asn1.DerSet.GetInstance(sequence[1]);
                        var extensionSequence = Org.BouncyCastle.Asn1.Asn1Sequence.GetInstance(extensionInstance[0]);
                        var extensions = Org.BouncyCastle.Asn1.X509.X509Extensions.GetInstance(extensionSequence);
                        Org.BouncyCastle.Asn1.X509.X509Extension extension = extensions.GetExtension(Org.BouncyCastle.Asn1.X509.X509Extensions.SubjectAlternativeName);
                        var asnEncodedAltNameExtension = new System.Security.Cryptography.AsnEncodedData(Org.BouncyCastle.Asn1.X509.X509Extensions.SubjectAlternativeName.ToString(), extension.Value.GetOctets());
                        var altNameExtension = new X509SubjectAltNameExtension(asnEncodedAltNameExtension, extension.IsCritical);
                        return altNameExtension;
                    }
                }
            }
            catch
            {
                throw new ServiceResultException(StatusCodes.BadInvalidArgument, "CSR altNameExtension invalid.");
            }
            return null;
        }
        #endregion

        #region Protected Properties
        protected string SubjectName { get; }
        #endregion 

    }

}
