using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Opc.Ua;
using Opc.Ua.Compliance;

namespace Opc.Ua.Core.Tests.Types.Compliance
{
    [TestFixture]
    public class UserIdentityTokenRedactionTests
    {
        [Test]
        public void DecryptedPassword_ShouldBeRedacted_WhenRedactingSecretInformation()
        {
            // Arrange
            var token = new UserNameIdentityToken();
            var secret = new byte[] { 1, 2, 3, 4 };
            token.DecryptedPassword = secret;

            // Act
            var redacted = RedactSecretInformation(token);

            // Assert
            var redactedValue = GetPropertyValue<byte[]>(redacted, "DecryptedPassword");
            Assert.That(redactedValue, Is.Null.Or.Empty, "DecryptedPassword should be redacted");
        }

        // Example redaction logic for test purposes
        private static T RedactSecretInformation<T>(T obj)
        {
            var clone = Activator.CreateInstance(obj.GetType());
            foreach (var prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var isSecret = prop.GetCustomAttributes(true)
                    .Any(attr => attr.GetType().Name == "SecretInformationAttribute");
                if (!isSecret && prop.CanWrite)
                {
                    prop.SetValue(clone, prop.GetValue(obj));
                }
                // else: skip or set to null (redact)
            }
            return (T)clone;
        }

        private static T GetPropertyValue<T>(object obj, string propertyName)
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return (T)prop?.GetValue(obj);
        }

        // Map: Type -> (PropertyName -> AttributeType)
        private static readonly Dictionary<Type, Dictionary<string, Type>> ExpectedComplianceAttributes =
            new Dictionary<Type, Dictionary<string, Type>>
            {
                {
                    typeof(UserNameIdentityToken),
                    new Dictionary<string, Type>
                    {
                        { "DecryptedPassword", typeof(SecretInformationAttribute) }
                    }
                },
                {
                    typeof(IssuedIdentityToken),
                    new Dictionary<string, Type>
                    {
                        { "DecryptedTokenData", typeof(SecretInformationAttribute) }
                    }
                },
                {
                    typeof(X509IdentityToken),
                    new Dictionary<string, Type>
                    {
                        { "Certificate", typeof(CertificateInformationAttribute) }
                    }
                },
                {
                    typeof(Opc.Ua.EndpointType),
                    new Dictionary<string, Type>
                    {
                        { "EndpointUrl", typeof(EndpointInformationAttribute) }
                    }
                },
                {
                    typeof(Opc.Ua.PubSubKeyPushTargetDataType),
                    new Dictionary<string, Type>
                    {
                        { "EndpointUrl", typeof(EndpointInformationAttribute) }
                    }
                },
                {
                    typeof(Opc.Ua.UserManagementDataType),
                    new Dictionary<string, Type>
                    {
                        { "UserName", typeof(UserNameInformationAttribute) }
                    }
                }
            };

        [Test]
        public void AllExpectedProperties_HaveComplianceAttributes()
        {
            foreach (var typeEntry in ExpectedComplianceAttributes)
            {
                var type = typeEntry.Key;
                var expectedProps = typeEntry.Value;

                foreach (var propEntry in expectedProps)
                {
                    var propName = propEntry.Key;
                    var attrType = propEntry.Value;

                    var prop = type.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                    Assert.That(prop, Is.Not.Null, $"Property '{propName}' not found on type '{type.Name}'.");

                    var hasAttribute = prop.GetCustomAttributes(attrType, inherit: true).Any();
                    Assert.That(hasAttribute, Is.True,
                        $"Property '{type.Name}.{propName}' is expected to have attribute '{attrType.Name}' but does not.");
                }
            }
        }
    }
}
