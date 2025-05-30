/* Copyright (c) 1996-2022 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation Corporate Members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

using System;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml;

namespace Opc.Ua
{
    /// <summary>
    /// A generic user identity class.
    /// </summary>
    [DataContract(Namespace = Namespaces.OpcUaXsd)]
    public class UserIdentity : IUserIdentity
    {
        #region Constructors
        /// <summary>
        /// Initializes the object as an anonymous user.
        /// </summary>
        public UserIdentity()
        {
            var token = new AnonymousIdentityToken();
            Initialize(token);
        }

        /// <summary>
        /// Initializes the object with a username and password.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        public UserIdentity(string username, byte[] password)
        {
            var token = new UserNameIdentityToken {
                UserName = username,
                DecryptedPassword = password
            };
            Initialize(token);
        }

        /// <summary>
        /// Initializes the object with a username and password.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        public UserIdentity(string username, ReadOnlySpan<byte> password)
        {
            var token = new UserNameIdentityToken {
                UserName = username,
                DecryptedPassword = password.ToArray()
            };
            Initialize(token);
        }

        /// <summary>
        /// Initializes the object with a UA identity token.
        /// </summary>
        /// <param name="issuedToken">The token.</param>
        public UserIdentity(IssuedIdentityToken issuedToken)
        {
            Initialize(issuedToken);
        }

        /// <summary>
        /// Initializes the object with a X509 certificate identifier.
        /// </summary>
        public UserIdentity(CertificateIdentifier certificateId)
            : this(certificateId, new CertificatePasswordProvider())
        {
        }

        /// <summary>
        /// Initializes the object with a X509 certificate identifier and a CertificatePasswordProvider.
        /// </summary>
        public UserIdentity(CertificateIdentifier certificateId, CertificatePasswordProvider certificatePasswordProvider)
        {
            if (certificateId == null) throw new ArgumentNullException(nameof(certificateId));

            X509Certificate2 certificate = certificateId.LoadPrivateKeyEx(certificatePasswordProvider).GetAwaiter().GetResult();

            if (certificate == null || !certificate.HasPrivateKey)
            {
                throw new ServiceResultException("Cannot create User Identity with CertificateIdentifier that does not contain a private key.");
            }

            Initialize(certificate);
        }

        /// <summary>
        /// Initializes the object with a X509 certificate.
        /// </summary>
        public UserIdentity(X509Certificate2 certificate)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));
            if (!certificate.HasPrivateKey) throw new ServiceResultException("Cannot create User Identity with Certificate that does not have a private key");
            Initialize(certificate);
        }

        /// <summary>
        /// Initializes the object with a UA identity token.
        /// </summary>
        /// <param name="token">The user identity token.</param>
        public UserIdentity(UserIdentityToken token)
        {
            Initialize(token);
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        /// <remarks>
        /// The user identity encodes only the token type,
        /// the issued token and the policy id, if available.
        /// Hence, the default constructor
        /// is used to initialize the token as anonymous.
        /// </remarks>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            Initialize(new AnonymousIdentityToken());
        }
        #endregion

        #region IUserIdentity Members
        /// <summary>
        /// Gets or sets the UserIdentityToken PolicyId associated with the UserIdentity.
        /// </summary>
        /// <remarks>
        /// This value is used to initialize the UserIdentityToken object when GetIdentityToken() is called.
        /// </remarks>
        [DataMember(Name = "PolicyId", IsRequired = false, Order = 10)]
        public string PolicyId
        {
            get { return m_token.PolicyId; }
            set { m_token.PolicyId = value; }
        }

        /// <summary cref="IUserIdentity.DisplayName" />
        public string DisplayName
        {
            get { return m_displayName; }
            set { m_displayName = value; }
        }

        /// <summary cref="IUserIdentity.TokenType" />
        [DataMember(Name = "TokenType", IsRequired = true, Order = 20)]
        public UserTokenType TokenType
        {
            get { return m_tokenType; }
            private set { m_tokenType = value; }
        }

        /// <summary cref="IUserIdentity.IssuedTokenType" />
        [DataMember(Name = "IssuedTokenType", IsRequired = false, Order = 30)]
        public XmlQualifiedName IssuedTokenType
        {
            get { return m_issuedTokenType; }
            private set { m_issuedTokenType = value; }
        }

        /// <summary cref="IUserIdentity.SupportsSignatures" />
        public bool SupportsSignatures
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///  Get or sets the list of granted role ids associated to the UserIdentity.
        /// </summary>
        public NodeIdCollection GrantedRoleIds
        {
            get { return m_grantedRoleIds; }
        }

        /// <summary cref="IUserIdentity.GetIdentityToken" />
        public UserIdentityToken GetIdentityToken()
        {
            // check for null and return anonymous.
            if (m_token == null)
            {
                return new AnonymousIdentityToken();
            }
            else
            {
                return m_token;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the object with a UA identity token
        /// </summary>
        private void Initialize(UserIdentityToken token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            m_grantedRoleIds = new NodeIdCollection();
            m_token = token;

            if (token is UserNameIdentityToken usernameToken)
            {
                m_tokenType = UserTokenType.UserName;
                m_issuedTokenType = null;
                m_displayName = usernameToken.UserName;
                return;
            }

            if (token is X509IdentityToken x509Token)
            {
                m_tokenType = UserTokenType.Certificate;
                m_issuedTokenType = null;
                if (x509Token.Certificate != null)
                {
                    m_displayName = x509Token.Certificate.Subject;
                }
                else
                {
                    X509Certificate2 cert = CertificateFactory.Create(x509Token.CertificateData, true);
                    m_displayName = cert.Subject;
                }
                return;
            }

            if (token is IssuedIdentityToken issuedToken)
            {
                if (issuedToken.IssuedTokenType == Ua.IssuedTokenType.JWT)
                {
                    if (issuedToken.DecryptedTokenData == null || issuedToken.DecryptedTokenData.Length == 0)
                    {
                        throw new ArgumentException("JSON Web Token has no data associated with it.", nameof(token));
                    }

                    m_tokenType = UserTokenType.IssuedToken;
                    m_issuedTokenType = new XmlQualifiedName("", Opc.Ua.Profiles.JwtUserToken);
                    m_displayName = "JWT";
                    return;
                }
                else
                {
                    throw new NotSupportedException("Only JWT Issued Tokens are supported!");
                }
            }

            if (token is AnonymousIdentityToken anonymousToken)
            {
                m_tokenType = UserTokenType.Anonymous;
                m_issuedTokenType = null;
                m_displayName = "Anonymous";
                return;
            }

            throw new ArgumentException("Unrecognized UA user identity token type.", nameof(token));
        }

        /// <summary>
        /// Initializes the object with an X509 certificate
        /// </summary>
        private void Initialize(X509Certificate2 certificate)
        {
            var token = new X509IdentityToken {
                CertificateData = certificate.RawData,
                Certificate = certificate
            };
            Initialize(token);
        }
        #endregion

        #region Private Fields
        private UserIdentityToken m_token;
        private string m_displayName;
        private UserTokenType m_tokenType;
        private XmlQualifiedName m_issuedTokenType;
        private NodeIdCollection m_grantedRoleIds;
        #endregion
    }

    #region ImpersonationContext Class
    /// <summary>
    /// Stores information about the user that is currently being impersonated.
    /// </summary>
    public class ImpersonationContext
    {
        #region Public Members
        /// <summary>
        /// The security principal being impersonated.
        /// </summary>
        public IPrincipal Principal { get; set; }
        #endregion
    }
    #endregion
}
