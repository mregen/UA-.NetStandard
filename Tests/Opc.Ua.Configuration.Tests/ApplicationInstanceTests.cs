/* ========================================================================
 * Copyright (c) 2005-2021 The OPC Foundation, Inc. All rights reserved.
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
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace Opc.Ua.Configuration.Tests
{
    /// <summary>
    /// Tests for the BuiltIn Types.
    /// </summary>
    [TestFixture, Category("ApplicationInstance")]
    [SetCulture("en-us"), SetUICulture("en-us")]
    [Parallelizable]
    public class ApplicationInstanceTests
    {
        #region Test Constants
        public const string ApplicationName = "UA Configuration Test";
        public const string ApplicationUri = "urn:localhost:opcfoundation.org:ConfigurationTest";
        public const string ProductUri = "http://opcfoundation.org/UA/ConfigurationTest";
        public const string SubjectName = "CN=UA Configuration Test, O=OPC Foundation, C=US, S=Arizona";
        public const string EndpointUrl = "opc.tcp://localhost:51000";
        #endregion

        #region Test Setup
        /// <summary>
        /// Test setup.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // pki directory root for test runs. 
            m_pkiRoot = Path.GetTempPath() + Path.GetRandomFileName() + Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Test setup.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            try
            {
                // pki directory root for test runs. 
                Directory.Delete(m_pkiRoot, true);
            }
            catch
            { }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Load a file configuration.
        /// </summary>
        [Test]
        public async Task TestFileConfigAsync()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            string configPath = Opc.Ua.Utils.GetAbsoluteFilePath("Opc.Ua.Configuration.Tests.Config.xml", true, false, false);
            Assert.NotNull(configPath);
            ApplicationConfiguration applicationConfiguration = await applicationInstance.LoadApplicationConfigurationAsync(configPath, true).ConfigureAwait(false);
            Assert.NotNull(applicationConfiguration);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        [Test]
        public async Task TestNoFileConfigAsClient()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .AsClient()
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        [Test]
        public async Task TestBadApplicationInstance()
        {
            // no app name
            var applicationInstance = new ApplicationInstance();
            Assert.NotNull(applicationInstance);
            Assert.ThrowsAsync<ServiceResultException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
            // discoveryserver can not be combined with client/server
            applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName,
                ApplicationType = ApplicationType.DiscoveryServer
            };
            Assert.ThrowsAsync<ArgumentException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsClient()
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
            Assert.ThrowsAsync<ArgumentException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
            // server overrides client settings
            applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName,
                ApplicationType = ApplicationType.Client
            };

            var config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .AsServer(new string[] { EndpointUrl })
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .Create()
                .ConfigureAwait(false);
            Assert.AreEqual(ApplicationType.Server, applicationInstance.ApplicationType);

            // client overrides server setting
            applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName,
                ApplicationType = ApplicationType.Server
            };

            await applicationInstance.Build(ApplicationUri, ProductUri)
                .AsClient()
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .Create()
                .ConfigureAwait(false);
            Assert.AreEqual(ApplicationType.Client, applicationInstance.ApplicationType);

            // invalid sec policy testing
            applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            // invalid use, use AddUnsecurePolicyNone instead
            Assert.ThrowsAsync<ArgumentException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddPolicy(MessageSecurityMode.None, SecurityPolicies.None)
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
            // invalid mix sign / none
            Assert.ThrowsAsync<ArgumentException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddPolicy(MessageSecurityMode.Sign, SecurityPolicies.None)
                   .AddSecurityConfiguration(SubjectName)
                   .Create()
                   .ConfigureAwait(false)
            );
            // invalid policy
            Assert.ThrowsAsync<ArgumentException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddPolicy(MessageSecurityMode.Sign, "123")
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
            // invalid user token policy
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
               await applicationInstance.Build(ApplicationUri, ProductUri)
                   .AsServer(new string[] { EndpointUrl })
                   .AddUserTokenPolicy(null)
                   .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                   .Create()
                   .ConfigureAwait(false)
            );
        }

        [Test]
        public async Task TestNoFileConfigAsServerMinimal()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .SetOperationTimeout(10000)
                .AsServer(new string[] { EndpointUrl })
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        [Test]
        public async Task TestNoFileConfigAsServerMaximal()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .SetTransportQuotas(new TransportQuotas() { OperationTimeout = 10000 })
                .AsServer(new string[] { EndpointUrl })
                .AddSignPolicies()
                .AddSignAndEncryptPolicies()
                .AddUnsecurePolicyNone()
                .AddPolicy(MessageSecurityMode.Sign, SecurityPolicies.Basic256)
                .AddPolicy(MessageSecurityMode.Sign, SecurityPolicies.Basic128Rsa15)
                .AddPolicy(MessageSecurityMode.SignAndEncrypt, SecurityPolicies.Basic256)
                .AddPolicy(MessageSecurityMode.SignAndEncrypt, SecurityPolicies.Basic128Rsa15)
                .AddUserTokenPolicy(UserTokenType.Anonymous)
                .AddUserTokenPolicy(UserTokenType.UserName)
                .AddUserTokenPolicy(new UserTokenPolicy(UserTokenType.Certificate) { SecurityPolicyUri = SecurityPolicies.Basic256Sha256 })
                .SetDiagnosticsEnabled(true)
                .SetPublishingResolution(100)
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .SetAddAppCertToTrustedStore(true)
                .SetAutoAcceptUntrustedCertificates(true)
                .SetMinimumCertificateKeySize(1024)
                .SetRejectSHA1SignedCertificates(false)
                .SetSendCertificateChain(true)
                .SetSuppressNonceValidationErrors(true)
                .SetRejectUnknownRevocationStatus(true)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        [Test]
        public async Task TestNoFileConfigAsClientAndServer()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .SetMaxBufferSize(32768)
                .AsServer(new string[] { EndpointUrl })
                .AddUnsecurePolicyNone()
                .AddSignPolicies()
                .AddSignAndEncryptPolicies()
                .AddPolicy(MessageSecurityMode.Sign, SecurityPolicies.Basic256)
                .SetDiagnosticsEnabled(true)
                .AsClient()
                .AddSecurityConfiguration(SubjectName, CertificateStoreType.Directory, CertificateStoreType.X509Store)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        /// <summary>
        /// Test case when app cert already exists or when new
        /// cert is created in X509Store.
        /// </summary>
        [Test, Repeat(2)]
        public async Task TestNoFileConfigAsServerX509Store()
        {
#if NETCOREAPP2_1_OR_GREATER
            // this test fails on macOS, ignore
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Assert.Ignore("X509Store trust lists not supported on mac OS.");
            }
#endif
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .AsServer(new string[] { EndpointUrl })
                .AddUnsecurePolicyNone()
                .AddSignAndEncryptPolicies()
                .AddUserTokenPolicy(UserTokenType.UserName)
                .AsClient()
                .SetDefaultSessionTimeout(10000)
                .AddSecurityConfiguration(SubjectName, CertificateStoreType.X509Store)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            var applicationCertificate = applicationInstance.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate;
            bool deleteAfterUse = applicationCertificate.Certificate != null;

            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
            using (ICertificateStore store = applicationInstance.ApplicationConfiguration.SecurityConfiguration.TrustedPeerCertificates.OpenStore())
            {
                // store public key in trusted store
                var rawData = applicationCertificate.Certificate.RawData;
                await store.Add(X509CertificateLoader.LoadCertificate(rawData)).ConfigureAwait(false);
            }

            if (deleteAfterUse)
            {
                var thumbprint = applicationCertificate.Certificate.Thumbprint;
                using (ICertificateStore store = applicationCertificate.OpenStore())
                {
                    bool success = await store.Delete(thumbprint).ConfigureAwait(false);
                    Assert.IsTrue(success);
                }
                using (ICertificateStore store = applicationInstance.ApplicationConfiguration.SecurityConfiguration.TrustedPeerCertificates.OpenStore())
                {
                    bool success = await store.Delete(thumbprint).ConfigureAwait(false);
                    Assert.IsTrue(success);
                }
            }
        }

        [Test]
        public async Task TestNoFileConfigAsServerCustom()
        {
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config = await applicationInstance.Build(ApplicationUri, ProductUri)
                .AsServer(new string[] { EndpointUrl, "opc.https://localhost:51001" }, new string[] { "opc.tcp://192.168.1.100:51000" })
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .SetAddAppCertToTrustedStore(true)
                .Create().ConfigureAwait(false);
            Assert.NotNull(config);
            bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            Assert.True(certOK);
        }

        public enum InvalidCertType
        {
            NoIssues,
            NoIssuer,
            Expired,
            IssuerExpired,
            NotYetValid,
            IssuerNotYetValid,
            KeySize1024,
            HostName
        };

        /// <summary>
        /// Test to verify that an existing cert with suppressible issues
        /// is not recreated/replaced.
        /// </summary>
        [Test]
        [TestCase(InvalidCertType.NoIssues, true, true)]
        [TestCase(InvalidCertType.NotYetValid, true, true)]
        [TestCase(InvalidCertType.Expired, true, true)]
        [TestCase(InvalidCertType.HostName, true, false)]
        [TestCase(InvalidCertType.HostName, false, true)]
        [TestCase(InvalidCertType.KeySize1024, true, false)]
        public async Task TestInvalidAppCertDoNotRecreate(InvalidCertType certType, bool server, bool suppress)
        {
            // pki directory root for test runs. 
            var pkiRoot = Path.GetTempPath() + Path.GetRandomFileName() + Path.DirectorySeparatorChar;

            var applicationInstance = new ApplicationInstance() { ApplicationName = ApplicationName };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config;
            if (server)
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsServer(new string[] { "opc.tcp://localhost:12345/Configuration" })
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }
            else
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsClient()
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }

            Assert.NotNull(config);

            CertificateIdentifier applicationCertificate =
                applicationInstance.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate;
            Assert.IsNull(applicationCertificate.Certificate);

            X509Certificate2 publicKey = null;
            using (var testCert = CreateInvalidCert(certType))
            {
                Assert.NotNull(testCert);
                Assert.True(testCert.HasPrivateKey);
                await testCert.AddToStoreAsync(
                    applicationCertificate.StoreType,
                    applicationCertificate.StorePath
                ).ConfigureAwait(false);
                publicKey = X509CertificateLoader.LoadCertificate(testCert.RawData);
            }

            using (publicKey)
            {
                if (suppress)
                {
                    bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0)
                        .ConfigureAwait(false);

                    Assert.True(certOK);
                    Assert.AreEqual(publicKey, applicationCertificate.Certificate);
                }
                else
                {
                    var sre = Assert.ThrowsAsync<ServiceResultException>(async () =>
                        await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false));
                    Assert.AreEqual((StatusCode)StatusCodes.BadConfigurationError, (StatusCode)sre.StatusCode);
                }
            }
        }
        /// <summary>
        /// Test to verify that an existing cert with suppressible issues
        /// is not recreated/replaced.
        /// </summary>
        [Test]
        [TestCase(InvalidCertType.NoIssues, true, true)]
        [TestCase(InvalidCertType.NoIssuer, true, false)]
        [TestCase(InvalidCertType.NotYetValid, true, true)]
        [TestCase(InvalidCertType.Expired, true, true)]
        [TestCase(InvalidCertType.IssuerNotYetValid, true, true)]
        [TestCase(InvalidCertType.IssuerExpired, true, true)]
        [TestCase(InvalidCertType.HostName, true, false)]
        [TestCase(InvalidCertType.HostName, false, true)]
        //TODO [TestCase(InvalidCertType.KeySize1024, true, false)]
        public async Task TestInvalidAppCertChainDoNotRecreate(InvalidCertType certType, bool server, bool suppress)
        {
            // pki directory root for test runs. 
            var pkiRoot = Path.GetTempPath() + Path.GetRandomFileName() + Path.DirectorySeparatorChar;

            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config;
            if (server)
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsServer(new string[] { "opc.tcp://localhost:12345/Configuration" })
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }
            else
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsClient()
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }
            Assert.NotNull(config);

            CertificateIdentifier applicationCertificate = applicationInstance.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate;
            Assert.IsNull(applicationCertificate.Certificate);

            var testCerts = CreateInvalidCertChain(certType);
            if (certType != InvalidCertType.NoIssuer)
            {
                using (var issuerCert = testCerts[1])
                {
                    Assert.NotNull(issuerCert);
                    Assert.False(issuerCert.HasPrivateKey);
                    await issuerCert.AddToStoreAsync(
                        applicationInstance.ApplicationConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StoreType,
                        applicationInstance.ApplicationConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StorePath
                        ).ConfigureAwait(false);
                }
            }

            X509Certificate2 publicKey = null;
            using (var testCert = testCerts[0])
            {
                Assert.NotNull(testCert);
                Assert.True(testCert.HasPrivateKey);
                await testCert.AddToStoreAsync(
                    applicationCertificate.StoreType,
                    applicationCertificate.StorePath
                    ).ConfigureAwait(false);
                publicKey = X509CertificateLoader.LoadCertificate(testCert.RawData);
            }

            using (publicKey)
            {
                if (suppress)
                {
                    bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0)
                        .ConfigureAwait(false);

                    Assert.True(certOK);
                    Assert.AreEqual(publicKey, applicationCertificate.Certificate);
                }
                else
                {
                    var sre = Assert.ThrowsAsync<ServiceResultException>(async () =>
                        await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false));
                    Assert.AreEqual((StatusCode)StatusCodes.BadConfigurationError, (StatusCode)sre.StatusCode);
                }
            }
        }

        /// <summary>
        /// Tests that a supplied certificate is stored in the Trusted store of the Server after calling method AddOwnCertificateToTrustedStoreAsync
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestAddOwnCertificateToTrustedStore()
        {
            //Arrange Application Instance
            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName
            };
            ApplicationConfiguration configuration = await applicationInstance.Build(ApplicationUri, ProductUri)
                .SetOperationTimeout(10000)
                .AsServer(new string[] { EndpointUrl })
                .AddSecurityConfiguration(SubjectName, m_pkiRoot)
                .Create().ConfigureAwait(false);

            //Arrange cert
            DateTime notBefore = DateTime.Today.AddDays(-30);
            DateTime notAfter = DateTime.Today.AddDays(30);

            using (var cert = CertificateFactory.CreateCertificate(SubjectName)
                .SetNotBefore(notBefore)
                .SetNotAfter(notAfter)
                .SetCAConstraint(-1)
                .CreateForRSA())
            {

                //Act
                await applicationInstance.AddOwnCertificateToTrustedStoreAsync(cert, new CancellationToken()).ConfigureAwait(false);
                ICertificateStore store = configuration.SecurityConfiguration.TrustedPeerCertificates.OpenStore();
                var storedCertificates = await store.FindByThumbprint(cert.Thumbprint).ConfigureAwait(false);

                //Assert
                Assert.IsTrue(storedCertificates.Contains(cert));
            }
        }

        /// <summary>
        /// Test to verify that a new cert is not recreated/replaced if DisableCertificateAutoCreation is set.
        /// </summary>
        [Theory]
        public async Task TestDisableCertificateAutoCreationAsync(bool server, bool disableCertificateAutoCreation)
        {
            // pki directory root for test runs. 
            var pkiRoot = Path.GetTempPath() + Path.GetRandomFileName() + Path.DirectorySeparatorChar;

            var applicationInstance = new ApplicationInstance() {
                ApplicationName = ApplicationName,
                DisableCertificateAutoCreation = disableCertificateAutoCreation
            };
            Assert.NotNull(applicationInstance);
            ApplicationConfiguration config;
            if (server)
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsServer(new string[] { "opc.tcp://localhost:12345/Configuration" })
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }
            else
            {
                config = await applicationInstance.Build(ApplicationUri, ProductUri)
                    .AsClient()
                    .AddSecurityConfiguration(SubjectName, pkiRoot)
                    .Create().ConfigureAwait(false);
            }
            Assert.NotNull(config);

            CertificateIdentifier applicationCertificate = applicationInstance.ApplicationConfiguration.SecurityConfiguration.ApplicationCertificate;
            Assert.IsNull(applicationCertificate.Certificate);

            if (disableCertificateAutoCreation)
            {
                var sre = Assert.ThrowsAsync<ServiceResultException>(async () =>
                    await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false));
                Assert.AreEqual((StatusCode)StatusCodes.BadConfigurationError, (StatusCode)sre.StatusCode);
            }
            else
            {
                bool certOK = await applicationInstance.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
                Assert.True(certOK);
            }
        }
        #endregion

        #region Private Methods
        private X509Certificate2 CreateInvalidCert(InvalidCertType certType)
        {
            // reasonable defaults
            DateTime notBefore = DateTime.Today.AddDays(-30);
            DateTime notAfter = DateTime.Today.AddDays(30);
            ushort keySize = CertificateFactory.DefaultKeySize;
            string[] domainNames = new string[] { Utils.GetHostName() };
            switch (certType)
            {
                case InvalidCertType.Expired:
                    notBefore = DateTime.Today.AddMonths(-12);
                    notAfter = DateTime.Today.AddDays(-7);
                    break;
                case InvalidCertType.NotYetValid:
                    notBefore = DateTime.Today.AddDays(7);
                    notAfter = notBefore.AddMonths(12);
                    break;
                case InvalidCertType.KeySize1024:
                    keySize = 1024;
                    break;
                case InvalidCertType.HostName:
                    domainNames = new string[] { "myhost", "1.2.3.4" };
                    break;
                default:
                    break;
            }

            return CertificateFactory.CreateCertificate(
                ApplicationUri,
                ApplicationName,
                SubjectName,
                domainNames)
                .SetNotBefore(notBefore)
                .SetNotAfter(notAfter)
                .SetRSAKeySize(keySize)
                .CreateForRSA();
        }

        private X509Certificate2Collection CreateInvalidCertChain(InvalidCertType certType)
        {
            // reasonable defaults
            DateTime notBefore = DateTime.Today.AddYears(-1);
            DateTime notAfter = DateTime.Today.AddYears(1);
            DateTime issuerNotBefore = notBefore;
            DateTime issuerNotAfter = notAfter;
            ushort keySize = CertificateFactory.DefaultKeySize;
            string[] domainNames = new string[] { Utils.GetHostName() };
            switch (certType)
            {
                case InvalidCertType.Expired:
                    notAfter = DateTime.Today.AddDays(-7);
                    break;
                case InvalidCertType.IssuerExpired:
                    issuerNotAfter = DateTime.Today.AddDays(-7);
                    break;
                case InvalidCertType.NotYetValid:
                    notBefore = DateTime.Today.AddDays(7);
                    break;
                case InvalidCertType.IssuerNotYetValid:
                    issuerNotBefore = DateTime.Today.AddDays(7);
                    break;
                case InvalidCertType.KeySize1024:
                    keySize = 1024;
                    break;
                case InvalidCertType.HostName:
                    domainNames = new string[] { "myhost", "1.2.3.4" };
                    break;
                default:
                    break;
            }

            string rootCASubjectName = "CN=Root CA Test, O=OPC Foundation, C=US, S=Arizona";
            using (var rootCA = CertificateFactory.CreateCertificate(rootCASubjectName)
                .SetNotBefore(issuerNotBefore)
                .SetNotAfter(issuerNotAfter)
                .SetCAConstraint(-1)
                .CreateForRSA())
            {

                var appCert = CertificateFactory.CreateCertificate(
                    ApplicationUri,
                    ApplicationName,
                    SubjectName,
                    domainNames)
                    .SetNotBefore(notBefore)
                    .SetNotAfter(notAfter)
                    .SetIssuer(rootCA)
                    .SetRSAKeySize(keySize)
                    .CreateForRSA();

                var result = new X509Certificate2Collection {
                    appCert,
                    X509CertificateLoader.LoadCertificate(rootCA.RawData)
                };

                return result;
            }
        }
        #endregion

        #region Private Fields
        string m_pkiRoot;
        #endregion
    }
}
