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
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Opc.Ua.Configuration;
using Opc.Ua.Gds.Server;
using Opc.Ua.Gds.Server.Database.Linq;
using Opc.Ua.Server;
using Opc.Ua.Server.UserDatabase;

namespace Opc.Ua.Gds.Tests
{
    public class GlobalDiscoveryTestServer
    {
        public GlobalDiscoverySampleServer Server => m_server;
        public ApplicationInstance Application { get; private set; }
        public ApplicationConfiguration Config { get; private set; }
        public int BasePort { get; private set; }

        public GlobalDiscoveryTestServer(bool autoAccept)
        {
            s_autoAccept = autoAccept;
        }

        public async Task StartServer(bool clean, int basePort = -1, string storeType = CertificateStoreType.Directory)
        {
            ApplicationInstance.MessageDlg = new ApplicationMessageDlg();

            string configSectionName = "Opc.Ua.GlobalDiscoveryTestServer";
            if (storeType == CertificateStoreType.X509Store)
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    throw new PlatformNotSupportedException("X509 Store with crls is only supported on Windows");
                }
                configSectionName = "Opc.Ua.GlobalDiscoveryTestServerX509Stores";
            }
            Application = new ApplicationInstance {
                ApplicationName = "Global Discovery Server",
                ApplicationType = ApplicationType.Server,
                ConfigSectionName = configSectionName
            };

            BasePort = basePort;
            Config = await Load(Application, basePort).ConfigureAwait(false);

            if (clean)
            {
                string thumbprint = Config.SecurityConfiguration.ApplicationCertificate.Thumbprint;
                if (thumbprint != null)
                {
                    using (var store = Config.SecurityConfiguration.ApplicationCertificate.OpenStore())
                    {
                        await store.Delete(thumbprint).ConfigureAwait(false);
                    }
                }

                // always start with clean cert store
                await TestUtils.CleanupTrustList(Config.SecurityConfiguration.ApplicationCertificate).ConfigureAwait(false);
                await TestUtils.CleanupTrustList(Config.SecurityConfiguration.TrustedIssuerCertificates).ConfigureAwait(false);
                await TestUtils.CleanupTrustList(Config.SecurityConfiguration.TrustedPeerCertificates).ConfigureAwait(false);
                await TestUtils.CleanupTrustList(Config.SecurityConfiguration.RejectedCertificateStore).ConfigureAwait(false);

                Config = await Load(Application, basePort).ConfigureAwait(false);
            }

            // check the application certificate.
            bool haveAppCertificate = await Application.CheckApplicationInstanceCertificateAsync(true, 0).ConfigureAwait(false);
            if (!haveAppCertificate)
            {
                throw new Exception("Application instance certificate invalid!");
            }

            if (!Config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                Config.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }

            // get the DatabaseStorePath configuration parameter.
            GlobalDiscoveryServerConfiguration gdsConfiguration = Config.ParseExtension<GlobalDiscoveryServerConfiguration>();
            string databaseStorePath = Utils.ReplaceSpecialFolderNames(gdsConfiguration.DatabaseStorePath);
            string usersDatabaseStorePath = Utils.ReplaceSpecialFolderNames(gdsConfiguration.UsersDatabaseStorePath);

            if (clean)
            {
                // clean up database
                if (File.Exists(databaseStorePath))
                {
                    File.Delete(databaseStorePath);
                }
                if (File.Exists(usersDatabaseStorePath))
                {
                    File.Delete(usersDatabaseStorePath);
                }

                // clean up GDS stores
                TestUtils.DeleteDirectory(gdsConfiguration.AuthoritiesStorePath);
                TestUtils.DeleteDirectory(gdsConfiguration.ApplicationCertificatesStorePath);
                foreach (var group in gdsConfiguration.CertificateGroups)
                {
                    TestUtils.DeleteDirectory(group.BaseStorePath);
                }
            }

            var applicationsDatabase = JsonApplicationsDatabase.Load(databaseStorePath);
            var userDatabase = JsonUserDatabase.Load(usersDatabaseStorePath);

            RegisterDefaultUsers(userDatabase);

            // start the server.
            m_server = new GlobalDiscoverySampleServer(
                applicationsDatabase,
                applicationsDatabase,
                new CertificateGroup(),
                userDatabase);
            await Application.StartAsync(m_server).ConfigureAwait(false);

            ServerState serverState = Server.GetStatus().State;
            if (serverState != ServerState.Running)
            {
                throw new ServiceResultException("Server failed to start");
            }
        }

        public void StopServer()
        {
            if (m_server != null)
            {
                Console.WriteLine("Server stopped. Waiting for exit...");

                using (GlobalDiscoverySampleServer server = m_server)
                {
                    m_server = null;
                    // Stop server and dispose
                    server.Stop();
                }
            }
        }

        public string ReadLogFile()
        {
            return File.ReadAllText(Utils.ReplaceSpecialFolderNames(Config.TraceConfiguration.OutputFilePath));
        }

        public bool ResetLogFile()
        {
            try
            {
                File.Delete(Utils.ReplaceSpecialFolderNames(Config.TraceConfiguration.OutputFilePath));
                return true;
            }
            catch { }
            return false;
        }

        public string GetLogFilePath()
        {
            return Utils.ReplaceSpecialFolderNames(Config.TraceConfiguration.OutputFilePath);
        }

        private static void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted)
            {
                e.Accept = s_autoAccept;
                if (s_autoAccept)
                {
                    Console.WriteLine("Accepted Certificate: {0}", e.Certificate.Subject);
                }
                else
                {
                    Console.WriteLine("Rejected Certificate: {0}", e.Certificate.Subject);
                }
            }
        }

        /// <summary>
        /// Creates the default GDS users.
        /// </summary>
        private void RegisterDefaultUsers(IUserDatabase userDatabase)
        {
            userDatabase.CreateUser("sysadmin", "demo"u8, new List<Role> { GdsRole.CertificateAuthorityAdmin, GdsRole.DiscoveryAdmin, Role.SecurityAdmin, Role.ConfigureAdmin });
            userDatabase.CreateUser("appadmin", "demo"u8, new List<Role> { Role.AuthenticatedUser, GdsRole.CertificateAuthorityAdmin, GdsRole.DiscoveryAdmin });
            userDatabase.CreateUser("appuser", "demo"u8, new List<Role> { Role.AuthenticatedUser });

            userDatabase.CreateUser("DiscoveryAdmin", "demo"u8, new List<Role> { Role.AuthenticatedUser, GdsRole.DiscoveryAdmin });
            userDatabase.CreateUser("CertificateAuthorityAdmin", "demo"u8, new List<Role> { Role.AuthenticatedUser, GdsRole.CertificateAuthorityAdmin });
        }

        private static async Task<ApplicationConfiguration> Load(ApplicationInstance application, int basePort)
        {
#if !USE_FILE_CONFIG
            // load the application configuration.
            ApplicationConfiguration config = await application.LoadApplicationConfigurationAsync(true).ConfigureAwait(false);
#else
            string root = Path.Combine("%LocalApplicationData%", "OPC");
            string gdsRoot = Path.Combine(root, "GDS");
            var gdsConfig = new GlobalDiscoveryServerConfiguration() {
                AuthoritiesStorePath = Path.Combine(gdsRoot, "authorities"),
                ApplicationCertificatesStorePath = Path.Combine(gdsRoot, "applications"),
                DefaultSubjectNameContext = "O=OPC Foundation",
                CertificateGroups = new CertificateGroupConfigurationCollection()
                {
                    new CertificateGroupConfiguration() {
                        Id = "Default",
                        CertificateType = "RsaSha256ApplicationCertificateType",
                        SubjectName = "CN=GDS Test CA, O=OPC Foundation",
                        BaseStorePath = Path.Combine(gdsRoot, "CA", "default"),
                        DefaultCertificateHashSize = 256,
                        DefaultCertificateKeySize = 2048,
                        DefaultCertificateLifetime = 12,
                        CACertificateHashSize = 512,
                        CACertificateKeySize = 4096,
                        CACertificateLifetime = 60
                    }
                },
                DatabaseStorePath = Path.Combine(gdsRoot, "gdsdb.json"),
                UsersDatabaseStorePath = Path.Combine(gdsRoot, "gdsusersdb.json")
            };

            // build the application configuration.
            ApplicationConfiguration config = await application
                .Build(
                    "urn:localhost:opcfoundation.org:GlobalDiscoveryTestServer",
                    "http://opcfoundation.org/UA/GlobalDiscoveryTestServer")
                .AsServer(new string[] { "opc.tcp://localhost:58810/GlobalDiscoveryTestServer" })
                .AddUserTokenPolicy(UserTokenType.Anonymous)
                .AddUserTokenPolicy(UserTokenType.UserName)
                .SetDiagnosticsEnabled(true)
                .AddServerCapabilities("GDS")
                .AddServerProfile("http://opcfoundation.org/UA-Profile/Server/GlobalDiscoveryAndCertificateManagement2017")
                .SetShutdownDelay(0)
                .AddSecurityConfiguration(
                    "CN=Global Discovery Test Server, O=OPC Foundation, DC=localhost",
                    gdsRoot)
                .SetAutoAcceptUntrustedCertificates(true)
                .SetRejectSHA1SignedCertificates(false)
                .SetRejectUnknownRevocationStatus(true)
                .SetMinimumCertificateKeySize(1024)
                .AddExtension<GlobalDiscoveryServerConfiguration>(null, gdsConfig)
                .SetDeleteOnLoad(true)
                .SetOutputFilePath(Path.Combine(root, "Logs", "Opc.Ua.Gds.Tests.log.txt"))
                .SetTraceMasks(519)
                .Create().ConfigureAwait(false);
#endif
            TestUtils.PatchBaseAddressesPorts(config, basePort);
            return config;
        }

        private GlobalDiscoverySampleServer m_server;
        private static bool s_autoAccept = false;
    }
}
