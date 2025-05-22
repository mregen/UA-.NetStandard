/* ========================================================================
 * Copyright (c) 2005-2025 The OPC Foundation, Inc. All rights reserved.
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
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Opc.Ua.Server.UserDatabase
{
    /// <summary>
    /// A user database with JSON storage.
    /// </summary>
    /// <remarks>
    /// This db is used for testing, not for production.
    /// </remarks>
    public class JsonUserDatabase : LinqUserDatabase
    {
        #region Constructors
        /// <summary>
        /// Create a JSON database.
        /// </summary>
        public JsonUserDatabase(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Load the JSON application database.
        /// </summary>
        public static IUserDatabase Load(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            try
            {
                if (File.Exists(fileName))
                {
                    var utf8Json = File.ReadAllBytes(fileName);
                    var utf8JsonReader = new Utf8JsonReader(utf8Json, allowTrailingCommasInJsonReader);
                    var db = JsonSerializer.Deserialize<JsonUserDatabase>(ref utf8JsonReader, exchangeJsonSerializerOptions);
                    db.FileName = fileName;
                    return db;
                }
            }
            catch
            {
                Utils.LogWarning("User database {0} was not found.", fileName);
            }
            return new JsonUserDatabase(fileName);
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Save the complete database.
        /// </summary>
        protected override void Save()
        {
            var utf8Json = JsonSerializer.SerializeToUtf8Bytes<JsonUserDatabase>(this, exchangeJsonSerializerOptions);
            File.WriteAllBytes(FileName, utf8Json);
        }

        /// <summary>
        /// Get or set the filename.
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string FileName { get; private set; }
        #endregion

        #region Private Fields
        private static readonly JsonSerializerOptions exchangeJsonSerializerOptions = new JsonSerializerOptions {
            AllowTrailingCommas = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            TypeInfoResolver = new SystemRuntimeSerializationAttributeResolver(),
        };

        private static readonly JsonReaderOptions allowTrailingCommasInJsonReader = new JsonReaderOptions {
            AllowTrailingCommas = true,
        };

        private class SystemRuntimeSerializationAttributeResolver : DefaultJsonTypeInfoResolver
        {
            public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
            {
                JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

                if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object &&
                    type.GetCustomAttribute<DataContractAttribute>() is not null)
                {
                    jsonTypeInfo.Properties.Clear();

                    foreach ((PropertyInfo propertyInfo, DataMemberAttribute attr) in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Select((prop) => (prop, prop.GetCustomAttribute<DataMemberAttribute>() as DataMemberAttribute))
                        .Where((x) => x.Item2 != null)
                        .OrderBy((x) => x.Item2!.Order))
                    {
                        JsonPropertyInfo jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(propertyInfo.PropertyType, attr.Name ?? propertyInfo.Name);
                        jsonPropertyInfo.Get =
                            propertyInfo.CanRead
                            ? propertyInfo.GetValue
                            : null;

                        jsonPropertyInfo.Set = propertyInfo.CanWrite
                            ? (obj, value) => propertyInfo.SetValue(obj, value)
                            : null;

                        jsonTypeInfo.Properties.Add(jsonPropertyInfo);
                    }
                }

                return jsonTypeInfo;
            }
        }
        #endregion
    }
}
