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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace Opc.Ua
{
    /// <summary>
    /// Reads objects from a JSON stream.
    /// </summary>
    public class JsonDecoder : IJsonDecoder
    {
        #region Public Fields
        /// <summary>
        /// The name of the Root array if the json is defined as an array 
        /// </summary>
        public const string RootArrayName = "___root_array___";

        /// <summary>
        /// If TRUE then the NamespaceUris and ServerUris tables are updated with new URIs read from the JSON stream.
        /// </summary>
        public bool UpdateNamespaceTable { get; set; }
        #endregion

        #region Private Fields
        private JsonTextReader m_reader;
        private Dictionary<string, object> m_root;
        private Stack<object> m_stack;
        private IServiceMessageContext m_context;
        private ushort[] m_namespaceMappings;
        private ushort[] m_serverMappings;
        private uint m_nestingLevel;
        // JSON encoded value of: “9999-12-31T23:59:59Z”
        private DateTime m_dateTimeMaxJsonValue = new DateTime((long)3155378975990000000);
        private enum JTokenNullObject
        {
            Undefined = 0,
            Object = 1,
            Array = 2
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Create a JSON decoder to decode a string.
        /// </summary>
        /// <param name="json">The JSON encoded string.</param>
        /// <param name="context">The service message context to use.</param>
        public JsonDecoder(string json, IServiceMessageContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Initialize();

            m_context = context;
            m_nestingLevel = 0;
            m_reader = new JsonTextReader(new StringReader(json));
            m_root = ReadObject();
            m_stack = new Stack<object>();
            m_stack.Push(m_root);
        }

        /// <summary>
        /// Create a JSON decoder to decode a <see cref="Type"/>from a <see cref="JsonTextReader"/>.
        /// </summary>
        /// <param name="systemType">The system type of the encoded JSON stream.</param>
        /// <param name="reader">The text reader.</param>
        /// <param name="context">The service message context to use.</param>
        public JsonDecoder(Type systemType, JsonTextReader reader, IServiceMessageContext context)
        {
            Initialize();

            m_context = context;
            m_nestingLevel = 0;
            m_reader = reader;
            m_root = ReadObject();
            m_stack = new Stack<object>();
            m_stack.Push(m_root);
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_reader = null;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Decodes a session-less message from a buffer.
        /// </summary>
        public static IEncodeable DecodeSessionLessMessage(byte[] buffer, IServiceMessageContext context)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (context == null) throw new ArgumentNullException(nameof(context));

            using (IJsonDecoder decoder = new JsonDecoder(UTF8Encoding.UTF8.GetString(buffer), context))
            {
                // decode the actual message.
                SessionLessServiceMessage message = new SessionLessServiceMessage();
                message.Decode(decoder);
                return message.Message;
            }
        }

        /// <summary>
        /// Decodes a message from a buffer.
        /// </summary>
        public static IEncodeable DecodeMessage(byte[] buffer, System.Type expectedType, IServiceMessageContext context)
        {
            return DecodeMessage(new ArraySegment<byte>(buffer), expectedType, context);
        }

        /// <summary>
        /// Decodes a message from a buffer.
        /// </summary>
        public static IEncodeable DecodeMessage(ArraySegment<byte> buffer, System.Type expectedType, IServiceMessageContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // check that the max message size was not exceeded.
            if (context.MaxMessageSize > 0 && context.MaxMessageSize < buffer.Count)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingLimitsExceeded,
                    "MaxMessageSize {0} < {1}",
                    context.MaxMessageSize,
                    buffer.Count);
            }

            using (JsonDecoder decoder = new JsonDecoder(UTF8Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count), context))
            {
                return decoder.DecodeMessage(expectedType);
            }
        }

        /// <inheritdoc/>
        public IEncodeable DecodeMessage(System.Type expectedType)
        {
            var namespaceUris = ReadStringArray("NamespaceUris");
            var serverUris = ReadStringArray("ServerUris");

            if ((namespaceUris != null && namespaceUris.Count > 0) || (serverUris != null && serverUris.Count > 0))
            {
                var namespaces = (namespaceUris == null || namespaceUris.Count == 0) ? m_context.NamespaceUris : new NamespaceTable(namespaceUris);
                var servers = (serverUris == null || serverUris.Count == 0) ? m_context.ServerUris : new StringTable(serverUris);

                SetMappingTables(namespaces, servers);
            }

            // read the node id.
            NodeId typeId = ReadNodeId("TypeId");

            // convert to absolute node id.
            ExpandedNodeId absoluteId = NodeId.ToExpandedNodeId(typeId, m_context.NamespaceUris);

            // lookup message type.
            Type actualType = m_context.Factory.GetSystemType(absoluteId);

            if (actualType == null)
            {
                throw new ServiceResultException(StatusCodes.BadDecodingError, Utils.Format("Cannot decode message with type id: {0}.", absoluteId));
            }

            // read the message.
            IEncodeable message = ReadEncodeable("Body", actualType, absoluteId);

            // return the message.
            return message;
        }

        /// <inheritdoc/>
        public void SetMappingTables(NamespaceTable namespaceUris, StringTable serverUris)
        {
            m_namespaceMappings = null;

            if (namespaceUris != null && m_context.NamespaceUris != null)
            {
                ushort[] namespaceMappings = new ushort[namespaceUris.Count];

                for (uint ii = 0; ii < namespaceUris.Count; ii++)
                {
                    var uri = namespaceUris.GetString(ii);

                    if (UpdateNamespaceTable)
                    {
                        namespaceMappings[ii] = m_context.NamespaceUris.GetIndexOrAppend(uri);
                    }
                    else
                    {
                        var index = m_context.NamespaceUris.GetIndex(namespaceUris.GetString(ii));
                        namespaceMappings[ii] = (index >= 0) ? (UInt16)index : UInt16.MaxValue;
                    }
                }

                m_namespaceMappings = namespaceMappings;
            }

            m_serverMappings = null;

            if (serverUris != null && m_context.ServerUris != null)
            {
                ushort[] serverMappings = new ushort[serverUris.Count];

                for (uint ii = 0; ii < serverUris.Count; ii++)
                {
                    var uri = serverUris.GetString(ii);

                    if (UpdateNamespaceTable)
                    {
                        serverMappings[ii] = m_context.ServerUris.GetIndexOrAppend(uri);
                    }
                    else
                    {
                        var index = m_context.ServerUris.GetIndex(serverUris.GetString(ii));
                        serverMappings[ii] = (index >= 0) ? (UInt16)index : UInt16.MaxValue;
                    }
                }

                m_serverMappings = serverMappings;
            }
        }

        /// <inheritdoc/>
        public void Close()
        {
            m_reader.Close();
        }

        /// <summary>
        /// Closes the stream used for reading.
        /// </summary>
        public void Close(bool checkEof)
        {
            if (checkEof && m_reader.TokenType != JsonToken.EndObject)
            {
                while (m_reader.Read() && m_reader.TokenType != JsonToken.EndObject)
                {
                    ;
                }
            }

            m_reader.Close();
        }

        /// <summary>
        /// Reads the body extension object from the stream.
        /// </summary>
        public object ReadExtensionObjectBody(ExpandedNodeId typeId)
        {
            return null;
        }
        #endregion

        #region IDisposable Members
        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Utils.SilentDispose(m_reader);
                m_reader = null;
            }
        }
        #endregion

        #region IDecoder Members
        /// <inheritdoc/>
        public EncodingType EncodingType => EncodingType.Json;

        /// <inheritdoc/>
        public IServiceMessageContext Context => m_context;

        /// <inheritdoc/>
        public void PushNamespace(string namespaceUri)
        {
        }

        /// <inheritdoc/>
        public void PopNamespace()
        {
        }

        /// <inheritdoc/>
        public bool ReadField(string fieldName, out object token)
        {
            token = null;

            if (string.IsNullOrEmpty(fieldName))
            {
                token = m_stack.Peek();
                return true;
            }

            if (!(m_stack.Peek() is Dictionary<string, object> context) || !context.TryGetValue(fieldName, out token))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool ReadBoolean(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return false;
            }

            var value = token as bool?;

            if (value == null)
            {
                return false;
            }

            return (bool)token;
        }

        /// <inheritdoc/>
        public sbyte ReadSByte(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return 0;
            }

            if (value < SByte.MinValue || value > SByte.MaxValue)
            {
                return 0;
            }

            return (sbyte)value;
        }

        /// <inheritdoc/>
        public byte ReadByte(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return 0;
            }

            if (value < Byte.MinValue || value > Byte.MaxValue)
            {
                return 0;
            }

            return (byte)value;
        }

        /// <inheritdoc/>
        public short ReadInt16(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return 0;
            }
;
            if (value < Int16.MinValue || value > Int16.MaxValue)
            {
                return 0;
            }

            return (short)value;
        }

        /// <inheritdoc/>
        public ushort ReadUInt16(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return 0;
            }

            if (value < UInt16.MinValue || value > UInt16.MaxValue)
            {
                return 0;
            }

            return (ushort)value;
        }

        /// <inheritdoc/>
        public int ReadInt32(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return ReadEnumeratedString<Int32>(token, Int32.TryParse);
            }

            if (value < Int32.MinValue || value > Int32.MaxValue)
            {
                return 0;
            }

            return (int)value;
        }

        /// <inheritdoc/>
        public uint ReadUInt32(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                return ReadEnumeratedString<UInt32>(token, UInt32.TryParse);
            }

            if (value < UInt32.MinValue || value > UInt32.MaxValue)
            {
                return 0;
            }

            return (uint)value;
        }

        /// <inheritdoc/>
        public long ReadInt64(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                long number = 0;

                if (!(token is string text) || !Int64.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out number))
                {
                    return 0;
                }

                return number;
            }

            if (value < Int64.MinValue || value > Int64.MaxValue)
            {
                return 0;
            }

            return (long)value;
        }

        /// <inheritdoc/>
        public ulong ReadUInt64(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as long?;

            if (value == null)
            {
                ulong number = 0;

                if (!(token is string text) || !UInt64.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out number))
                {
                    return 0;
                }

                return number;
            }

            if (value < 0)
            {
                return 0;
            }

            return (ulong)value;
        }

        /// <inheritdoc/>
        public float ReadFloat(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as double?;

            if (value == null)
            {
                var text = token as string;
                float number = 0;
                if (text == null || !Single.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                {
                    if (text != null)
                    {
                        if (string.Equals(text, "Infinity", StringComparison.OrdinalIgnoreCase))
                        {
                            return Single.PositiveInfinity;
                        }
                        else if (string.Equals(text, "-Infinity", StringComparison.OrdinalIgnoreCase))
                        {
                            return Single.NegativeInfinity;
                        }
                        else if (string.Equals(text, "NaN", StringComparison.OrdinalIgnoreCase))
                        {
                            return Single.NaN;
                        }
                    }

                    var integer = token as long?;
                    if (integer == null)
                    {
                        return 0;
                    }

                    return (float)integer;
                }

                return number;
            }

            float floatValue = (float)value;
            if (floatValue >= Single.MinValue && floatValue <= Single.MaxValue)
            {
                return (float)value;
            }

            return 0;
        }

        /// <inheritdoc/>
        public double ReadDouble(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return 0;
            }

            var value = token as double?;

            if (value == null)
            {
                var text = token as string;
                double number = 0;

                if (text == null || !Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                {
                    if (text != null)
                    {
                        if (string.Equals(text, "Infinity", StringComparison.OrdinalIgnoreCase))
                        {
                            return Double.PositiveInfinity;
                        }
                        else if (string.Equals(text, "-Infinity", StringComparison.OrdinalIgnoreCase))
                        {
                            return Double.NegativeInfinity;
                        }
                        else if (string.Equals(text, "NaN", StringComparison.OrdinalIgnoreCase))
                        {
                            return Double.NaN;
                        }
                    }

                    var integer = token as long?;

                    if (integer == null)
                    {
                        return 0;
                    }

                    return (double)integer;
                }

                return number;
            }

            return (double)value;
        }

        /// <inheritdoc/>
        public string ReadString(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }


            if (!(token is string value))
            {
                return null;
            }

            if (m_context.MaxStringLength > 0 && m_context.MaxStringLength < value.Length)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            return (string)value;
        }

        /// <inheritdoc/>
        public DateTime ReadDateTime(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return DateTime.MinValue;
            }

            var value = token as DateTime?;
            if (value != null)
            {
                return value.Value >= m_dateTimeMaxJsonValue ? DateTime.MaxValue : value.Value;
            }

            if (token is string text)
            {
                try
                {
                    var result = XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.Utc);
                    return result >= m_dateTimeMaxJsonValue ? DateTime.MaxValue : result;
                }
                catch (FormatException fe)
                {
                    throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Failed to decode DateTime: {0}", fe.Message);
                }
            }

            return DateTime.MinValue;
        }

        /// <inheritdoc/>
        public Uuid ReadGuid(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return Uuid.Empty;
            }

            if (!(token is string value))
            {
                return Uuid.Empty;
            }

            try
            {
                return new Uuid(value);
            }
            catch (FormatException fe)
            {
                throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Failed to create Guid: {0}", fe.Message);
            }
        }

        /// <inheritdoc/>
        public byte[] ReadByteString(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }

            if (token is JTokenNullObject)
            {
                return null;
            }

            if (!(token is string value))
            {
                return Array.Empty<byte>();
            }

            var bytes = SafeConvertFromBase64String(value);

            if (m_context.MaxByteStringLength > 0 && m_context.MaxByteStringLength < bytes.Length)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            return bytes;
        }

        /// <inheritdoc/>
        public XmlElement ReadXmlElement(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }


            if (!(token is string value))
            {
                return null;
            }

            var bytes = SafeConvertFromBase64String(value);

            if (bytes != null && bytes.Length > 0)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    string xmlString = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                    using (XmlReader reader = XmlReader.Create(new StringReader(xmlString), Utils.DefaultXmlReaderSettings()))
                    {
                        document.Load(reader);
                    }

                    return document.DocumentElement;
                }
                catch (XmlException xe)
                {
                    throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Unable to decode Xml: {0}", xe.Message);
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public NodeId ReadNodeId(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return NodeId.Null;
            }

            if (token is string text)
            {
                try
                {
                    var nodeId = NodeId.Parse(
                        m_context,
                        text,
                        new NodeIdParsingOptions() {
                            UpdateTables = UpdateNamespaceTable,
                            NamespaceMappings = m_namespaceMappings,
                            ServerMappings = m_serverMappings
                        });

                    return nodeId;
                }
                catch (ServiceResultException sre) when (sre.StatusCode == StatusCodes.BadNodeIdInvalid)
                {
                    throw ServiceResultException.Create(StatusCodes.BadDecodingError, sre, "Failed to decode NodeId: {0}", sre.Message);
                }
            }

            if (!(token is Dictionary<string, object> value))
            {
                return NodeId.Null;
            }

            IdType idType = IdType.Numeric;
            ushort namespaceIndex = 0;

            try
            {
                m_stack.Push(value);

                if (value.ContainsKey("IdType"))
                {
                    idType = (IdType)ReadInt32("IdType");
                }

                object namespaceToken = null;

                if (ReadField("Namespace", out namespaceToken))
                {
                    var index = namespaceToken as long?;

                    if (index == null)
                    {
                        if (namespaceToken is string namespaceUri)
                        {
                            namespaceIndex = ToNamespaceIndex(namespaceUri);
                        }
                    }
                    else
                    {
                        if (index.Value >= 0 || index.Value < UInt16.MaxValue)
                        {
                            namespaceIndex = ToNamespaceIndex(index.Value);
                        }
                    }
                }

                if (value.ContainsKey("Id"))
                {
                    switch (idType)
                    {
                        case IdType.Numeric:
                        default:
                        {
                            return new NodeId(ReadUInt32("Id"), namespaceIndex);
                        }

                        case IdType.Opaque:
                        {
                            return new NodeId(ReadByteString("Id"), namespaceIndex);
                        }

                        case IdType.String:
                        {
                            return new NodeId(ReadString("Id"), namespaceIndex);
                        }

                        case IdType.Guid:
                        {
                            return new NodeId(ReadGuid("Id"), namespaceIndex);
                        }
                    }
                }
                return DefaultNodeId(idType, namespaceIndex);
            }
            finally
            {
                m_stack.Pop();
            }
        }

        /// <inheritdoc/>
        public ExpandedNodeId ReadExpandedNodeId(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return ExpandedNodeId.Null;
            }

            if (token is string text)
            {
                try
                {
                    var nodeId = ExpandedNodeId.Parse(
                        m_context,
                        text,
                        new NodeIdParsingOptions() {
                            UpdateTables = UpdateNamespaceTable,
                            NamespaceMappings = m_namespaceMappings,
                            ServerMappings = m_serverMappings
                        });

                    return nodeId;
                }
                catch (ServiceResultException sre) when (sre.StatusCode == StatusCodes.BadNodeIdInvalid)
                {
                    throw ServiceResultException.Create(StatusCodes.BadDecodingError, sre, "Failed to decode ExpandedNodeId: {0}", sre.Message);
                }

            }

            if (!(token is Dictionary<string, object> value))
            {
                return ExpandedNodeId.Null;
            }

            IdType idType = IdType.Numeric;
            ushort namespaceIndex = 0;
            string namespaceUri = null;
            uint serverIndex = 0;

            try
            {
                m_stack.Push(value);

                if (value.ContainsKey("IdType"))
                {
                    idType = (IdType)ReadInt32("IdType");
                }

                object namespaceToken = null;

                if (ReadField("Namespace", out namespaceToken))
                {
                    var index = namespaceToken as long?;

                    if (index == null)
                    {
                        namespaceUri = namespaceToken as string;
                    }
                    else
                    {
                        if (index.Value >= 0 || index.Value < UInt16.MaxValue)
                        {
                            namespaceIndex = ToNamespaceIndex(index.Value);
                        }
                    }
                }

                object serverUriToken = null;

                if (ReadField("ServerUri", out serverUriToken))
                {
                    var index = serverUriToken as long?;

                    if (index == null)
                    {
                        serverIndex = ToServerIndex(serverUriToken as string);
                    }
                    else
                    {
                        if (index.Value >= 0 || index.Value < UInt32.MaxValue)
                        {
                            serverIndex = ToServerIndex(index.Value);
                        }
                    }
                }

                if (namespaceUri != null)
                {
                    namespaceIndex = ToNamespaceIndex(namespaceUri);

                    if (UInt16.MaxValue != namespaceIndex)
                    {
                        namespaceUri = null;
                    }
                    else
                    {
                        namespaceIndex = 0;
                    }
                }

                if (value.ContainsKey("Id"))
                {
                    switch (idType)
                    {
                        case IdType.Numeric:
                        default:
                        {
                            return new ExpandedNodeId(ReadUInt32("Id"), namespaceIndex, namespaceUri, serverIndex);
                        }

                        case IdType.Opaque:
                        {
                            return new ExpandedNodeId(ReadByteString("Id"), namespaceIndex, namespaceUri, serverIndex);
                        }

                        case IdType.String:
                        {
                            return new ExpandedNodeId(ReadString("Id"), namespaceIndex, namespaceUri, serverIndex);
                        }

                        case IdType.Guid:
                        {
                            return new ExpandedNodeId(ReadGuid("Id"), namespaceIndex, namespaceUri, serverIndex);
                        }
                    }
                }

                return new ExpandedNodeId(DefaultNodeId(idType, namespaceIndex), namespaceUri, serverIndex);
            }
            finally
            {
                m_stack.Pop();
            }
        }

        /// <inheritdoc/>
        public StatusCode ReadStatusCode(string fieldName)
        {
            object token;

            if (!ReadField(fieldName, out token))
            {
                // the status code was not found
                return StatusCodes.Good;
            }

            if (token is long code)
            {
                return (StatusCode)code;
            }

            bool wasPush = PushStructure(fieldName);

            try
            {
                // try to read the non reversible Code
                if (ReadField("Code", out token))
                {
                    return (StatusCode)ReadUInt32("Code");
                }

                // read the uint code
                return ReadUInt32(null);
            }
            finally
            {
                if (wasPush)
                {
                    Pop();
                }
            }
        }

        /// <inheritdoc/>
        public DiagnosticInfo ReadDiagnosticInfo(string fieldName)
        {
            return ReadDiagnosticInfo(fieldName, 0);
        }

        /// <inheritdoc/>
        public QualifiedName ReadQualifiedName(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return QualifiedName.Null;
            }

            if (token is string text)
            {
                QualifiedName qn = QualifiedName.Parse(m_context, text, UpdateNamespaceTable, true);

                if (qn.NamespaceIndex != 0)
                {
                    var ns = ToNamespaceIndex(qn.NamespaceIndex);

                    if (ns != qn.NamespaceIndex)
                    {
                        qn = new QualifiedName(qn.Name, ns);
                    }
                }

                return qn;
            }

            if (!(token is Dictionary<string, object> value))
            {
                return QualifiedName.Null;
            }

            UInt16 namespaceIndex = 0;
            string name = null;
            try
            {
                m_stack.Push(value);

                if (value.ContainsKey("Name"))
                {
                    name = ReadString("Name");
                }

                object namespaceToken = null;

                if (ReadField("Uri", out namespaceToken))
                {
                    var index = namespaceToken as long?;

                    if (index == null)
                    {
                        if (namespaceToken is string namespaceUri)
                        {
                            namespaceIndex = ToNamespaceIndex(namespaceUri);
                        }
                    }
                    else
                    {
                        if (index.Value >= 0 || index.Value < UInt16.MaxValue)
                        {
                            namespaceIndex = ToNamespaceIndex(index.Value);
                        }
                    }
                }
            }
            finally
            {
                m_stack.Pop();
            }

            return new QualifiedName(name, namespaceIndex);
        }

        /// <inheritdoc/>
        public LocalizedText ReadLocalizedText(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return LocalizedText.Null;
            }

            string locale = null;
            string text = null;


            if (!(token is Dictionary<string, object> value))
            {
                // read non reversible encoding
                text = token as string;

                if (text != null)
                {
                    return new LocalizedText(text);
                }

                return LocalizedText.Null;
            }

            try
            {
                m_stack.Push(value);

                if (value.ContainsKey("Locale"))
                {
                    locale = ReadString("Locale");
                }

                if (value.ContainsKey("Text"))
                {
                    text = ReadString("Text");
                }
            }
            finally
            {
                m_stack.Pop();
            }

            return new LocalizedText(locale, text);
        }

        /// <inheritdoc/>
        public Variant ReadVariant(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return Variant.Null;
            }


            if (!(token is Dictionary<string, object> value))
            {
                return Variant.Null;
            }

            CheckAndIncrementNestingLevel();

            try
            {
                m_stack.Push(value);

                BuiltInType type = (BuiltInType)ReadByte("Type");

                var context = m_stack.Peek() as Dictionary<string, object>;

                if (!context.TryGetValue("Body", out token))
                {
                    return Variant.Null;
                }

                Variant array;
                if (token is Array)
                {
                    array = ReadVariantBody("Body", type);
                }
                else if (token is List<object>)
                {
                    array = ReadVariantArrayBody("Body", type);
                }
                else
                {
                    return ReadVariantBody("Body", type);
                }
                Int32Collection dimensions = ReadInt32Array("Dimensions");

                if (array.Value is Array arrayValue && dimensions != null && dimensions.Count > 1)
                {
                    int length = arrayValue.Length;
                    var dimensionsArray = dimensions.ToArray();
                    (bool valid, int matrixLength) = Matrix.ValidateDimensions(dimensionsArray, length, Context.MaxArrayLength);

                    if (!valid || (matrixLength != length))
                    {
                        throw ServiceResultException.Create(StatusCodes.BadDecodingError,
                            "ArrayDimensions length does not match with the ArrayLength in Variant object.");
                    }

                    array = new Variant(new Matrix(arrayValue, type, dimensionsArray));
                }

                return array;
            }
            finally
            {
                m_nestingLevel--;
                m_stack.Pop();
            }
        }

        /// <inheritdoc/>
        public DataValue ReadDataValue(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }


            if (!(token is Dictionary<string, object> value))
            {
                return null;
            }

            DataValue dv = new DataValue();

            try
            {
                m_stack.Push(value);

                dv.WrappedValue = ReadVariant("Value");
                dv.StatusCode = ReadStatusCode("StatusCode");
                dv.SourceTimestamp = ReadDateTime("SourceTimestamp");
                dv.SourcePicoseconds = ReadUInt16("SourcePicoseconds");
                dv.ServerTimestamp = ReadDateTime("ServerTimestamp");
                dv.ServerPicoseconds = ReadUInt16("ServerPicoseconds");
            }
            finally
            {
                m_stack.Pop();
            }

            return dv;
        }

        /// <inheritdoc/>
        public ExtensionObject ReadExtensionObject(string fieldName)
        {
            var extension = ExtensionObject.Null;
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return extension;
            }

            if (!(token is Dictionary<string, object> value))
            {
                return extension;
            }

            try
            {
                m_stack.Push(value);

                ExpandedNodeId typeId = ReadExpandedNodeId("TypeId");
                ExpandedNodeId absoluteId =
                    typeId.IsAbsolute ?
                    typeId :
                    NodeId.ToExpandedNodeId(typeId.InnerNodeId, m_context.NamespaceUris);

                if (!NodeId.IsNull(typeId) && NodeId.IsNull(absoluteId))
                {
                    Utils.LogWarning("Cannot de-serialized extension objects if the NamespaceUri is not in the NamespaceTable: Type = {0}", typeId);
                }
                else
                {
                    typeId = absoluteId;
                }

                byte encoding = ReadByte("Encoding");

                if (encoding == (byte)ExtensionObjectEncoding.Binary)
                {
                    var bytes = ReadByteString("Body");
                    return new ExtensionObject(typeId, bytes ?? Array.Empty<byte>());
                }

                if (encoding == (byte)ExtensionObjectEncoding.Xml)
                {
                    var xml = ReadXmlElement("Body");
                    if (xml == null)
                    {
                        return extension;
                    }
                    return new ExtensionObject(typeId, xml);
                }

                if (encoding == (byte)ExtensionObjectEncoding.Json)
                {
                    var json = ReadString("Body");
                    if (string.IsNullOrEmpty(json))
                    {
                        return extension;
                    }
                    return new ExtensionObject(typeId, json);
                }

                Type systemType = m_context.Factory.GetSystemType(typeId);

                if (systemType != null)
                {
                    var encodeable = ReadEncodeable("Body", systemType, typeId);
                    if (encodeable == null)
                    {
                        return extension;
                    }

                    return new ExtensionObject(typeId, encodeable);
                }

                using (var ostrm = new MemoryStream())
                {
                    using (var stream = new StreamWriter(ostrm))
                    using (JsonTextWriter writer = new JsonTextWriter(stream))
                    {
                        EncodeAsJson(writer, token);
                    }
                    // Close the writer before retrieving the data
                    return new ExtensionObject(typeId, ostrm.ToArray());
                }
            }
            finally
            {
                m_stack.Pop();
            }
        }

        /// <inheritdoc/>
        public IEncodeable ReadEncodeable(string fieldName, System.Type systemType, ExpandedNodeId encodeableTypeId = null)
        {
            if (systemType == null)
            {
                throw new ArgumentNullException(nameof(systemType));
            }

            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }

            if (!(Activator.CreateInstance(systemType) is IEncodeable value))
            {
                throw new ServiceResultException(StatusCodes.BadDecodingError, Utils.Format("Type does not support IEncodeable interface: '{0}'", systemType.FullName));
            }

            if (encodeableTypeId != null)
            {
                // set type identifier for custom complex data types before decode.
                if (value is IComplexTypeInstance complexTypeInstance)
                {
                    complexTypeInstance.TypeId = encodeableTypeId;
                }
            }

            CheckAndIncrementNestingLevel();

            try
            {
                m_stack.Push(token);
                value.Decode(this);
            }
            finally
            {
                m_stack.Pop();
                m_nestingLevel--;
            }

            return value;
        }

        /// <inheritdoc/>
        public Enum ReadEnumerated(string fieldName, System.Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            object token;

            if (!ReadField(fieldName, out token))
            {
                return (Enum)Enum.ToObject(enumType, 0);
            }

            if (token is long code)
            {
                return (Enum)Enum.ToObject(enumType, code);
            }

            if (token is string text)
            {
                int index = text.LastIndexOf('_');

                if (index > 0 && long.TryParse(text.Substring(index + 1), out code))
                {
                    return (Enum)Enum.ToObject(enumType, code);
                }
            }

            return (Enum)Enum.ToObject(enumType, 0);
        }

        /// <inheritdoc/>
        public BooleanCollection ReadBooleanArray(string fieldName)
        {
            var values = new BooleanCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadBoolean(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public SByteCollection ReadSByteArray(string fieldName)
        {
            var values = new SByteCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadSByte(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public ByteCollection ReadByteArray(string fieldName)
        {
            var values = new ByteCollection();

            List<object> token = null;


            string value = ReadString(fieldName);
            if (value != null)
            {
                return SafeConvertFromBase64String(value);
            }

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadByte(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Int16Collection ReadInt16Array(string fieldName)
        {
            var values = new Int16Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadInt16(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public UInt16Collection ReadUInt16Array(string fieldName)
        {
            var values = new UInt16Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadUInt16(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Int32Collection ReadInt32Array(string fieldName)
        {
            var values = new Int32Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadInt32(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public UInt32Collection ReadUInt32Array(string fieldName)
        {
            var values = new UInt32Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadUInt32(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Int64Collection ReadInt64Array(string fieldName)
        {
            var values = new Int64Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadInt64(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public UInt64Collection ReadUInt64Array(string fieldName)
        {
            var values = new UInt64Collection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadUInt64(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public FloatCollection ReadFloatArray(string fieldName)
        {
            var values = new FloatCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadFloat(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public DoubleCollection ReadDoubleArray(string fieldName)
        {
            var values = new DoubleCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadDouble(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public StringCollection ReadStringArray(string fieldName)
        {
            var values = new StringCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadString(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public DateTimeCollection ReadDateTimeArray(string fieldName)
        {
            var values = new DateTimeCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    values.Add(ReadDateTime(null));
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public UuidCollection ReadGuidArray(string fieldName)
        {
            var values = new UuidCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadGuid(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public ByteStringCollection ReadByteStringArray(string fieldName)
        {
            var values = new ByteStringCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadByteString(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public XmlElementCollection ReadXmlElementArray(string fieldName)
        {
            var values = new XmlElementCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadXmlElement(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public NodeIdCollection ReadNodeIdArray(string fieldName)
        {
            var values = new NodeIdCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadNodeId(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public ExpandedNodeIdCollection ReadExpandedNodeIdArray(string fieldName)
        {
            var values = new ExpandedNodeIdCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadExpandedNodeId(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public StatusCodeCollection ReadStatusCodeArray(string fieldName)
        {
            var values = new StatusCodeCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadStatusCode(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public DiagnosticInfoCollection ReadDiagnosticInfoArray(string fieldName)
        {
            var values = new DiagnosticInfoCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadDiagnosticInfo(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public QualifiedNameCollection ReadQualifiedNameArray(string fieldName)
        {
            var values = new QualifiedNameCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadQualifiedName(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public LocalizedTextCollection ReadLocalizedTextArray(string fieldName)
        {
            var values = new LocalizedTextCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadLocalizedText(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public VariantCollection ReadVariantArray(string fieldName)
        {
            var values = new VariantCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadVariant(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public DataValueCollection ReadDataValueArray(string fieldName)
        {
            var values = new DataValueCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadDataValue(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public ExtensionObjectCollection ReadExtensionObjectArray(string fieldName)
        {
            var values = new ExtensionObjectCollection();

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return values;
            }

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadExtensionObject(null);
                    values.Add(element);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Array ReadEncodeableArray(string fieldName, System.Type systemType, ExpandedNodeId encodeableTypeId = null)
        {
            if (systemType == null)
            {
                throw new ArgumentNullException(nameof(systemType));
            }

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return Array.CreateInstance(systemType, 0);
            }

            var values = Array.CreateInstance(systemType, token.Count);

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadEncodeable(null, systemType, encodeableTypeId);
                    values.SetValue(element, ii);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Array ReadEnumeratedArray(string fieldName, System.Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return Array.CreateInstance(enumType, 0);
            }

            var values = Array.CreateInstance(enumType, token.Count);

            for (int ii = 0; ii < token.Count; ii++)
            {
                try
                {
                    m_stack.Push(token[ii]);
                    var element = ReadEnumerated(null, enumType);
                    values.SetValue(element, ii);
                }
                finally
                {
                    m_stack.Pop();
                }
            }

            return values;
        }

        /// <inheritdoc/>
        public Array ReadArray(
            string fieldName,
            int valueRank,
            BuiltInType builtInType,
            Type systemType = null,
            ExpandedNodeId encodeableTypeId = null)
        {
            if (valueRank == ValueRanks.OneDimension)
            {
                switch (builtInType)
                {
                    case BuiltInType.Boolean:
                        return ReadBooleanArray(fieldName).ToArray();
                    case BuiltInType.SByte:
                        return ReadSByteArray(fieldName).ToArray();
                    case BuiltInType.Byte:
                        return ReadByteArray(fieldName).ToArray();
                    case BuiltInType.Int16:
                        return ReadInt16Array(fieldName).ToArray();
                    case BuiltInType.UInt16:
                        return ReadUInt16Array(fieldName).ToArray();
                    case BuiltInType.Enumeration:
                    {
                        DetermineIEncodeableSystemType(ref systemType, encodeableTypeId);
                        if (systemType?.IsEnum == true)
                        {
                            return ReadEnumeratedArray(fieldName, systemType);
                        }
                        goto case BuiltInType.Int32;
                    }
                    case BuiltInType.Int32:
                        return ReadInt32Array(fieldName).ToArray();
                    case BuiltInType.UInt32:
                        return ReadUInt32Array(fieldName).ToArray();
                    case BuiltInType.Int64:
                        return ReadInt64Array(fieldName).ToArray();
                    case BuiltInType.UInt64:
                        return ReadUInt64Array(fieldName).ToArray();
                    case BuiltInType.Float:
                        return ReadFloatArray(fieldName).ToArray();
                    case BuiltInType.Double:
                        return ReadDoubleArray(fieldName).ToArray();
                    case BuiltInType.String:
                        return ReadStringArray(fieldName).ToArray();
                    case BuiltInType.DateTime:
                        return ReadDateTimeArray(fieldName).ToArray();
                    case BuiltInType.Guid:
                        return ReadGuidArray(fieldName).ToArray();
                    case BuiltInType.ByteString:
                        return ReadByteStringArray(fieldName).ToArray();
                    case BuiltInType.XmlElement:
                        return ReadXmlElementArray(fieldName).ToArray();
                    case BuiltInType.NodeId:
                        return ReadNodeIdArray(fieldName).ToArray();
                    case BuiltInType.ExpandedNodeId:
                        return ReadExpandedNodeIdArray(fieldName).ToArray();
                    case BuiltInType.StatusCode:
                        return ReadStatusCodeArray(fieldName).ToArray();
                    case BuiltInType.QualifiedName:
                        return ReadQualifiedNameArray(fieldName).ToArray();
                    case BuiltInType.LocalizedText:
                        return ReadLocalizedTextArray(fieldName).ToArray();
                    case BuiltInType.DataValue:
                        return ReadDataValueArray(fieldName).ToArray();
                    case BuiltInType.Variant:
                    {
                        if (DetermineIEncodeableSystemType(ref systemType, encodeableTypeId))
                        {
                            return ReadEncodeableArray(fieldName, systemType, encodeableTypeId);
                        }
                        return ReadVariantArray(fieldName).ToArray();
                    }
                    case BuiltInType.ExtensionObject:
                        return ReadExtensionObjectArray(fieldName).ToArray();
                    case BuiltInType.DiagnosticInfo:
                        return ReadDiagnosticInfoArray(fieldName).ToArray();
                    default:
                    {
                        if (DetermineIEncodeableSystemType(ref systemType, encodeableTypeId))
                        {
                            return ReadEncodeableArray(fieldName, systemType, encodeableTypeId);
                        }

                        throw new ServiceResultException(
                            StatusCodes.BadDecodingError,
                            Utils.Format("Cannot decode unknown type in Array object with BuiltInType: {0}.", builtInType));
                    }
                }
            }
            else if (valueRank >= ValueRanks.TwoDimensions)
            {
                object token = null;

                if (!ReadField(fieldName, out token))
                {
                    return null;
                }

                if (token is Dictionary<string, object> value)
                {
                    m_stack.Push(value);
                    Int32Collection dimensions2 = null;

                    if (value.ContainsKey("Dimensions"))
                    {
                        dimensions2 = ReadInt32Array("Dimensions");
                    }
                    else
                    {
                        dimensions2 = new Int32Collection(valueRank);
                    }

                    var array2 = ReadArray("Array", 1, builtInType, systemType, encodeableTypeId);
                    m_stack.Pop();

                    var matrix2 = new Matrix(array2, builtInType, dimensions2.ToArray());
                    return matrix2.ToArray();
                }

                if (!(token is List<object> array))
                {
                    return null;
                }

                List<object> elements = new List<object>();
                List<int> dimensions = new List<int>();
                if (builtInType == BuiltInType.Enumeration || builtInType == BuiltInType.Variant || builtInType == BuiltInType.Null)
                {
                    DetermineIEncodeableSystemType(ref systemType, encodeableTypeId);
                }
                ReadMatrixPart(fieldName, array, builtInType, ref elements, ref dimensions, 0, systemType, encodeableTypeId);

                if (dimensions.Count == 0)
                {
                    // for an empty element create the empty dimension array 
                    dimensions = new int[valueRank].ToList();
                }
                else if (dimensions.Count < ValueRanks.TwoDimensions)
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadDecodingError,
                        "The ValueRank {0} of the decoded array doesn't match the desired ValueRank {1}.",
                        dimensions.Count, valueRank);
                }

                Matrix matrix = null;
                switch (builtInType)
                {
                    case BuiltInType.Boolean:
                        matrix = new Matrix(elements.Cast<bool>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.SByte:
                        matrix = new Matrix(elements.Cast<sbyte>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Byte:
                        matrix = new Matrix(elements.Cast<byte>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Int16:
                        matrix = new Matrix(elements.Cast<Int16>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.UInt16:
                        matrix = new Matrix(elements.Cast<UInt16>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Int32:
                        matrix = new Matrix(elements.Cast<Int32>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.UInt32:
                        matrix = new Matrix(elements.Cast<UInt32>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Int64:
                        matrix = new Matrix(elements.Cast<Int64>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.UInt64:
                        matrix = new Matrix(elements.Cast<UInt64>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Float:
                        matrix = new Matrix(elements.Cast<float>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Double:
                        matrix = new Matrix(elements.Cast<double>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.String:
                        matrix = new Matrix(elements.Cast<string>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.DateTime:
                        matrix = new Matrix(elements.Cast<DateTime>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Guid:
                        matrix = new Matrix(elements.Cast<Uuid>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.ByteString:
                        matrix = new Matrix(elements.Cast<byte[]>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.XmlElement:
                        matrix = new Matrix(elements.Cast<XmlElement>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.NodeId:
                        matrix = new Matrix(elements.Cast<NodeId>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.ExpandedNodeId:
                        matrix = new Matrix(elements.Cast<ExpandedNodeId>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.StatusCode:
                        matrix = new Matrix(elements.Cast<StatusCode>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.QualifiedName:
                        matrix = new Matrix(elements.Cast<QualifiedName>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.LocalizedText:
                        matrix = new Matrix(elements.Cast<LocalizedText>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.DataValue:
                        matrix = new Matrix(elements.Cast<DataValue>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.Enumeration:
                    {
                        if (systemType?.IsEnum == true)
                        {
                            var newElements = Array.CreateInstance(systemType, elements.Count);
                            int ii = 0;
                            foreach (var element in elements)
                            {
                                newElements.SetValue(Convert.ChangeType(element, systemType, CultureInfo.InvariantCulture), ii++);
                            }
                            matrix = new Matrix(newElements, builtInType, dimensions.ToArray());
                        }
                        else
                        {
                            matrix = new Matrix(elements.Cast<Int32>().ToArray(), builtInType, dimensions.ToArray());
                        }
                        break;
                    }
                    case BuiltInType.Variant:
                    {
                        if (DetermineIEncodeableSystemType(ref systemType, encodeableTypeId))
                        {
                            Array newElements = Array.CreateInstance(systemType, elements.Count);
                            for (int i = 0; i < elements.Count; i++)
                            {
                                newElements.SetValue(Convert.ChangeType(elements[i], systemType, CultureInfo.InvariantCulture), i);
                            }
                            matrix = new Matrix(newElements, builtInType, dimensions.ToArray());
                            break;
                        }
                        matrix = new Matrix(elements.Cast<Variant>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    }
                    case BuiltInType.ExtensionObject:
                        matrix = new Matrix(elements.Cast<ExtensionObject>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    case BuiltInType.DiagnosticInfo:
                        matrix = new Matrix(elements.Cast<DiagnosticInfo>().ToArray(), builtInType, dimensions.ToArray());
                        break;
                    default:
                    {
                        if (DetermineIEncodeableSystemType(ref systemType, encodeableTypeId))
                        {
                            Array newElements = Array.CreateInstance(systemType, elements.Count);
                            for (int i = 0; i < elements.Count; i++)
                            {
                                newElements.SetValue(Convert.ChangeType(elements[i], systemType, CultureInfo.InvariantCulture), i);
                            }
                            matrix = new Matrix(newElements, builtInType, dimensions.ToArray());
                            break;
                        }

                        throw ServiceResultException.Create(
                            StatusCodes.BadDecodingError,
                            "Cannot decode unknown type in Array object with BuiltInType: {0}.",
                            builtInType);
                    }
                }

                return matrix.ToArray();
            }
            return null;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public bool PushStructure(string fieldName)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return false;
            }

            if (token != null)
            {
                m_stack.Push(token);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool PushArray(string fieldName, int index)
        {
            List<object> token = null;

            if (!ReadArrayField(fieldName, out token))
            {
                return false;
            }

            if (index < token.Count)
            {
                m_stack.Push(token[index]);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void Pop()
        {
            m_stack.Pop();
        }
        #endregion

        #region Private Methods
        private ushort ToNamespaceIndex(string uri)
        {
            var index = m_context.NamespaceUris.GetIndex(uri);

            if (index < 0)
            {
                if (!UpdateNamespaceTable)
                {
                    return UInt16.MaxValue;
                }
                else
                {
                    index = m_context.NamespaceUris.GetIndexOrAppend(uri);
                }
            }

            return (ushort)index;
        }

        private ushort ToNamespaceIndex(long index)
        {
            if (m_namespaceMappings == null || index <= 0)
            {
                return (ushort)index;
            }

            if (index < 0 || index >= m_namespaceMappings.Length)
            {
                throw new ServiceResultException(StatusCodes.BadDecodingError, $"No mapping for NamespaceIndex={index}.");
            }

            return m_namespaceMappings[index];
        }

        private ushort ToServerIndex(string uri)
        {
            var index = m_context.ServerUris.GetIndex(uri);

            if (index < 0)
            {
                if (!UpdateNamespaceTable)
                {
                    return UInt16.MaxValue;
                }
                else
                {
                    index = m_context.ServerUris.GetIndexOrAppend(uri);
                }
            }

            return (ushort)index;
        }

        private ushort ToServerIndex(long index)
        {
            if (m_serverMappings == null || index <= 0)
            {
                return (ushort)index;
            }

            if (index < 0 || index >= m_serverMappings.Length)
            {
                throw new ServiceResultException(StatusCodes.BadDecodingError, $"No mapping for ServerIndex(={index}.");
            }

            return m_serverMappings[index];
        }

        /// <summary>
        /// Helper to provide the TryParse method when reading an enumerated string.
        /// </summary>
        private delegate bool TryParseHandler<T>(string s, NumberStyles numberStyles, CultureInfo cultureInfo, out T result);

        /// <summary>
        /// Helper to read an enumerated string in an extension object.
        /// </summary>
        /// <typeparam name="T">The number type which was encoded.</typeparam>
        /// <param name="token"></param>
        /// <param name="handler"></param>
        /// <returns>The parsed number or 0.</returns>
        private T ReadEnumeratedString<T>(object token, TryParseHandler<T> handler) where T : struct
        {
            T number = default;
            if (token is string text)
            {
                bool retry = false;
                do
                {
                    if (handler?.Invoke(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out number) == false)
                    {
                        int lastIndex = text.LastIndexOf('_');
                        if (lastIndex != -1)
                        {
                            text = text.Substring(lastIndex + 1);
                            retry = true;
                        }
                    }
                } while (retry);
            }

            return number;
        }

        /// <summary>
        /// Reads a DiagnosticInfo from the stream.
        /// Limits the InnerDiagnosticInfos to the specified depth.
        /// </summary>
        private DiagnosticInfo ReadDiagnosticInfo(string fieldName, int depth)
        {
            object token = null;

            if (!ReadField(fieldName, out token))
            {
                return null;
            }


            if (!(token is Dictionary<string, object> value))
            {
                return null;
            }

            if (depth >= DiagnosticInfo.MaxInnerDepth)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingLimitsExceeded,
                    "Maximum nesting level of InnerDiagnosticInfo was exceeded");
            }

            CheckAndIncrementNestingLevel();

            try
            {
                m_stack.Push(value);

                DiagnosticInfo di = new DiagnosticInfo();

                bool hasDiagnosticInfo = false;
                if (value.ContainsKey("SymbolicId"))
                {
                    di.SymbolicId = ReadInt32("SymbolicId");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("NamespaceUri"))
                {
                    di.NamespaceUri = ReadInt32("NamespaceUri");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("Locale"))
                {
                    di.Locale = ReadInt32("Locale");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("LocalizedText"))
                {
                    di.LocalizedText = ReadInt32("LocalizedText");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("AdditionalInfo"))
                {
                    di.AdditionalInfo = ReadString("AdditionalInfo");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("InnerStatusCode"))
                {
                    di.InnerStatusCode = ReadStatusCode("InnerStatusCode");
                    hasDiagnosticInfo = true;
                }

                if (value.ContainsKey("InnerDiagnosticInfo") && depth < DiagnosticInfo.MaxInnerDepth)
                {
                    di.InnerDiagnosticInfo = ReadDiagnosticInfo("InnerDiagnosticInfo", depth + 1);
                    hasDiagnosticInfo = true;
                }

                return hasDiagnosticInfo ? di : null;
            }
            finally
            {
                m_nestingLevel--;
                m_stack.Pop();
            }
        }

        /// <summary>
        /// Get the system type from the type factory if not specified by caller.
        /// </summary>
        /// <param name="systemType">The reference to the system type, or null</param>
        /// <param name="encodeableTypeId">The encodeable type id of the system type.</param>
        /// <returns>If the system type is assignable to <see cref="IEncodeable"/> </returns>
        private bool DetermineIEncodeableSystemType(ref Type systemType, ExpandedNodeId encodeableTypeId)
        {
            if (encodeableTypeId != null && systemType == null)
            {
                systemType = Context.Factory.GetSystemType(encodeableTypeId);
            }
            return typeof(IEncodeable).IsAssignableFrom(systemType);
        }

        /// <summary>
        /// Read the body of a Variant as a BuiltInType
        /// </summary>
        private Variant ReadVariantBody(string fieldName, BuiltInType type)
        {
            switch (type)
            {
                case BuiltInType.Boolean: { return new Variant(ReadBoolean(fieldName), TypeInfo.Scalars.Boolean); }
                case BuiltInType.SByte: { return new Variant(ReadSByte(fieldName), TypeInfo.Scalars.SByte); }
                case BuiltInType.Byte: { return new Variant(ReadByte(fieldName), TypeInfo.Scalars.Byte); }
                case BuiltInType.Int16: { return new Variant(ReadInt16(fieldName), TypeInfo.Scalars.Int16); }
                case BuiltInType.UInt16: { return new Variant(ReadUInt16(fieldName), TypeInfo.Scalars.UInt16); }
                case BuiltInType.Int32: { return new Variant(ReadInt32(fieldName), TypeInfo.Scalars.Int32); }
                case BuiltInType.UInt32: { return new Variant(ReadUInt32(fieldName), TypeInfo.Scalars.UInt32); }
                case BuiltInType.Int64: { return new Variant(ReadInt64(fieldName), TypeInfo.Scalars.Int64); }
                case BuiltInType.UInt64: { return new Variant(ReadUInt64(fieldName), TypeInfo.Scalars.UInt64); }
                case BuiltInType.Float: { return new Variant(ReadFloat(fieldName), TypeInfo.Scalars.Float); }
                case BuiltInType.Double: { return new Variant(ReadDouble(fieldName), TypeInfo.Scalars.Double); }
                case BuiltInType.String: { return new Variant(ReadString(fieldName), TypeInfo.Scalars.String); }
                case BuiltInType.ByteString: { return new Variant(ReadByteString(fieldName), TypeInfo.Scalars.ByteString); }
                case BuiltInType.DateTime: { return new Variant(ReadDateTime(fieldName), TypeInfo.Scalars.DateTime); }
                case BuiltInType.Guid: { return new Variant(ReadGuid(fieldName), TypeInfo.Scalars.Guid); }
                case BuiltInType.NodeId: { return new Variant(ReadNodeId(fieldName), TypeInfo.Scalars.NodeId); }
                case BuiltInType.ExpandedNodeId: { return new Variant(ReadExpandedNodeId(fieldName), TypeInfo.Scalars.ExpandedNodeId); }
                case BuiltInType.QualifiedName: { return new Variant(ReadQualifiedName(fieldName), TypeInfo.Scalars.QualifiedName); }
                case BuiltInType.LocalizedText: { return new Variant(ReadLocalizedText(fieldName), TypeInfo.Scalars.LocalizedText); }
                case BuiltInType.StatusCode: { return new Variant(ReadStatusCode(fieldName), TypeInfo.Scalars.StatusCode); }
                case BuiltInType.XmlElement: { return new Variant(ReadXmlElement(fieldName), TypeInfo.Scalars.XmlElement); }
                case BuiltInType.ExtensionObject: { return new Variant(ReadExtensionObject(fieldName), TypeInfo.Scalars.ExtensionObject); }
                case BuiltInType.Variant: { return new Variant(ReadVariant(fieldName), TypeInfo.Scalars.Variant); }
                case BuiltInType.DiagnosticInfo: { return new Variant(ReadDiagnosticInfo(fieldName), TypeInfo.Scalars.DiagnosticInfo); }
                case BuiltInType.DataValue: { return new Variant(ReadDataValue(fieldName), TypeInfo.Scalars.DataValue); }
            }

            return Variant.Null;
        }

        /// <summary>
        /// Read the Body of a Variant as an Array
        /// </summary>
        private Variant ReadVariantArrayBody(string fieldName, BuiltInType type)
        {
            switch (type)
            {
                case BuiltInType.Boolean: { return new Variant(ReadBooleanArray(fieldName), TypeInfo.Arrays.Boolean); }
                case BuiltInType.SByte: { return new Variant(ReadSByteArray(fieldName), TypeInfo.Arrays.SByte); }
                case BuiltInType.Byte: { return new Variant(ReadByteArray(fieldName), TypeInfo.Arrays.Byte); }
                case BuiltInType.Int16: { return new Variant(ReadInt16Array(fieldName), TypeInfo.Arrays.Int16); }
                case BuiltInType.UInt16: { return new Variant(ReadUInt16Array(fieldName), TypeInfo.Arrays.UInt16); }
                case BuiltInType.Int32: { return new Variant(ReadInt32Array(fieldName), TypeInfo.Arrays.Int32); }
                case BuiltInType.UInt32: { return new Variant(ReadUInt32Array(fieldName), TypeInfo.Arrays.UInt32); }
                case BuiltInType.Int64: { return new Variant(ReadInt64Array(fieldName), TypeInfo.Arrays.Int64); }
                case BuiltInType.UInt64: { return new Variant(ReadUInt64Array(fieldName), TypeInfo.Arrays.UInt64); }
                case BuiltInType.Float: { return new Variant(ReadFloatArray(fieldName), TypeInfo.Arrays.Float); }
                case BuiltInType.Double: { return new Variant(ReadDoubleArray(fieldName), TypeInfo.Arrays.Double); }
                case BuiltInType.String: { return new Variant(ReadStringArray(fieldName), TypeInfo.Arrays.String); }
                case BuiltInType.ByteString: { return new Variant(ReadByteStringArray(fieldName), TypeInfo.Arrays.ByteString); }
                case BuiltInType.DateTime: { return new Variant(ReadDateTimeArray(fieldName), TypeInfo.Arrays.DateTime); }
                case BuiltInType.Guid: { return new Variant(ReadGuidArray(fieldName), TypeInfo.Arrays.Guid); }
                case BuiltInType.NodeId: { return new Variant(ReadNodeIdArray(fieldName), TypeInfo.Arrays.NodeId); }
                case BuiltInType.ExpandedNodeId: { return new Variant(ReadExpandedNodeIdArray(fieldName), TypeInfo.Arrays.ExpandedNodeId); }
                case BuiltInType.QualifiedName: { return new Variant(ReadQualifiedNameArray(fieldName), TypeInfo.Arrays.QualifiedName); }
                case BuiltInType.LocalizedText: { return new Variant(ReadLocalizedTextArray(fieldName), TypeInfo.Arrays.LocalizedText); }
                case BuiltInType.StatusCode: { return new Variant(ReadStatusCodeArray(fieldName), TypeInfo.Arrays.StatusCode); }
                case BuiltInType.XmlElement: { return new Variant(ReadXmlElementArray(fieldName), TypeInfo.Arrays.XmlElement); }
                case BuiltInType.ExtensionObject: { return new Variant(ReadExtensionObjectArray(fieldName), TypeInfo.Arrays.ExtensionObject); }
                case BuiltInType.Variant: { return new Variant(ReadVariantArray(fieldName), TypeInfo.Arrays.Variant); }
                case BuiltInType.DiagnosticInfo: { return new Variant(ReadDiagnosticInfoArray(fieldName), TypeInfo.Arrays.DiagnosticInfo); }
                case BuiltInType.DataValue: { return new Variant(ReadDataValueArray(fieldName), TypeInfo.Arrays.DataValue); }
            }

            return Variant.Null;
        }

        /// <summary>
        /// Reads the content of an Array from json stream
        /// </summary>
        private List<object> ReadArray()
        {
            CheckAndIncrementNestingLevel();

            try
            {
                List<object> elements = new List<object>();

                while (m_reader.Read() && m_reader.TokenType != JsonToken.EndArray)
                {
                    switch (m_reader.TokenType)
                    {
                        case JsonToken.Comment:
                        {
                            break;
                        }

                        case JsonToken.Null:
                        {
                            elements.Add(JTokenNullObject.Array);
                            break;
                        }
                        case JsonToken.Date:
                        case JsonToken.Boolean:
                        case JsonToken.Integer:
                        case JsonToken.Float:
                        case JsonToken.String:
                        {
                            elements.Add(m_reader.Value);
                            break;
                        }

                        case JsonToken.StartArray:
                        {
                            elements.Add(ReadArray());
                            break;
                        }

                        case JsonToken.StartObject:
                        {
                            elements.Add(ReadObject());
                            break;
                        }

                        default:
                            break;
                    }
                }

                return elements;
            }
            finally
            {
                m_nestingLevel--;
            }
        }

        /// <summary>
        /// Reads an object from the json stream
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> ReadObject()
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            try
            {
                while (m_reader.Read() && m_reader.TokenType != JsonToken.EndObject)
                {
                    if (m_reader.TokenType == JsonToken.StartArray)
                    {
                        fields[RootArrayName] = ReadArray();
                    }
                    else if (m_reader.TokenType == JsonToken.PropertyName)
                    {
                        string name = (string)m_reader.Value;

                        if (m_reader.Read() && m_reader.TokenType != JsonToken.EndObject)
                        {
                            switch (m_reader.TokenType)
                            {
                                case JsonToken.Comment:
                                {
                                    break;
                                }

                                case JsonToken.Null:
                                {
                                    fields[name] = JTokenNullObject.Object;
                                    break;
                                }

                                case JsonToken.Date:
                                case JsonToken.Bytes:
                                case JsonToken.Boolean:
                                case JsonToken.Integer:
                                case JsonToken.Float:
                                case JsonToken.String:
                                {
                                    fields[name] = m_reader.Value;
                                    break;
                                }

                                case JsonToken.StartArray:
                                {
                                    fields[name] = ReadArray();
                                    break;
                                }

                                case JsonToken.StartObject:
                                {
                                    fields[name] = ReadObject();
                                    break;
                                }

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (JsonReaderException jre)
            {
                throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Error reading JSON object: {0}", jre.Message);
            }
            return fields;
        }

        /// <summary>
        /// Read the Matrix part (simple array or array of arrays)
        /// </summary>
        private void ReadMatrixPart(
            string fieldName,
            List<object> currentArray,
            BuiltInType builtInType,
            ref List<object> elements,
            ref List<int> dimensions,
            int level,
            Type systemType,
            ExpandedNodeId encodeableTypeId)
        {
            CheckAndIncrementNestingLevel();

            try
            {
                if (currentArray?.Count > 0)
                {
                    bool hasInnerArray = false;
                    for (int ii = 0; ii < currentArray.Count; ii++)
                    {
                        if (ii == 0 && dimensions.Count <= level)
                        {
                            // remember dimension length
                            dimensions.Add(currentArray.Count);
                        }
                        if (currentArray[ii] is List<object>)
                        {
                            hasInnerArray = true;

                            PushArray(fieldName, ii);

                            ReadMatrixPart(null, currentArray[ii] as List<object>, builtInType, ref elements, ref dimensions, level + 1, systemType, encodeableTypeId);

                            Pop();
                        }
                        else
                        {
                            break; // do not continue reading array of array
                        }
                    }
                    if (!hasInnerArray)
                    {
                        // read array from one dimension
                        Array part = ReadArray(null, ValueRanks.OneDimension, builtInType, systemType, encodeableTypeId);
                        if (part != null && part.Length > 0)
                        {
                            // add part elements to final list 
                            foreach (var item in part)
                            {
                                elements.Add(item);
                            }
                        }
                    }
                }
            }
            finally
            {
                m_nestingLevel--;
            }
        }

        /// <summary>
        /// Get Default value for NodeId for diferent IdTypes 
        /// </summary>
        /// <returns>new NodeId</returns>
        private NodeId DefaultNodeId(IdType idType, ushort namespaceIndex)
        {
            switch (idType)
            {
                case IdType.Numeric:
                default:
                {
                    return new NodeId(0U, namespaceIndex);
                }

                case IdType.Opaque:
                {
                    return new NodeId(Array.Empty<byte>(), namespaceIndex);
                }

                case IdType.String:
                {
                    return new NodeId("", namespaceIndex);
                }

                case IdType.Guid:
                {
                    return new NodeId(Guid.Empty, namespaceIndex);
                }
            }
        }

        private void EncodeAsJson(JsonTextWriter writer, object value)
        {
            try
            {
                if (value is Dictionary<string, object> map)
                {
                    EncodeAsJson(writer, map);
                    return;
                }

                if (value is List<object> list)
                {
                    writer.WriteStartArray();

                    foreach (var element in list)
                    {
                        EncodeAsJson(writer, element);
                    }

                    writer.WriteEndArray();
                    return;
                }

                writer.WriteValue(value);
            }
            catch (JsonWriterException jwe)
            {
                throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Unable to encode ExtensionObject Body as Json: {0}", jwe.Message);
            }
        }

        private void EncodeAsJson(JsonTextWriter writer, Dictionary<string, object> value)
        {
            writer.WriteStartObject();

            foreach (var field in value)
            {
                writer.WritePropertyName(field.Key);
                EncodeAsJson(writer, field.Value);
            }

            writer.WriteEndObject();
        }

        private bool ReadArrayField(string fieldName, out List<object> array)
        {
            array = null;
            object token;

            if (!ReadField(fieldName, out token))
            {
                return false;
            }

            array = token as List<object>;

            if (array == null)
            {
                return false;
            }

            if (m_context.MaxArrayLength > 0 && m_context.MaxArrayLength < array.Count)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            return true;
        }

        /// <summary>
        /// Safe Convert function which throws a BadDecodingError if unsuccessful.
        /// </summary>
        private byte[] SafeConvertFromBase64String(string s)
        {
            try
            {
                return Convert.FromBase64String(s);
            }
            catch (FormatException fe)
            {
                throw ServiceResultException.Create(StatusCodes.BadDecodingError, "Error decoding base64 string: {0}", fe.Message);
            }
        }

        /// <summary>
        /// Test and increment the nesting level.
        /// </summary>
        private void CheckAndIncrementNestingLevel()
        {
            if (m_nestingLevel > m_context.MaxEncodingNestingLevels)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingLimitsExceeded,
                    "Maximum nesting level of {0} was exceeded",
                    m_context.MaxEncodingNestingLevels);
            }
            m_nestingLevel++;
        }
        #endregion
    }
}
