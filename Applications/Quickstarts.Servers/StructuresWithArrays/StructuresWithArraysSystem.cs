/* ========================================================================
 * Copyright (c) 2005-2019 The OPC Foundation, Inc. All rights reserved.
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
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using Opc.Ua;
using Opc.Ua.Server;
using Opc.Ua.Test;

namespace StructuresWithArrays
{
    public class StructuresWithArraysSystem
    {
        public StructuresWithArraysSystem(INodeManager2 system, NamespaceTable namespaceUris, StringTable serverUris)
        {
            m_randomSource = new RandomSource();
            m_generator = new DataGenerator(m_randomSource);
            m_generator.NamespaceUris = namespaceUris;
            m_generator.ServerUris = serverUris;
            m_system = system;
        }

        /// <summary>
        /// The data generator for the system.
        /// </summary>
        public Opc.Ua.Test.DataGenerator DataGenerator => m_generator;

        /// <summary>
        /// 
        /// </summary>
        public Structure_A GetRandomStructureA()
        {
            return new Structure_A() {
                Double_Field = m_generator.GetRandomDouble(),
                Duration_Field = m_generator.GetRandomDouble()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Structure_B GetRandomStructureB()
        {
            return new Structure_B() {
                Duration_Field = m_generator.GetRandomDouble(),
                Int32_Field = m_generator.GetRandomInt32()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Structure_C GetRandomStructureC()
        {
            return new Structure_C() {
                Int32_Field = m_generator.GetRandomInt32()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public RolePermissionType GetRandomRolePermissionType()
        {
            NodeId[] roleTypes = {
                Opc.Ua.ObjectIds.WellKnownRole_Anonymous,
                Opc.Ua.ObjectIds.WellKnownRole_AuthenticatedUser,
                Opc.Ua.ObjectIds.WellKnownRole_ConfigureAdmin,
                Opc.Ua.ObjectIds.WellKnownRole_Engineer,
                Opc.Ua.ObjectIds.WellKnownRole_Observer,
                Opc.Ua.ObjectIds.WellKnownRole_Operator,
                Opc.Ua.ObjectIds.WellKnownRole_SecurityAdmin,
                Opc.Ua.ObjectIds.WellKnownRole_Supervisor
                };
            RolePermissionType rolePermissionType = new RolePermissionType {
                RoleId = roleTypes[m_generator.GetRandomByte() % roleTypes.Length],
                Permissions = m_generator.GetRandomByte()
            };
            return rolePermissionType;
        }

        /// <summary>
        /// 
        /// </summary>
        public LargeComplexStructure GetRandomLargeComplexStructure()
        {
            return new LargeComplexStructure {
                Scalar_Structure_A = GetRandomStructureA(),
                Scalar_Structure_B = GetRandomStructureB(),
                Scalar_Boolean = m_generator.GetRandomBoolean(),
                Scalar_Duration = m_generator.GetRandomDouble(),
                Scalar_String_within_max_length = m_generator.GetRandomString(),
                Scalar_String_no_max_length = m_generator.GetRandomString(),
                Scalar_Structure = m_generator.GetRandomBoolean() ? new ExtensionObject(GetRandomStructureA()) : new ExtensionObject(GetRandomStructureB()),
                Scalar_RolePermissionType = GetRandomRolePermissionType(),
                Scalar_BaseDataType = m_generator.GetRandomVariant(),
                Scalar_ApplicationDescription = GetRandomApplicationDescription(),
            };
        }

        private ApplicationDescription GetRandomApplicationDescription()
        {
            var domainNames = RandomDomainNames();
            string localhost = domainNames[0];
            string pureAppName = m_generator.GetRandomString("en");
            pureAppName = Regex.Replace(pureAppName, @"[^\w\d\s]", "");
            string pureAppUri = Regex.Replace(pureAppName, @"[^\w\d]", "");
            string appName = "UA " + pureAppName;
            var appType = (ApplicationType)(m_randomSource.NextInt32((int)(ApplicationType.DiscoveryServer + 1)));
            string appUri = ("urn:localhost:opcfoundation.org:" + pureAppUri.ToLower()).Replace("localhost", localhost);
            string prodUri = "http://opcfoundation.org/UA/" + pureAppUri;
            int port = (m_generator.GetRandomInt16() & 0x1fff) + 50000;
            StringCollection discoveryUrls = new StringCollection();

            switch (appType)
            {
                case ApplicationType.Client:
                    appName += " Client";
                    break;
                case ApplicationType.ClientAndServer:
                    appName += " Client and";
                    goto case ApplicationType.Server;
                case ApplicationType.DiscoveryServer:
                    appName += " DiscoveryServer";
                    discoveryUrls = RandomDiscoveryUrl(domainNames, 4840, pureAppUri);
                    break;
                case ApplicationType.Server:
                    appName += " Server";
                    discoveryUrls = RandomDiscoveryUrl(domainNames, port, pureAppUri);
                    break;
            }

            ApplicationDescription applicationDescription = new ApplicationDescription {
                ApplicationName = appName,
                ApplicationType = appType,
                ApplicationUri = appUri,
                DiscoveryProfileUri = null,
                DiscoveryUrls = discoveryUrls,
                GatewayServerUri = null,
                ProductUri  = prodUri
            };

            return applicationDescription;
        }

        private string RandomLocalHost()
        {
            string localhost = Regex.Replace(m_generator.GetRandomSymbol("en").Trim().ToLower(), @"[^\w\d]", "");
            if (localhost.Length >= 12)
            {
                localhost = localhost.Substring(0, 12);
            }
            return localhost;
        }

        private string[] RandomDomainNames()
        {
            int count = m_randomSource.NextInt32(8) + 1;
            var result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = RandomLocalHost();
            }
            return result;
        }

        private StringCollection RandomDiscoveryUrl(StringCollection domainNames, int port, string appUri)
        {
            var result = new StringCollection();
            foreach (var name in domainNames)
            {
                int random = m_randomSource.NextInt32(7);
                if ((result.Count == 0) || (random & 1) == 0)
                {
                    result.Add(String.Format("opc.tcp://{0}:{1}/{2}", name, (port++).ToString(), appUri));
                }
                if ((random & 2) == 0)
                {
                    result.Add(String.Format("http://{0}:{1}/{2}", name, (port++).ToString(), appUri));
                }
                if ((random & 4) == 0)
                {
                    result.Add(String.Format("https://{0}:{1}/{2}", name, (port++).ToString(), appUri));
                }
            }
            return result;
        }

        #region Private Fields
        private object m_lock = new object();
        private IRandomSource m_randomSource;
        private DataGenerator m_generator;
        private INodeManager2 m_system;
        #endregion
    }
}
