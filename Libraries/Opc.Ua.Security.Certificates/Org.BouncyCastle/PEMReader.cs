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

#if !NETSTANDARD2_1 && !NET5_0_OR_GREATER
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Opc.Ua.Security.Certificates
{
    /// <summary>
    /// Methods or read PEM data.
    /// </summary>
    public static class PEMReader
    {
        #region Public Methods
        /// <summary>
        /// Import a private key from PEM.
        /// </summary>
        public static RSA ImportPrivateKeyFromPEM(
            byte[] pemDataBlob,
            ReadOnlySpan<char> password)
        {
            RSA rsaPrivateKey = null;
            Org.BouncyCastle.OpenSsl.PemReader pemReader;
            using (var pemStreamReader = new StreamReader(new MemoryStream(pemDataBlob), Encoding.UTF8, true))
            {
                if (password.IsEmpty || password.IsWhiteSpace())
                {
                    pemReader = new Org.BouncyCastle.OpenSsl.PemReader(pemStreamReader);
                }
                else
                {
                    var pwFinder = new Password(password.ToArray());
                    pemReader = new Org.BouncyCastle.OpenSsl.PemReader(pemStreamReader, pwFinder);
                }
                try
                {
                    // find the private key in the PEM blob
                    object pemObject = pemReader.ReadObject();
                    while (pemObject != null)
                    {
                        RsaPrivateCrtKeyParameters privateKey = null;
                        if (pemObject is Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair keypair)
                        {
                            privateKey = keypair.Private as RsaPrivateCrtKeyParameters;
                        }

                        if (privateKey == null)
                        {
                            privateKey = pemObject as RsaPrivateCrtKeyParameters;
                        }

                        if (privateKey != null)
                        {
                            rsaPrivateKey = RSA.Create();
                            rsaPrivateKey.ImportParameters(DotNetUtilities.ToRSAParameters(privateKey));
                            break;
                        }

                        // read next object
                        pemObject = pemReader.ReadObject();
                    }
                }
                finally
                {
                    pemReader.Reader.Dispose();
                }
            }

            if (rsaPrivateKey == null)
            {
                throw new CryptographicException("PEM data blob does not contain a private key.");
            }

            return rsaPrivateKey;
        }
        #endregion

        #region Internal class
        /// <summary>
        /// Wrapper for a password string.
        /// </summary>
        internal class Password
            : IPasswordFinder
        {
            private readonly char[] m_password;

            public Password(char[] word)
            {
                this.m_password = (char[])word.Clone();
            }

            public char[] GetPassword()
            {
                return (char[])m_password.Clone();
            }
        }
        #endregion
    }
}
#endif
