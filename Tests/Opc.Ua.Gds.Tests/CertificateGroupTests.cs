using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using NUnit.Framework;
using Opc.Ua.Gds.Server;
using Opc.Ua.Security.Certificates;
using Assert = NUnit.Framework.Legacy.ClassicAssert;


namespace Opc.Ua.Gds.Tests
{
    [TestFixture, Category("GDS")]
    [SetCulture("en-us"), SetUICulture("en-us")]
    [Parallelizable]
    public class CertificateGroupTests
    {

        private string m_path;

        [SetUp]
        public void Setup()
        {
            m_path = Utils.ReplaceSpecialFolderNames("%LocalApplicationData%/OPC/GDS/TestStore");
        }

        [TearDown]
        public void Dispose()
        {
            if (Directory.Exists(m_path))
            {
                Directory.Delete(m_path, true);
            }
        }

        #region Test Methods
        [Test]
        public void TestCreateCACertificateAsyncThrowsException()
        {
            var configuration = new CertificateGroupConfiguration();
            configuration.SubjectName = "CN=GDS Test CA, O=OPC Foundation";
            configuration.BaseStorePath = m_path;
            var certificateGroup = new CertificateGroup().Create(m_path + "/authorities", configuration);
            Assert.That(() => certificateGroup.CreateCACertificateAsync("This is not the ValidSubjectName for my CertificateGroup"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task TestCreateCACertificateAsyncCertIsInTrustedStoreAsync()
        {
            var configuration = new CertificateGroupConfiguration();
            configuration.SubjectName = "CN=GDS Test CA, O=OPC Foundation";
            configuration.BaseStorePath = m_path;
            var certificateGroup = new CertificateGroup().Create(m_path + "/authorities", configuration);
            var certificate = await certificateGroup.CreateCACertificateAsync(configuration.SubjectName).ConfigureAwait(false);
            Assert.NotNull(certificate);
            var certificateStoreIdentifier = new CertificateStoreIdentifier(configuration.TrustedListPath);
            using (ICertificateStore trustedStore = certificateStoreIdentifier.OpenStore())
            {
                X509Certificate2Collection certs = await trustedStore.FindByThumbprint(certificate.Thumbprint).ConfigureAwait(false);
                Assert.IsTrue(certs.Count == 1);
            }
        }

        [Test]
        public async Task TestCreateCACertificateAsyncCertIsInTrustedIssuerStoreAsync()
        {
            var applicatioConfiguration = new ApplicationConfiguration();
            applicatioConfiguration.SecurityConfiguration = new SecurityConfiguration();
            applicatioConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StorePath = m_path + Path.DirectorySeparatorChar + "issuers";
            applicatioConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StoreType = CertificateStoreType.Directory;
            var cgConfiguration = new CertificateGroupConfiguration();
            cgConfiguration.SubjectName = "CN=GDS Test CA, O=OPC Foundation";
            cgConfiguration.BaseStorePath = m_path;
            var certificateGroup = new CertificateGroup().Create(m_path + Path.DirectorySeparatorChar + "authorities", cgConfiguration, applicatioConfiguration.SecurityConfiguration.TrustedIssuerCertificates.StorePath);
            X509Certificate2 certificate = await certificateGroup.CreateCACertificateAsync(cgConfiguration.SubjectName).ConfigureAwait(false);
            Assert.NotNull(certificate);
            using (ICertificateStore trustedStore = applicatioConfiguration.SecurityConfiguration.TrustedIssuerCertificates.OpenStore())
            {
                X509Certificate2Collection certs = await trustedStore.FindByThumbprint(certificate.Thumbprint).ConfigureAwait(false);
                Assert.IsTrue(certs.Count == 1);
                X509CRLCollection crls = await trustedStore.EnumerateCRLs(certificate).ConfigureAwait(false);
                Assert.AreEqual(1, crls.Count);
            }

            X509Certificate2 certificateUpdated = await certificateGroup.CreateCACertificateAsync(cgConfiguration.SubjectName).ConfigureAwait(false);
            Assert.NotNull(certificateUpdated);
            using (ICertificateStore trustedStore = applicatioConfiguration.SecurityConfiguration.TrustedIssuerCertificates.OpenStore())
            {
                X509Certificate2Collection certs = await trustedStore.FindByThumbprint(certificate.Thumbprint).ConfigureAwait(false);
                Assert.IsTrue(certs.Count == 1);
                X509CRLCollection crls = await trustedStore.EnumerateCRLs(certificateUpdated).ConfigureAwait(false);
                Assert.AreEqual(1, crls.Count);
            }
        }
        #endregion
    }
}
