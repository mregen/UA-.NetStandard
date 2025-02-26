// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json.Linq;
using Opc.Ua.Buffers;

#pragma warning disable CA2265 // Do not compare Span<T> to 'null' or 'default'

namespace Opc.Ua
{
    /// <summary>
    /// Writes objects to a JSON stream.
    /// </summary>
    public class JsonEncoder : IJsonEncoder
    {
        #region Private Fields
        private Stream m_stream;
        private IBufferWriter<byte> m_bufferWriter;
        private Utf8JsonWriter m_writer;
        private Stack<string> m_namespaces;
        private bool m_inVariantWithEncoding;
        private IServiceMessageContext m_context;
        private ushort[] m_namespaceMappings;
        private ushort[] m_serverMappings;
        private uint m_nestingLevel;
        private bool m_topLevelIsArray;
        private bool m_levelOneSkipped;
        private bool m_leaveOpen;
        private bool m_forceNamespaceUri;
        private bool m_forceNamespaceUriForIndex1;
        private bool m_includeDefaultNumberValues;
        private bool m_includeDefaultValues;
        private bool m_encodeNodeIdAsString;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the object with default values.
        /// Selects the reversible or non reversible encoding.
        /// </summary>
        public JsonEncoder(
            IServiceMessageContext context,
            bool useReversibleEncoding) :
            this(context, useReversibleEncoding ? JsonEncodingType.Reversible : JsonEncodingType.NonReversible, null, false)
        {
        }

        /// <summary>
        /// Initializes the object with default values.
        /// Selects the reversible or non reversible encoding.
        /// </summary>
        public JsonEncoder(
            IServiceMessageContext context,
            bool useReversibleEncoding,
            bool topLevelIsArray,
            Stream stream = null,
            bool leaveOpen = false,
            int streamSize = 0) :
            this(context, useReversibleEncoding ? JsonEncodingType.Reversible : JsonEncodingType.NonReversible, topLevelIsArray, stream, leaveOpen)
        {
        }

        /// <summary>
        /// Initializes the object with default values.
        /// </summary>
        public JsonEncoder(
            IServiceMessageContext context,
            JsonEncodingType encoding,
            bool topLevelIsArray,
            Stream stream,
            bool leaveOpen = false,
            int streamSize = 0)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Initialize(encoding);

            m_context = context;
            m_stream = stream;
            m_leaveOpen = leaveOpen;
            m_topLevelIsArray = topLevelIsArray;

            var options = new JsonWriterOptions() {
                Indented = false,
                MaxDepth = m_context.MaxEncodingNestingLevels > 0 ? m_context.MaxEncodingNestingLevels : 0,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (m_stream == null)
            {
                m_bufferWriter = new ArrayPoolBufferWriter<byte>();
                m_writer = new Utf8JsonWriter(m_bufferWriter, options);
                m_leaveOpen = false;
            }
            else
            {
                m_writer = new Utf8JsonWriter(m_stream, options);
            }

            InitializeWriter();
        }

        /// <summary>
        /// Initializes the object with default values.
        /// </summary>
        /// <remarks>
        /// Unlike a stream, for an IBufferWriter the implicit assumption is
        /// that the buffer writer is owned and disposed by the caller.
        /// </remarks>
        public JsonEncoder(
            IServiceMessageContext context,
            JsonEncodingType encoding,
            IBufferWriter<byte> bufferWriter = null,
            bool topLevelIsArray = false)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Initialize(encoding);

            m_context = context;
            m_bufferWriter = bufferWriter;
            m_leaveOpen = true;
            m_topLevelIsArray = topLevelIsArray;

            var options = new JsonWriterOptions() {
                Indented = false,
                MaxDepth = m_context.MaxEncodingNestingLevels > 0 ? m_context.MaxEncodingNestingLevels : 0,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (bufferWriter == null)
            {
                m_bufferWriter = new ArrayPoolBufferWriter<byte>();
                m_leaveOpen = false;
            }

            m_writer = new Utf8JsonWriter(m_bufferWriter, options);

            InitializeWriter();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize(JsonEncodingType encoding)
        {
            m_stream = null;
            m_writer = null;
            m_namespaces = new Stack<string>();
            m_leaveOpen = false;
            m_nestingLevel = 0;
            m_levelOneSkipped = false;

            // defaults for JSON encoding
            EncodingToUse = encoding;
            if (encoding == JsonEncodingType.Reversible || encoding == JsonEncodingType.NonReversible)
            {
                // defaults for reversible and non reversible JSON encoding
                // -- encode namespace index for reversible encoding / uri for non reversible
                // -- do not include default values for reversible encoding
                // -- include default values for non reversible encoding
                m_forceNamespaceUri =
                m_forceNamespaceUriForIndex1 =
                m_includeDefaultValues = encoding == JsonEncodingType.NonReversible;
                m_includeDefaultNumberValues = true;
                m_encodeNodeIdAsString = false;
            }
            else
            {
                // defaults for compact and verbose JSON encoding, properties throw exception if modified
                m_forceNamespaceUri = true;
                m_forceNamespaceUriForIndex1 = true;
                m_includeDefaultValues = encoding == JsonEncodingType.Verbose;
                m_includeDefaultNumberValues = encoding == JsonEncodingType.Verbose;
                m_encodeNodeIdAsString = true;
            }
            m_inVariantWithEncoding = false;
        }

        /// <summary>
        /// Initialize Writer.
        /// Writes the start object or array, and initializes the nesting level.
        /// </summary>
        private void InitializeWriter()
        {
            Debug.Assert(m_nestingLevel == 0);
            if (m_topLevelIsArray)
            {
                m_writer.WriteStartArray();
            }
            else
            {
                m_writer.WriteStartObject();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Encodes a session-less message to a buffer.
        /// </summary>
        public static void EncodeSessionLessMessage(IEncodeable message, Stream stream, IServiceMessageContext context, bool leaveOpen)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (context == null) throw new ArgumentNullException(nameof(context));

            // create encoder.
            JsonEncoder encoder = new JsonEncoder(context, true, false, stream, leaveOpen);
            try
            {
                long start = stream.Position;

                // write the message.
                var envelope = new SessionLessServiceMessage {
                    NamespaceUris = context.NamespaceUris,
                    ServerUris = context.ServerUris,
                    Message = message
                };

                envelope.Encode(encoder);

                // check that the max message size was not exceeded.
                if (context.MaxMessageSize > 0 && context.MaxMessageSize < (int)(stream.Position - start))
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadEncodingLimitsExceeded,
                        "MaxMessageSize {0} < {1}",
                        context.MaxMessageSize,
                        (int)(stream.Position - start));
                }

                encoder.Close();
            }
            finally
            {
                if (leaveOpen)
                {
                    stream.Position = 0;
                }
                encoder.Dispose();
            }
        }

        /// <inheritdoc/>
        public void EncodeMessage(IEncodeable message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            // convert the namespace uri to an index.
            NodeId typeId = ExpandedNodeId.ToNodeId(message.TypeId, m_context.NamespaceUris);

            // write the type id.
            WriteNodeId("TypeId", typeId);

            // write the message.
            WriteEncodeable("Body", message, message.GetType());
        }

        /// <inheritdoc/>
        public void SetMappingTables(NamespaceTable namespaceUris, StringTable serverUris)
        {
            m_namespaceMappings = null;

            if (namespaceUris != null && m_context.NamespaceUris != null)
            {
                m_namespaceMappings = namespaceUris.CreateMapping(m_context.NamespaceUris, false);
            }

            m_serverMappings = null;

            if (serverUris != null && m_context.ServerUris != null)
            {
                m_serverMappings = serverUris.CreateMapping(m_context.ServerUris, false);
            }
        }

        /// <inheritdoc/>
        public string CloseAndReturnText()
        {
            try
            {
                InternalClose(false);

                if (m_stream is MemoryStream memoryStream)
                {
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                if (m_bufferWriter is ArrayPoolBufferWriter<byte> bufferWriter)
                {
                    return Encoding.UTF8.GetString(bufferWriter.GetReadOnlySequence());
                }

                throw new NotSupportedException("Cannot get text from external stream. Use Close or MemoryStream or ArrayPoolBufferWriter instead.");
            }
            finally
            {
                m_writer?.Dispose();
                m_writer = null;
            }
        }

        /// <inheritdoc/>
        public int Close() => InternalClose(true);
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
                if (m_writer != null)
                {
                    InternalClose(true);
                    m_writer = null;
                }

                if (!m_leaveOpen)
                {
                    Utils.SilentDispose(m_stream);
                    Utils.SilentDispose(m_bufferWriter);
                }

                m_bufferWriter = null;
                m_stream = null;
            }
        }
        #endregion

        #region IJsonEncodeable Members
        /// <inheritdoc/>
        public JsonEncodingType EncodingToUse { get; private set; }

        /// <inheritdoc/>
        public bool SuppressArtifacts { get; set; }

        /// <inheritdoc/>
        public void PushStructure(string fieldName)
        {
            CheckAndIncrementNestingLevel();
            if (string.IsNullOrEmpty(fieldName))
            {
                if (m_nestingLevel == 1 && !m_topLevelIsArray)
                {
                    m_levelOneSkipped = true;
                    return;
                }

                m_writer.WriteStartObject();
            }
            else
            {
                m_writer.WriteStartObject(fieldName);
            }
        }

        /// <inheritdoc/>
        public void PushArray(string fieldName)
        {
            CheckAndIncrementNestingLevel();

            if (string.IsNullOrEmpty(fieldName))
            {
                if (m_nestingLevel == 1 && !m_topLevelIsArray)
                {
                    m_levelOneSkipped = true;
                    return;
                }

                m_writer.WriteStartArray();
            }
            else
            {
                m_writer.WriteStartArray(fieldName);
            }
        }

        /// <inheritdoc/>
        public void PopStructure()
        {
            if (m_nestingLevel > 1 || m_topLevelIsArray ||
               (m_nestingLevel == 1 && !m_levelOneSkipped))
            {
                m_writer.WriteEndObject();
            }

            m_nestingLevel--;
            Debug.Assert(m_nestingLevel >= 0);
        }

        /// <inheritdoc/>
        public void PopArray()
        {
            if (m_nestingLevel > 1 || m_topLevelIsArray ||
               (m_nestingLevel == 1 && !m_levelOneSkipped))
            {
                m_writer.WriteEndArray();
            }

            m_nestingLevel--;
            Debug.Assert(m_nestingLevel >= 0);
        }

        /// <inheritdoc/>
        [Obsolete("Non/Reversible encoding is deprecated. Use UsingAlternateEncoding instead to support new encoding types.")]
        public void UsingReversibleEncoding<T>(Action<string, T> action, string fieldName, T value, bool useReversibleEncoding)
        {
            JsonEncodingType currentValue = EncodingToUse;
            try
            {
                EncodingToUse = useReversibleEncoding ? JsonEncodingType.Reversible : JsonEncodingType.NonReversible;
                action(fieldName, value);
            }
            finally
            {
                EncodingToUse = currentValue;
            }
        }

        /// <inheritdoc/>
        public void UsingAlternateEncoding<T>(Action<string, T> action, string fieldName, T value, JsonEncodingType useEncoding)
        {
            JsonEncodingType currentValue = EncodingToUse;
            try
            {
                EncodingToUse = useEncoding;
                action(fieldName, value);
            }
            finally
            {
                EncodingToUse = currentValue;
            }
        }

        /// <inheritdoc/>
        public void WriteSwitchField(uint switchField)
        {
            if ((!SuppressArtifacts && EncodingToUse == JsonEncodingType.Compact) || EncodingToUse == JsonEncodingType.Reversible)
            {
                WriteUInt32("SwitchField"u8, switchField);
            }
        }

        /// <inheritdoc/>
        public void WriteEncodingMask(uint encodingMask)
        {
            if ((!SuppressArtifacts && EncodingToUse == JsonEncodingType.Compact) || EncodingToUse == JsonEncodingType.Reversible)
            {
                WriteUInt32("EncodingMask"u8, encodingMask);
            }
        }
        #endregion

        #region IEncoder Members
        /// <inheritdoc/>
        public EncodingType EncodingType => EncodingType.Json;

        /// <inheritdoc/>
        public bool UseReversibleEncoding => EncodingToUse != JsonEncodingType.NonReversible;

        /// <inheritdoc/>
        public IServiceMessageContext Context => m_context;

        /// <inheritdoc/>
        public bool ForceNamespaceUri
        {
            get => m_forceNamespaceUri;
            set => m_forceNamespaceUri = ThrowIfCompactOrVerbose(value);
        }

        /// <summary>
        /// The Json encoder to encode namespace URI for all
        /// namespaces
        /// </summary>
        public bool ForceNamespaceUriForIndex1
        {
            get => m_forceNamespaceUriForIndex1;
            set => m_forceNamespaceUriForIndex1 = ThrowIfCompactOrVerbose(value);
        }

        /// <summary>
        /// The Json encoder default value option.
        /// </summary>
        public bool IncludeDefaultValues
        {
            get => m_includeDefaultValues;
            set => m_includeDefaultValues = ThrowIfCompactOrVerbose(value);
        }

        /// <summary>
        /// The Json encoder default value option for numbers.
        /// </summary>
        public bool IncludeDefaultNumberValues
        {
            get => m_includeDefaultNumberValues || m_includeDefaultValues;
            set => m_includeDefaultNumberValues = ThrowIfCompactOrVerbose(value);
        }

        /// <summary>
        /// The Json encoder default encoding for NodeId as string or object.
        /// </summary>
        public bool EncodeNodeIdAsString
        {
            get => m_encodeNodeIdAsString;
            set => m_encodeNodeIdAsString = ThrowIfCompactOrVerbose(value);
        }

        /// <inheritdoc/>
        public void PushNamespace(string namespaceUri)
        {
            m_namespaces.Push(namespaceUri);
        }

        /// <inheritdoc/>
        public void PopNamespace()
        {
            m_namespaces.Pop();
        }

        /// <inheritdoc/>
        public void WriteBoolean(string fieldName, bool value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && !value)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteBoolean(fieldName, value);
            }
            else
            {
                m_writer.WriteBooleanValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteSByte(string fieldName, sbyte value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteByte(string fieldName, byte value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteInt16(string fieldName, short value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteUInt16(string fieldName, ushort value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteInt32(string fieldName, int value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteUInt32(string fieldName, uint value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteInt64(string fieldName, long value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteString(fieldName, value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                m_writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <inheritdoc/>
        public void WriteUInt64(string fieldName, ulong value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteString(fieldName, value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                m_writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <inheritdoc/>
        public void WriteFloat(string fieldName, float value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && (value > -Single.Epsilon) && (value < Single.Epsilon))
            {
                return;
            }

            if (Single.IsNaN(value))
            {
                WriteSimpleField(fieldName, "NaN"u8);
            }
            else if (Single.IsPositiveInfinity(value))
            {
                WriteSimpleField(fieldName, "Infinity"u8);
            }
            else if (Single.IsNegativeInfinity(value))
            {
                WriteSimpleField(fieldName, "-Infinity"u8);
            }
            else if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteDouble(string fieldName, double value)
        {
            if (fieldName != null && !IncludeDefaultNumberValues && (value > -Double.Epsilon) && (value < Double.Epsilon))
            {
                return;
            }

            if (Double.IsNaN(value))
            {
                WriteSimpleField(fieldName, "NaN"u8);
            }
            else if (Double.IsPositiveInfinity(value))
            {
                WriteSimpleField(fieldName, "Infinity"u8);
            }
            else if (Double.IsNegativeInfinity(value))
            {
                WriteSimpleField(fieldName, "-Infinity"u8);
            }
            else if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc/>
        public void WriteString(string fieldName, string value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == null)
            {
                return;
            }

            WriteSimpleField(fieldName, value);
        }

        /// <inheritdoc/>
        public void WriteDateTime(string fieldName, DateTime value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == DateTime.MinValue)
            {
                return;
            }

            if (value <= DateTime.MinValue)
            {
                WriteSimpleField(fieldName, "0001-01-01T00:00:00Z"u8);
            }
            else if (value >= DateTime.MaxValue)
            {
                WriteSimpleField(fieldName, "9999-12-31T23:59:59Z"u8);
            }
            else
            {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
                Span<byte> valueString = stackalloc byte[DateTimeRoundTripKindLength];
                ConvertUniversalTimeToString(value, valueString, out int charsWritten);
                WriteSimpleField(fieldName, valueString.Slice(0, charsWritten));
#else
                WriteSimpleField(fieldName, ConvertUniversalTimeToString(value));
#endif
            }
        }

        /// <inheritdoc/>
        public void WriteGuid(string fieldName, Uuid value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == Uuid.Empty)
            {
                return;
            }

            WriteSimpleField(fieldName, value.ToString());
        }

        /// <inheritdoc/>
        public void WriteGuid(string fieldName, Guid value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == Guid.Empty)
            {
                return;
            }

            WriteSimpleField(fieldName, value.ToString());
        }

        /// <inheritdoc/>
        public void WriteByteString(string fieldName, byte[] value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == null)
            {
                return;
            }

            if (value == null)
            {
                WriteSimpleFieldNull(fieldName);
                return;
            }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            WriteByteStringAsSpan(fieldName, value.AsSpan());
#else
            WriteByteString(fieldName, value, 0, value.Length);
#endif
        }

        /// <inheritdoc/>
        public void WriteByteString(string fieldName, byte[] value, int index, int count)
        {
            if (fieldName != null && !IncludeDefaultValues && value == null)
            {
                return;
            }

            if (value == null)
            {
                WriteSimpleFieldNull(fieldName);
                return;
            }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            WriteByteStringAsSpan(fieldName, value.AsSpan(index, count));
#else
            // check the length.
            if (m_context.MaxByteStringLength > 0 && m_context.MaxByteStringLength < count)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteString(fieldName, Convert.ToBase64String(value, index, count));
            }
            else
            {
                m_writer.WriteStringValue(Convert.ToBase64String(value, index, count));
            }
#endif
        }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        /// <inheritdoc/>
        public void WriteByteString(string fieldName, ReadOnlySpan<byte> value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == null)
            {
                return;
            }

            if (value == null)
            {
                WriteSimpleFieldNull(fieldName);
                return;
            }

            WriteByteStringAsSpan(fieldName, value);
        }

        private void WriteByteStringAsSpan(string fieldName, ReadOnlySpan<byte> value)
        {
            // check the length.
            if (m_context.MaxByteStringLength > 0 && m_context.MaxByteStringLength < value.Length)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            if (value.Length > 0)
            {
                const int maxStackLimit = 1024;
                int length = (value.Length + 2) / 3 * 4;
                char[] arrayPool = null;
                Span<char> chars = length <= maxStackLimit ?
                    stackalloc char[length] :
                    (arrayPool = ArrayPool<char>.Shared.Rent(length)).AsSpan(0, length);
                try
                {
                    bool success = Convert.TryToBase64Chars(value, chars, out int charsWritten, Base64FormattingOptions.None);
                    if (success)
                    {
                        WriteSimpleFieldAsCharSpan(fieldName, chars.Slice(0, charsWritten));
                        return;
                    }

                    throw new ServiceResultException(StatusCodes.BadEncodingError, "Failed to convert ByteString to Base64");
                }
                finally
                {
                    if (arrayPool != null)
                    {
                        ArrayPool<char>.Shared.Return(arrayPool);
                    }
                }
            }

            WriteSimpleField(fieldName, ""u8);
        }
#endif

        /// <inheritdoc/>
        public void WriteXmlElement(string fieldName, XmlElement value)
        {
            if (fieldName != null && !IncludeDefaultValues && value == null)
            {
                return;
            }

            if (value == null)
            {
                WriteSimpleFieldNull(fieldName);
                return;
            }

            string xml = value.OuterXml;

            if (m_context.MaxStringLength > 0 && m_context.MaxStringLength < xml.Length)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingLimitsExceeded,
                    "MaxStringLength {0} < {1}",
                    m_context.MaxStringLength,
                    xml.Length);
            }

            int maxByteCount = Encoding.UTF8.GetMaxByteCount(xml.Length);
            byte[] encodedBytes = ArrayPool<byte>.Shared.Rent(maxByteCount);
            try
            {
                int count = Encoding.UTF8.GetBytes(xml, 0, xml.Length, encodedBytes, 0);
                WriteByteString(fieldName, encodedBytes.AsSpan(0, count));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(encodedBytes);
            }
        }


        /// <inheritdoc/>
        public void WriteNodeId(string fieldName, NodeId value)
        {
            bool isNull = value == null || NodeId.IsNull(value);

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (m_encodeNodeIdAsString)
            {
                WriteSimpleField(fieldName, isNull ? "" : value.Format(m_context, ForceNamespaceUri));
                return;
            }

            PushStructure(fieldName);

            if (!isNull)
            {
                ushort namespaceIndex = value.NamespaceIndex;
                if (ForceNamespaceUri && namespaceIndex > (ForceNamespaceUriForIndex1 ? 0 : 1))
                {
                    string namespaceUri = Context.NamespaceUris.GetString(namespaceIndex);
                    WriteNodeIdContents(value, namespaceUri);
                }
                else
                {
                    WriteNodeIdContents(value);
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteExpandedNodeId(string fieldName, ExpandedNodeId value)
        {
            bool isNull = NodeId.IsNull(value);

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (m_encodeNodeIdAsString)
            {
                WriteSimpleField(fieldName, isNull ? "" : value.Format(m_context, ForceNamespaceUri));
                return;
            }

            PushStructure(fieldName);

            try
            {
                if (!isNull)
                {
                    string namespaceUri = value.NamespaceUri;
                    ushort namespaceIndex = value.InnerNodeId.NamespaceIndex;
                    if (ForceNamespaceUri && namespaceUri == null && namespaceIndex > (ForceNamespaceUriForIndex1 ? 0 : 1))
                    {
                        namespaceUri = Context.NamespaceUris.GetString(namespaceIndex);
                    }
                    WriteNodeIdContents(value.InnerNodeId, namespaceUri);

                    uint serverIndex = value.ServerIndex;

                    if (serverIndex >= 1)
                    {
                        if (EncodingToUse == JsonEncodingType.NonReversible)
                        {
                            string uri = m_context.ServerUris.GetString(serverIndex);

                            if (!string.IsNullOrEmpty(uri))
                            {
                                WriteSimpleField("ServerUri"u8, uri);
                            }

                            return;
                        }

                        if (m_serverMappings != null && m_serverMappings.Length > serverIndex)
                        {
                            serverIndex = m_serverMappings[serverIndex];
                        }

                        if (serverIndex != 0)
                        {
                            WriteUInt32("ServerUri"u8, serverIndex);
                        }
                    }
                }
            }
            finally
            {
                PopStructure();
            }
        }


        /// <inheritdoc/>
        public void WriteStatusCode(string fieldName, StatusCode value)
        {
            bool isNull = value == StatusCodes.Good;

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (EncodingToUse == JsonEncodingType.Reversible)
            {
                WriteUInt32(fieldName, value.Code);
                return;
            }

            PushStructure(fieldName);

            if (!isNull)
            {
                WriteUInt32("Code"u8, value.Code);

                if (EncodingToUse == JsonEncodingType.NonReversible || EncodingToUse == JsonEncodingType.Verbose)
                {
                    byte[] symbolicId = StatusCode.LookupUtf8SymbolicId(value.CodeBits);

                    if (symbolicId != null)
                    {
                        WriteSimpleField("Symbol"u8, symbolicId);
                    }
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteDiagnosticInfo(string fieldName, DiagnosticInfo value)
        {
            WriteDiagnosticInfo(fieldName, value, 0);
        }

        /// <inheritdoc/>
        public void WriteQualifiedName(string fieldName, QualifiedName value)
        {
            bool isNull = QualifiedName.IsNull(value);

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (m_encodeNodeIdAsString)
            {
                WriteSimpleField(fieldName, isNull ? "" : value.Format(m_context, ForceNamespaceUri));
                return;
            }

            PushStructure(fieldName);

            if (!isNull)
            {
                WriteString("Name", value.Name);
                WriteNamespaceIndex("Uri", value.NamespaceIndex);
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteLocalizedText(string fieldName, LocalizedText value)
        {
            bool isNull = LocalizedText.IsNullOrEmpty(value);

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (EncodingToUse == JsonEncodingType.NonReversible)
            {
                WriteSimpleField(fieldName, isNull ? "" : value.Text);
                return;
            }

            PushStructure(fieldName);

            if (!isNull)
            {
                WriteSimpleField("Text"u8, value.Text);

                if (!string.IsNullOrEmpty(value.Locale))
                {
                    WriteSimpleField("Locale"u8, value.Locale);
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteVariant(string fieldName, Variant value)
        {
            bool isNull = (value.TypeInfo == null || value.TypeInfo.BuiltInType == BuiltInType.Null || value.Value == null);

            if (EncodingToUse == JsonEncodingType.Compact || EncodingToUse == JsonEncodingType.Verbose)
            {
                if (fieldName != null && isNull && EncodingToUse == JsonEncodingType.Compact)
                {
                    return;
                }

                PushStructure(fieldName);

                if (!isNull)
                {
                    WriteVariantIntoObject("Value", value);
                }

                PopStructure();
                return;
            }

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (!isNull && EncodingToUse != JsonEncodingType.NonReversible)
            {
                PushStructure(fieldName);

                // encode enums as int32.
                byte encodingByte = (byte)value.TypeInfo.BuiltInType;

                if (value.TypeInfo.BuiltInType == BuiltInType.Enumeration)
                {
                    encodingByte = (byte)BuiltInType.Int32;
                }

                if (!SuppressArtifacts)
                {
                    WriteByte("Type", encodingByte);
                }

                m_writer.WritePropertyName("Body"u8);
                WriteVariantContents(value.Value, value.TypeInfo);

                if (value.Value is Matrix matrix)
                {
                    WriteInt32Array("Dimensions", matrix.Dimensions);
                }

                PopStructure();
            }
            else if (!string.IsNullOrEmpty(fieldName))
            {
                m_nestingLevel++;

                m_writer.WritePropertyName(fieldName);
                WriteVariantContents(value.Value, value.TypeInfo);

                m_nestingLevel--;
            }
        }

        private void WriteVariantIntoObject(string fieldName, Variant value)
        {
            if (Variant.Null == value)
            {
                return;
            }

            CheckAndIncrementNestingLevel();

            try
            {
                bool isNull = (value.TypeInfo == null || value.TypeInfo.BuiltInType == BuiltInType.Null || value.Value == null);

                if (!isNull)
                {
                    byte encodingByte = (byte)value.TypeInfo.BuiltInType;

                    if (value.TypeInfo.BuiltInType == BuiltInType.Enumeration)
                    {
                        encodingByte = (byte)BuiltInType.Int32;
                    }

                    if (!SuppressArtifacts)
                    {
                        WriteByte("UaType", encodingByte);
                    }
                }

                if (!string.IsNullOrEmpty(fieldName))
                {
                    m_writer.WritePropertyName(fieldName);
                }

                WriteVariantContents(value.Value, value.TypeInfo);

                if (value.Value is Matrix matrix)
                {
                    WriteInt32Array("Dimensions", matrix.Dimensions);
                }
            }
            finally
            {
                m_nestingLevel--;
            }
        }

        /// <inheritdoc/>
        public void WriteDataValue(string fieldName, DataValue value)
        {
            bool isNull = value == null;

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            PushStructure(fieldName);

            if (!isNull)
            {
                if (value.WrappedValue.TypeInfo != null && value.WrappedValue.TypeInfo.BuiltInType != BuiltInType.Null)
                {
                    if (EncodingToUse != JsonEncodingType.Compact && EncodingToUse != JsonEncodingType.Verbose)
                    {
                        WriteVariant("Value", value.WrappedValue);
                    }
                    else
                    {
                        WriteVariantIntoObject("Value", value.WrappedValue);
                    }
                }

                if (value.StatusCode != StatusCodes.Good)
                {
                    WriteStatusCode("StatusCode"u8, value.StatusCode);
                }

                if (value.SourceTimestamp != DateTime.MinValue)
                {
                    WriteDateTime("SourceTimestamp"u8, value.SourceTimestamp);

                    if (value.SourcePicoseconds != 0)
                    {
                        WriteUInt16("SourcePicoseconds", value.SourcePicoseconds);
                    }
                }

                if (value.ServerTimestamp != DateTime.MinValue)
                {
                    WriteDateTime("ServerTimestamp"u8, value.ServerTimestamp);

                    if (value.ServerPicoseconds != 0)
                    {
                        WriteUInt16("ServerPicoseconds", value.ServerPicoseconds);
                    }
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteDataValueStruct(string fieldName, ref DataValueStruct value, DataSetFieldContentMask dataSetFieldContentMask)
        {
            PushStructure(fieldName);

            if (value.WrappedValue.TypeInfo != null && value.WrappedValue.TypeInfo.BuiltInType != BuiltInType.Null)
            {
                if (EncodingToUse != JsonEncodingType.Compact && EncodingToUse != JsonEncodingType.Verbose)
                {
                    WriteVariant("Value", value.WrappedValue);
                }
                else
                {
                    WriteVariantIntoObject("Value", value.WrappedValue);
                }
            }

            if (value.StatusCode != StatusCodes.Good && (dataSetFieldContentMask & DataSetFieldContentMask.StatusCode) != 0)
            {
                WriteStatusCode("StatusCode"u8, value.StatusCode);
            }

            if ((dataSetFieldContentMask & DataSetFieldContentMask.SourceTimestamp) != 0 && value.SourceTimestamp != DateTime.MinValue)
            {
                WriteDateTime("SourceTimestamp"u8, value.SourceTimestamp);

                if (value.SourcePicoseconds != 0 && (dataSetFieldContentMask & DataSetFieldContentMask.SourcePicoSeconds) != 0)
                {
                    WriteUInt16("SourcePicoseconds", value.SourcePicoseconds);
                }
            }

            if ((dataSetFieldContentMask & DataSetFieldContentMask.ServerTimestamp) != 0 && value.ServerTimestamp != DateTime.MinValue)
            {
                WriteDateTime("ServerTimestamp"u8, value.ServerTimestamp);

                if (value.ServerPicoseconds != 0 && (dataSetFieldContentMask & DataSetFieldContentMask.ServerPicoSeconds) != 0)
                {
                    WriteUInt16("ServerPicoseconds", value.ServerPicoseconds);
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteExtensionObject(string fieldName, ExtensionObject value)
        {
            bool isNull = value == null || value.Encoding == ExtensionObjectEncoding.None;

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (isNull)
            {
                PushStructure(fieldName);
                PopStructure();
                return;
            }

            object body = value?.Body;
            var encodeable = body as IEncodeable;
            if (encodeable != null && EncodingToUse == JsonEncodingType.NonReversible)
            {
                // non reversible encoding, only the content of the Body field is encoded.
                if (body is IStructureTypeInfo structureType && structureType.StructureType == StructureType.Union)
                {
                    m_writer.WritePropertyName(fieldName ?? "Value");
                    encodeable.Encode(this);
                    return;
                }

                PushStructure(fieldName);
                encodeable.Encode(this);
                PopStructure();
                return;
            }

            PushStructure(fieldName);

            ExpandedNodeId typeId = (!NodeId.IsNull(value.TypeId)) ? value.TypeId : encodeable?.TypeId ?? NodeId.Null;
            var localTypeId = ExpandedNodeId.ToNodeId(typeId, Context.NamespaceUris);

            if (EncodingToUse == JsonEncodingType.Compact || EncodingToUse == JsonEncodingType.Verbose)
            {
                if (encodeable != null)
                {
                    if (!SuppressArtifacts && !NodeId.IsNull(localTypeId))
                    {
                        WriteNodeId("UaTypeId", localTypeId);
                    }

                    encodeable.Encode(this);
                }
                else
                {
                    if (body is JObject json)
                    {
                        if (!SuppressArtifacts && !NodeId.IsNull(localTypeId))
                        {
                            WriteNodeId("UaTypeId", localTypeId);
                        }

                        string text = json.ToString(Newtonsoft.Json.Formatting.None);
                        m_writer.WriteRawValue(text.Substring(1, text.Length - 2));
                    }
                    else if (value.Encoding == ExtensionObjectEncoding.Binary)
                    {
                        if (!SuppressArtifacts && !NodeId.IsNull(localTypeId))
                        {
                            WriteNodeId("UaTypeId", localTypeId);
                        }

                        WriteByte("UaEncoding", (byte)ExtensionObjectEncoding.Binary);
                        WriteByteString("UaBody", body as byte[]);
                    }
                    else if (value.Encoding == ExtensionObjectEncoding.Xml)
                    {
                        if (!SuppressArtifacts && !NodeId.IsNull(localTypeId))
                        {
                            WriteNodeId("UaTypeId", localTypeId);
                        }

                        WriteByte("UaEncoding", (byte)ExtensionObjectEncoding.Xml);
                        WriteXmlElement("UaBody", body as XmlElement);
                    }
                }

                PopStructure();
                return;
            }

            WriteNodeId("TypeId", localTypeId);

            if (encodeable != null)
            {
                switch (value.Encoding)
                {
                    case ExtensionObjectEncoding.Binary: { typeId = encodeable.BinaryEncodingId; break; }
                    case ExtensionObjectEncoding.Xml: { typeId = encodeable.XmlEncodingId; break; }
                    default: { typeId = encodeable.TypeId; break; }
                }
            }

            if (encodeable != null)
            {
                WriteEncodeable("Body", encodeable, null);
            }
            else
            {
                if (body is JObject json)
                {
                    string text = json.ToString(Newtonsoft.Json.Formatting.None);
                    m_writer.WriteRawValue(text.Substring(1, text.Length - 2));
                }
                else
                {
                    WriteByte("Encoding", (byte)value.Encoding);

                    if (value.Encoding == ExtensionObjectEncoding.Binary)
                    {
                        WriteByteString("Body", body as byte[]);
                    }
                    else if (value.Encoding == ExtensionObjectEncoding.Xml)
                    {
                        WriteXmlElement("Body", body as XmlElement);
                    }
                    else if (value.Encoding == ExtensionObjectEncoding.Json)
                    {
                        WriteSimpleField("Body"u8, body as string);
                    }
                }
            }

            PopStructure();
        }

        /// <inheritdoc/>
        public void WriteEncodeable(string fieldName, IEncodeable value, Type systemType)
        {
            bool isNull = value == null;

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            bool noFieldName = string.IsNullOrEmpty(fieldName);
            bool skipObject = false;

            if (m_nestingLevel == 0)
            {
                if (!noFieldName && m_topLevelIsArray)
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadEncodingError,
                        "With Array as top level, encodeables with fieldname create invalid json");
                }
                skipObject = noFieldName && !m_topLevelIsArray;
            }

            try
            {
                if (skipObject)
                {
                    value.Encode(this);
                }
                else
                {
                    PushStructure(fieldName);

                    value.Encode(this);

                    PopStructure();
                }
            }
            catch (InvalidOperationException ioe)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingError,
                    ioe,
                    "Failed to write encodable type {0}",
                    systemType.Name);
            }
        }

        /// <inheritdoc/>
        public void WriteEncodeable<T>(string fieldName, ref T value) where T : IEncodeable, new()
        {
            bool isNull = value == null;

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            bool noFieldName = string.IsNullOrEmpty(fieldName);
            bool skipObject = false;

            if (m_nestingLevel == 0)
            {
                if (!noFieldName && m_topLevelIsArray)
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadEncodingError,
                        "With Array as top level, encodeables with fieldname create invalid json");
                }
                skipObject = noFieldName && !m_topLevelIsArray;
            }

            try
            {
                if (skipObject)
                {
                    value.Encode(this);
                }
                else
                {
                    PushStructure(fieldName);

                    value.Encode(this);

                    PopStructure();
                }
            }
            catch (InvalidOperationException ioe)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingError,
                    ioe,
                    "Failed to write encodable type {0}",
                    typeof(T).Name);
            }
        }

        /// <inheritdoc/>
        public void WriteEnumerated(string fieldName, Enum value)
        {
            int numeric = Convert.ToInt32(value, CultureInfo.InvariantCulture);

            if (EncodingToUse == JsonEncodingType.Reversible ||
                EncodingToUse == JsonEncodingType.Compact)
            {
                if (!string.IsNullOrEmpty(fieldName))
                {
                    m_writer.WriteNumber(fieldName, numeric);
                }
                else
                {
                    m_writer.WriteNumberValue(numeric);
                }
            }
            else
            {
                string numericString = numeric.ToString(CultureInfo.InvariantCulture);
                string valueString = value.ToString();
                if (valueString == numericString)
                {
                    WriteSimpleField(fieldName, numericString);
                }
                else
                {
                    WriteSimpleField(fieldName, Utils.Format("{0}_{1}", valueString, numeric));
                }
            }
        }

        /// <inheritdoc/>
        private void WriteEnumerated(int numeric)
        {
            bool writeNumber = EncodingToUse == JsonEncodingType.Reversible || EncodingToUse == JsonEncodingType.Compact;
            if (!writeNumber)
            {
                string numericString = numeric.ToString(CultureInfo.InvariantCulture);
                WriteSimpleField((string)null, numericString);
            }
            else
            {
                m_writer.WriteNumberValue(numeric);
            }
        }

        /// <inheritdoc/>
        public void WriteBooleanArray(string fieldName, IList<bool> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteBoolean(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteSByteArray(string fieldName, IList<sbyte> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteSByte(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteByteArray(string fieldName, IList<byte> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteByte(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteInt16Array(string fieldName, IList<short> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteInt16(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteUInt16Array(string fieldName, IList<ushort> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteUInt16(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteInt32Array(string fieldName, IList<int> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteInt32(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteUInt32Array(string fieldName, IList<uint> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteUInt32((string)null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteInt64Array(string fieldName, IList<long> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteInt64(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteUInt64Array(string fieldName, IList<ulong> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteUInt64(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteFloatArray(string fieldName, IList<float> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteFloat(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteDoubleArray(string fieldName, IList<double> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteDouble(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteStringArray(string fieldName, IList<string> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteString(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteDateTimeArray(string fieldName, IList<DateTime> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                if (values[ii] <= DateTime.MinValue)
                {
                    m_writer.WriteNullValue();
                }
                else
                {
                    WriteDateTime((string)null, values[ii]);
                }
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteGuidArray(string fieldName, IList<Uuid> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteGuid(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteGuidArray(string fieldName, IList<Guid> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteGuid(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteByteStringArray(string fieldName, IList<byte[]> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteByteString(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteXmlElementArray(string fieldName, IList<XmlElement> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteXmlElement(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteNodeIdArray(string fieldName, IList<NodeId> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteNodeId(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteExpandedNodeIdArray(string fieldName, IList<ExpandedNodeId> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteExpandedNodeId(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteStatusCodeArray(string fieldName, IList<StatusCode> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                if (!UseReversibleEncoding && values[ii] == StatusCodes.Good)
                {
                    m_writer.WriteNullValue();
                }
                else
                {
                    WriteStatusCode((string)null, values[ii]);
                }
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteDiagnosticInfoArray(string fieldName, IList<DiagnosticInfo> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteDiagnosticInfo(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteQualifiedNameArray(string fieldName, IList<QualifiedName> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteQualifiedName(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteLocalizedTextArray(string fieldName, IList<LocalizedText> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteLocalizedText(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteVariantArray(string fieldName, IList<Variant> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                if (values[ii] == Variant.Null)
                {
                    m_writer.WriteNullValue();
                    continue;
                }

                WriteVariant((string)null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteDataValueArray(string fieldName, IList<DataValue> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteDataValue(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteExtensionObjectArray(string fieldName, IList<ExtensionObject> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteExtensionObject(null, values[ii]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteEncodeableArray(string fieldName, IList<IEncodeable> values, Type systemType)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            if (m_nestingLevel == 0)
            {
                bool hasFieldName = !string.IsNullOrWhiteSpace(fieldName);
                if (hasFieldName && m_topLevelIsArray || !hasFieldName && !m_topLevelIsArray)
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadEncodingError,
                        m_topLevelIsArray ?
                        "With Array as top level, an encodable array with fieldname creates invalid json." :
                        "With Object as top level, an encodable array without fieldname creates invalid json.");
                }
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteEncodeable(null, values[ii], systemType);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteEncodeableArray<T>(string fieldName, ArraySegment<T> values) where T : IEncodeable, new()
        {
            if (CheckForSimpleFieldArraySegmentNull(fieldName, values))
            {
                return;
            }

            if (m_nestingLevel == 0)
            {
                bool hasFieldName = !string.IsNullOrWhiteSpace(fieldName);
                if (hasFieldName && m_topLevelIsArray || !hasFieldName && !m_topLevelIsArray)
                {
                    throw ServiceResultException.Create(
                        StatusCodes.BadEncodingError,
                        m_topLevelIsArray ?
                        "With Array as top level, an encodable array with fieldname creates invalid json." :
                        "With Object as top level, an encodable array without fieldname creates invalid json.");
                }
            }

            PushArray(fieldName);

            for (int ii = 0; ii < values.Count; ii++)
            {
                WriteEncodeable<T>(null, ref values.Array[ii + values.Offset]);
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteEnumeratedArray(string fieldName, Array values, Type systemType)
        {
            if (fieldName != null && (values == null || values.Length == 0))
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            PushArray(fieldName);

            // check the length.
            if (m_context.MaxArrayLength > 0 && m_context.MaxArrayLength < values.Length)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            // encode each element in the array.
            Type arrayType = values.GetType().GetElementType();
            if (arrayType.IsEnum)
            {
                foreach (Enum value in values)
                {
                    WriteEnumerated(null, value);
                }
            }
            else
            {
                if (arrayType != typeof(Int32))
                {
                    throw new ServiceResultException(
                        StatusCodes.BadEncodingError,
                        Utils.Format("Type '{0}' is not allowed in an Enumeration.", arrayType.FullName));
                }
                foreach (Int32 value in values)
                {
                    WriteEnumerated(value);
                }
            }

            PopArray();
        }

        /// <inheritdoc/>
        public void WriteArray(string fieldName, object array, int valueRank, BuiltInType builtInType)
        {
            // write array.
            if (valueRank == ValueRanks.OneDimension)
            {
                switch (builtInType)
                {
                    case BuiltInType.Boolean: { WriteBooleanArray(fieldName, (bool[])array); return; }
                    case BuiltInType.SByte: { WriteSByteArray(fieldName, (sbyte[])array); return; }
                    case BuiltInType.Byte: { WriteByteArray(fieldName, (byte[])array); return; }
                    case BuiltInType.Int16: { WriteInt16Array(fieldName, (short[])array); return; }
                    case BuiltInType.UInt16: { WriteUInt16Array(fieldName, (ushort[])array); return; }
                    case BuiltInType.Int32: { WriteInt32Array(fieldName, (int[])array); return; }
                    case BuiltInType.UInt32: { WriteUInt32Array(fieldName, (uint[])array); return; }
                    case BuiltInType.Int64: { WriteInt64Array(fieldName, (long[])array); return; }
                    case BuiltInType.UInt64: { WriteUInt64Array(fieldName, (ulong[])array); return; }
                    case BuiltInType.Float: { WriteFloatArray(fieldName, (float[])array); return; }
                    case BuiltInType.Double: { WriteDoubleArray(fieldName, (double[])array); return; }
                    case BuiltInType.String: { WriteStringArray(fieldName, (string[])array); return; }
                    case BuiltInType.DateTime: { WriteDateTimeArray(fieldName, (DateTime[])array); return; }
                    case BuiltInType.Guid: { WriteGuidArray(fieldName, (Uuid[])array); return; }
                    case BuiltInType.ByteString: { WriteByteStringArray(fieldName, (byte[][])array); return; }
                    case BuiltInType.XmlElement: { WriteXmlElementArray(fieldName, (XmlElement[])array); return; }
                    case BuiltInType.NodeId: { WriteNodeIdArray(fieldName, (NodeId[])array); return; }
                    case BuiltInType.ExpandedNodeId: { WriteExpandedNodeIdArray(fieldName, (ExpandedNodeId[])array); return; }
                    case BuiltInType.StatusCode: { WriteStatusCodeArray(fieldName, (StatusCode[])array); return; }
                    case BuiltInType.QualifiedName: { WriteQualifiedNameArray(fieldName, (QualifiedName[])array); return; }
                    case BuiltInType.LocalizedText: { WriteLocalizedTextArray(fieldName, (LocalizedText[])array); return; }
                    case BuiltInType.ExtensionObject: { WriteExtensionObjectArray(fieldName, (ExtensionObject[])array); return; }
                    case BuiltInType.DataValue: { WriteDataValueArray(fieldName, (DataValue[])array); return; }
                    case BuiltInType.DiagnosticInfo: { WriteDiagnosticInfoArray(fieldName, (DiagnosticInfo[])array); return; }
                    case BuiltInType.Enumeration:
                    {
                        if (!(array is Array enumArray))
                        {
                            throw ServiceResultException.Create(
                                StatusCodes.BadEncodingError,
                                "Unexpected non Array type encountered while encoding an array of enumeration.");
                        }
                        WriteEnumeratedArray(fieldName, enumArray, enumArray.GetType().GetElementType());
                        return;
                    }
                    case BuiltInType.Variant:
                    {
                        if (array is Variant[] variants)
                        {
                            WriteVariantArray(fieldName, variants);
                            return;
                        }

                        // try to write IEncodeable Array
                        if (array is IEncodeable[] encodeableArray)
                        {
                            WriteEncodeableArray(fieldName, encodeableArray, array.GetType().GetElementType());
                            return;
                        }

                        // try to write IEncodeable encodeables
                        if (array is IList<IEncodeable> encodeableList)
                        {
                            WriteEncodeableArray(fieldName, encodeableList, array.GetType().GetElementType());
                            return;
                        }

                        if (array is object[] objects)
                        {
                            WriteObjectArray(fieldName, objects);
                            return;
                        }

                        // try to cast array to IEncodeable encodeables
                        if (array is Array arrayType &&
                            (arrayType.Length == 0 || arrayType.GetType().GetElementType().IsInstanceOfType(arrayType.GetValue(0))))
                        {
                            List<IEncodeable> encodeables = arrayType.Cast<IEncodeable>().ToList();
                            WriteEncodeableArray(fieldName, encodeables, array.GetType().GetElementType());
                            return;
                        }

                        throw ServiceResultException.Create(
                            StatusCodes.BadEncodingError,
                            "Unexpected type encountered while encoding an array of Variants: {0}",
                            array.GetType());
                    }
                    default:
                    {
                        // try to write IEncodeable Array
                        if (array is IEncodeable[] encodeableArray)
                        {
                            WriteEncodeableArray(fieldName, encodeableArray, array.GetType().GetElementType());
                            return;
                        }
                        if (array == null)
                        {
                            WriteSimpleFieldNullOrOmit(fieldName);
                            return;
                        }
                        throw ServiceResultException.Create(
                            StatusCodes.BadEncodingError,
                            "Unexpected BuiltInType encountered while encoding an array: {0}",
                            builtInType);
                    }
                }
            }

            // write matrix.
            else if (valueRank > ValueRanks.OneDimension)
            {
                if (!(array is Matrix matrix))
                {
                    if (array is Array multiArray && multiArray.Rank == valueRank)
                    {
                        matrix = new Matrix(multiArray, builtInType);
                    }
                    else
                    {
                        throw ServiceResultException.Create(
                            StatusCodes.BadEncodingError,
                            "Unexpected array type encountered while encoding array: {0}",
                            array.GetType().Name);
                    }
                }

                if (matrix != null)
                {
                    if (EncodingToUse == JsonEncodingType.Compact || EncodingToUse == JsonEncodingType.Verbose)
                    {
                        WriteArrayDimensionMatrix(fieldName, builtInType, matrix);
                    }
                    else
                    {
                        int index = 0;
                        WriteStructureMatrix(fieldName, matrix, 0, ref index, matrix.TypeInfo);
                    }
                    return;
                }

                // field is omitted
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Writes a raw value.
        /// </summary>
        public void WriteRawValue(FieldMetaData field, DataValue dv, DataSetFieldContentMask mask)
        {
            bool encodeAsObject = false;
            m_writer.WritePropertyName(field.Name);

            if (mask != DataSetFieldContentMask.None && mask != DataSetFieldContentMask.RawData)
            {
                encodeAsObject = true;
                PushStructure("Value"u8);
            }

            if (mask == DataSetFieldContentMask.None && StatusCode.IsBad(dv.StatusCode))
            {
                dv = new DataValue() { WrappedValue = dv.StatusCode };
            }

            WriteRawValueContents(field, dv);

            if (encodeAsObject)
            {
                if ((mask & DataSetFieldContentMask.StatusCode) != 0 && dv.StatusCode != StatusCodes.Good)
                {
                    WriteStatusCode(nameof(dv.StatusCode), dv.StatusCode);
                }

                if ((mask & DataSetFieldContentMask.SourceTimestamp) != 0)
                {
                    if (dv.SourceTimestamp != DateTime.MinValue)
                    {
                        WriteDateTime(nameof(dv.SourceTimestamp), dv.SourceTimestamp);

                        if (dv.SourcePicoseconds != 0)
                        {
                            WriteUInt16(nameof(dv.SourcePicoseconds), dv.SourcePicoseconds);
                        }
                    }
                }

                if ((mask & DataSetFieldContentMask.ServerTimestamp) != 0)
                {
                    if (dv.ServerTimestamp != DateTime.MinValue)
                    {
                        WriteDateTime(nameof(dv.ServerTimestamp), dv.ServerTimestamp);

                        if (dv.ServerPicoseconds != 0)
                        {
                            WriteUInt16(nameof(dv.ServerPicoseconds), dv.ServerPicoseconds);
                        }
                    }
                }

                PopStructure();
            }
        }
        #endregion

        #region Private Methods
        private void WriteRawExtensionObject(object value)
        {
            if (value is ExtensionObject eo)
            {
                value = eo.Body;
            }

            if (value is IEncodeable encodeable)
            {
                PushStructure();
                encodeable.Encode(this);
                PopStructure();
            }
            else
            {
                m_writer.WriteNullValue();
            }
        }

        private void WriteRawVariantArray(object value)
        {
            if (value is IList<Variant> list)
            {
                PushArray(null);

                foreach (Variant ii in list)
                {
                    if (ii is Variant vt)
                    {
                        PushStructure();
                        WriteVariantContents(vt.Value, vt.TypeInfo);
                        PopStructure();
                    }
                    else
                    {
                        m_writer.WriteNullValue();
                    }
                }

                PopArray();
            }
            else
            {
                m_writer.WriteNullValue();
            }
        }

        private void WriteRawValueContents(FieldMetaData field, DataValue dv)
        {
            object value = dv.Value;
            TypeInfo typeInfo = dv.WrappedValue.TypeInfo;

            if (dv.WrappedValue == Variant.Null)
            {
                value = TypeInfo.GetDefaultValue(NodeId.Create(field.BuiltInType), field.ValueRank);
                typeInfo = new TypeInfo((BuiltInType)field.BuiltInType, field.ValueRank);

                if (value != null)
                {
                    WriteVariantContents(value, typeInfo);
                }
                else if (field.ValueRank >= 0)
                {
                    PushArray((string)null);
                    PopArray();
                }
                else if (field.BuiltInType == (byte)BuiltInType.ExtensionObject)
                {
                    PushStructure();
                    PopStructure();
                }
                else
                {
                    m_writer.WriteNullValue();
                }
                return;
            }

            if (field.ValueRank == ValueRanks.Scalar)
            {
                if (field.BuiltInType == (byte)BuiltInType.ExtensionObject)
                {
                    WriteRawExtensionObject(value);
                    return;
                }
            }
            else
            {
                if (value is Matrix matrix)
                {
                    PushStructure();
                    PushArray("Array");

                    foreach (object ii in matrix.Elements)
                    {
                        if (field.BuiltInType == (byte)BuiltInType.ExtensionObject)
                        {
                            WriteRawExtensionObject(ii);
                            continue;
                        }
                        else if (field.BuiltInType == (byte)BuiltInType.Variant)
                        {
                            if (ii is Variant vt)
                            {
                                WriteVariant(null, vt);
                            }
                            else
                            {
                                m_writer.WriteNullValue();
                            }
                            continue;
                        }

                        WriteVariantContents(ii, new TypeInfo((BuiltInType)field.BuiltInType, ValueRanks.Scalar));
                    }

                    PopArray();
                    WriteInt32Array("Dimensions", matrix.Dimensions);
                    PopStructure();
                    return;
                }

                if (field.BuiltInType == (byte)BuiltInType.ExtensionObject)
                {
                    if (value is IList<ExtensionObject> list)
                    {
                        PushArray(null);

                        foreach (ExtensionObject element in list)
                        {
                            WriteRawExtensionObject(element);
                        }

                        PopArray();
                        return;
                    }
                }

                if (field.BuiltInType == (byte)BuiltInType.Variant)
                {
                    if (value is IList<Variant> list)
                    {
                        WriteRawVariantArray(value);
                        return;
                    }
                }
            }

            WriteVariantContents(value, typeInfo);

            if (EncodingToUse == JsonEncodingType.Reversible)
            {
                if (dv.Value is Matrix matrix)
                {
                    WriteInt32Array("Dimensions", matrix.Dimensions);
                }

                PopStructure();
            }
        }

        /// <summary>
        /// Writes the namespace index for NodeIds.
        /// </summary>
        private void WriteNamespaceIndex(string fieldName, ushort namespaceIndex)
        {
            if (namespaceIndex == 0)
            {
                return;
            }

            if ((!UseReversibleEncoding || ForceNamespaceUri) && namespaceIndex > (ForceNamespaceUriForIndex1 ? 0 : 1))
            {
                string uri = m_context.NamespaceUris.GetString(namespaceIndex);
                if (!string.IsNullOrEmpty(uri))
                {
                    WriteSimpleField(fieldName, uri);
                    return;
                }
            }

            if (m_namespaceMappings != null && m_namespaceMappings.Length > namespaceIndex)
            {
                namespaceIndex = m_namespaceMappings[namespaceIndex];
            }

            if (namespaceIndex != 0)
            {
                WriteUInt16(fieldName, namespaceIndex);
            }
        }

        /// <summary>
        /// Writes the contents of a Variant to the stream.
        /// </summary>
        private void WriteVariantContents(object value, TypeInfo typeInfo)
        {
            bool inVariantWithEncoding = m_inVariantWithEncoding;
            try
            {
                m_inVariantWithEncoding = true;

                // check for null.
                if (value == null)
                {
                    m_writer.WriteNullValue();
                    return;
                }

                // write scalar.
                if (typeInfo.ValueRank < 0)
                {
                    switch (typeInfo.BuiltInType)
                    {
                        case BuiltInType.Boolean: { WriteBoolean((string)null, (bool)value); return; }
                        case BuiltInType.SByte: { WriteSByte((string)null, (sbyte)value); return; }
                        case BuiltInType.Byte: { WriteByte((string)null, (byte)value); return; }
                        case BuiltInType.Int16: { WriteInt16((string)null, (short)value); return; }
                        case BuiltInType.UInt16: { WriteUInt16((string)null, (ushort)value); return; }
                        case BuiltInType.Int32: { WriteInt32((string)null, (int)value); return; }
                        case BuiltInType.UInt32: { WriteUInt32((string)null, (uint)value); return; }
                        case BuiltInType.Int64: { WriteInt64((string)null, (long)value); return; }
                        case BuiltInType.UInt64: { WriteUInt64((string)null, (ulong)value); return; }
                        case BuiltInType.Float: { WriteFloat((string)null, (float)value); return; }
                        case BuiltInType.Double: { WriteDouble((string)null, (double)value); return; }
                        case BuiltInType.String: { WriteString((string)null, (string)value); return; }
                        case BuiltInType.DateTime: { WriteDateTime((string)null, (DateTime)value); return; }
                        case BuiltInType.Guid: { WriteGuid((string)null, (Uuid)value); return; }
                        case BuiltInType.ByteString: { WriteByteString((string)null, (byte[])value); return; }
                        case BuiltInType.XmlElement: { WriteXmlElement((string)null, (XmlElement)value); return; }
                        case BuiltInType.NodeId: { WriteNodeId((string)null, (NodeId)value); return; }
                        case BuiltInType.ExpandedNodeId: { WriteExpandedNodeId((string)null, (ExpandedNodeId)value); return; }
                        case BuiltInType.StatusCode: { WriteStatusCode((string)null, (StatusCode)value); return; }
                        case BuiltInType.QualifiedName: { WriteQualifiedName((string)null, (QualifiedName)value); return; }
                        case BuiltInType.LocalizedText: { WriteLocalizedText((string)null, (LocalizedText)value); return; }
                        case BuiltInType.ExtensionObject: { WriteExtensionObject((string)null, (ExtensionObject)value); return; }
                        case BuiltInType.DataValue: { WriteDataValue((string)null, (DataValue)value); return; }
                        case BuiltInType.Enumeration: { WriteEnumerated((string)null, (Enum)value); return; }
                        case BuiltInType.DiagnosticInfo: { WriteDiagnosticInfo((string)null, (DiagnosticInfo)value); return; }
                    }
                }
                // write array
                else if (typeInfo.ValueRank >= ValueRanks.OneDimension)
                {
                    int valueRank = typeInfo.ValueRank;
                    if (EncodingToUse != JsonEncodingType.NonReversible && value is Matrix matrix)
                    {
                        // linearize the matrix
                        value = matrix.Elements;
                        valueRank = ValueRanks.OneDimension;
                    }
                    WriteArray(null, value, valueRank, typeInfo.BuiltInType);
                }
            }
            finally
            {
                m_inVariantWithEncoding = inVariantWithEncoding;
            }
        }

        /// <summary>
        /// Writes the type information of NodeIds.
        /// </summary>
        private void WriteNodeIdContents(NodeId value, string namespaceUri = null)
        {
            if (value.IdType > IdType.Numeric)
            {
                WriteInt32("IdType", (int)value.IdType);
            }

            switch (value.IdType)
            {
                case IdType.Numeric:
                {
                    WriteUInt32("Id"u8, (uint)value.Identifier);
                    break;
                }

                case IdType.String:
                {
                    WriteString("Id", (string)value.Identifier);
                    break;
                }

                case IdType.Guid:
                {
                    if (value.Identifier is Guid guidIdentifier)
                    {
                        WriteGuid("Id", guidIdentifier);
                    }
                    else if (value.Identifier is Uuid uuidIdentifier)
                    {
                        WriteGuid("Id", uuidIdentifier);
                    }
                    else
                    {
                        throw new ServiceResultException(
                            StatusCodes.BadEncodingError,
                            "Invalid Identifier type to encode as Guid NodeId.");
                    }
                    break;
                }

                case IdType.Opaque:
                {
                    WriteByteString("Id", (byte[])value.Identifier);
                    break;
                }
            }

            if (namespaceUri != null)
            {
                WriteString("Namespace", namespaceUri);
            }
            else
            {
                WriteNamespaceIndex("Namespace", value.NamespaceIndex);
            }
        }

        /// <inheritdoc cref="PushStructure(string)"/>
        private void PushStructure(ReadOnlySpan<byte> fieldName)
        {
            CheckAndIncrementNestingLevel();
            if (fieldName == null || fieldName.IsEmpty)
            {
                m_writer.WriteStartObject();
            }
            else
            {
                m_writer.WriteStartObject(fieldName);
            }
        }

        /// <inheritdoc cref="PushStructure(string)"/>
        private void PushStructure()
        {
            CheckAndIncrementNestingLevel();

            if (m_nestingLevel == 1 && !m_topLevelIsArray)
            {
                m_levelOneSkipped = true;
                return;
            }

            m_writer.WriteStartObject();
        }

        /// <summary>
        /// Writes a null value.
        /// </summary>
        private void WriteSimpleFieldNull(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNullValue();
            }
            else
            {
                m_writer.WriteNull(fieldName);
            }
        }

        /// <summary>
        /// Writes a null value if the field name is null or omits the field.
        /// </summary>
        private void WriteSimpleFieldNullOrOmit(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteNullValue();
            }
        }

        /// <inheritdoc cref="WriteSimpleFieldNullOrOmit(string)"/>
        private void WriteSimpleFieldNullOrOmit(ReadOnlySpan<byte> fieldName)
        {
            if (fieldName == null)
            {
                m_writer.WriteNullValue();
            }
        }

        /// <summary>
        /// Writes a JSON field value pair.
        /// </summary>
        private void WriteSimpleField(string fieldName, string value)
        {
            // unlike Span<byte>, Span<char> can not become null, handle the case here
            if (value == null)
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <inheritdoc cref="WriteSimpleField(string, string)"/>
        private void WriteSimpleField(string fieldName, ReadOnlySpan<byte> value)
        {
            if (value == null)
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <inheritdoc cref="WriteSimpleField(string, string)"/>
        private void WriteSimpleField(ReadOnlySpan<byte> fieldName, ReadOnlySpan<byte> value)
        {
            Debug.Assert(fieldName != null);

            // unlike Span<byte>, Span<char> can not become null, handle the case here
            if (value == null)
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            if (fieldName == null)
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <inheritdoc cref="WriteSimpleField(string, string)"/>
        private void WriteSimpleField(ReadOnlySpan<byte> fieldName, string value)
        {
            // unlike Span<byte>, Span<char> can not become null, handle the case here
            if (value == null)
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            if (fieldName == null)
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <summary>
        /// Writes a JSON field value pair, but accepts only ReadOnlySpan{char} as value.
        /// </summary>
        private void WriteSimpleFieldAsCharSpan(string fieldName, ReadOnlySpan<char> value)
        {
            Debug.Assert(!value.IsEmpty);

            if (fieldName == null)
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <inheritdoc cref="WriteSimpleFieldAsCharSpan(string, ReadOnlySpan{char})"/>
        private void WriteSimpleFieldAsCharSpan(ReadOnlySpan<byte> fieldName, ReadOnlySpan<char> value)
        {
            Debug.Assert(!value.IsEmpty);

            if (fieldName == null)
            {
                m_writer.WriteStringValue(value);
            }
            else
            {
                m_writer.WriteString(fieldName, value);
            }
        }

        /// <inheritdoc cref="WriteUInt32(string, uint)"/>
        private void WriteUInt32(ReadOnlySpan<byte> fieldName, uint value)
        {
            Debug.Assert(fieldName != null);
            if (/*fieldName != null && */!IncludeDefaultNumberValues && value == 0)
            {
                return;
            }

            if (fieldName != null)
            {
                m_writer.WriteNumber(fieldName, value);
            }
            else
            {
                m_writer.WriteNumberValue(value);
            }
        }

        /// <inheritdoc cref="WriteStatusCode(string, StatusCode)"/>
        private void WriteStatusCode(ReadOnlySpan<byte> fieldName, StatusCode value)
        {
            bool isNull = value == StatusCodes.Good;

            Debug.Assert(fieldName != null);
            if (/*fieldName != null && */isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (EncodingToUse == JsonEncodingType.Reversible)
            {
                WriteUInt32(fieldName, value.Code);
                return;
            }

            // Verbose and NonReversible
            PushStructure(fieldName);

            if (!isNull)
            {
                WriteUInt32("Code"u8, value.Code);
                if (EncodingToUse == JsonEncodingType.NonReversible || EncodingToUse == JsonEncodingType.Verbose)
                {
                    byte[] symbolicId = StatusCode.LookupUtf8SymbolicId(value.CodeBits);
                    if (symbolicId != null)
                    {
                        WriteSimpleField("Symbol"u8, symbolicId);
                    }
                }
            }

            PopStructure();
        }

        /// <inheritdoc cref="WriteDateTime(string, DateTime)"/>
        private void WriteDateTime(ReadOnlySpan<byte> fieldName, DateTime value)
        {
            Debug.Assert(fieldName != null);

            if (value <= DateTime.MinValue)
            {
                WriteSimpleField(fieldName, "0001-01-01T00:00:00Z"u8);
            }
            else if (value >= DateTime.MaxValue)
            {
                WriteSimpleField(fieldName, "9999-12-31T23:59:59Z"u8);
            }
            else
            {
                Span<byte> valueUtf8String = stackalloc byte[DateTimeRoundTripKindLength];
                ConvertUniversalTimeToString(value, valueUtf8String, out int charsWritten);
                WriteSimpleField(fieldName, valueUtf8String.Slice(0, charsWritten));
            }
        }

        /// <summary>
        /// Writes a Variant array to the stream.
        /// </summary>
        private void WriteObjectArray(string fieldName, IList<object> values)
        {
            if (CheckForSimpleFieldNull(fieldName, values))
            {
                return;
            }

            PushArray(fieldName);

            if (values != null)
            {
                for (int ii = 0; ii < values.Count; ii++)
                {
                    WriteVariant("Variant", new Variant(values[ii]));
                }
            }

            PopArray();
        }

        /// <summary>
        /// Returns true if a simple field can be written.
        /// </summary>
        private bool CheckForSimpleFieldArraySegmentNull<T>(string fieldName, ArraySegment<T> values)
        {
            // always include default values for non reversible/verbose
            // include default values when encoding in a Variant
            if (values.Array == null || (values.Count == 0 && !m_inVariantWithEncoding && !m_includeDefaultValues))
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return true;
            }

            if (m_context.MaxArrayLength > 0 && m_context.MaxArrayLength < values.Count)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            return false;
        }

        /// <summary>
        /// Returns true if a simple field can be written.
        /// </summary>
        private bool CheckForSimpleFieldNull<T>(string fieldName, IList<T> values)
        {
            // always include default values for non reversible/verbose
            // include default values when encoding in a Variant
            if (values == null || (values.Count == 0 && !m_inVariantWithEncoding && !m_includeDefaultValues))
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return true;
            }

            if (values != null && m_context.MaxArrayLength > 0 && m_context.MaxArrayLength < values.Count)
            {
                throw new ServiceResultException(StatusCodes.BadEncodingLimitsExceeded);
            }

            return false;
        }

        /// <summary>
        /// Called on properties which can only be modified for the deprecated encoding.
        /// </summary>
        private bool ThrowIfCompactOrVerbose(bool value)
        {
            if (EncodingToUse == JsonEncodingType.Compact || EncodingToUse == JsonEncodingType.Verbose)
            {
                throw new NotSupportedException($"This property can not be modified with {EncodingToUse} encoding.");
            }
            return value;
        }

        /// <summary>
        /// Completes writing and returns the text length.
        /// </summary>
        private int InternalClose(bool dispose)
        {
            if (m_nestingLevel == 0)
            {
                if (m_topLevelIsArray)
                {
                    m_writer.WriteEndArray();
                }
                else
                {
                    m_writer.WriteEndObject();
                }
            }

            m_writer?.Flush();
            int length = (int)m_writer.BytesCommitted;

            if (dispose)
            {
                m_writer.Dispose();
                m_writer = null;
            }

            return length;
        }

        /// <inheritdoc cref="WriteDiagnosticInfo(string, DiagnosticInfo)"/>
        private void WriteDiagnosticInfo(string fieldName, DiagnosticInfo value, int depth)
        {
            bool isNull = (value == null || value.IsNullDiagnosticInfo);

            if (fieldName != null && isNull && !IncludeDefaultValues)
            {
                return;
            }

            if (value == null)
            {
                WriteSimpleFieldNullOrOmit(fieldName);
                return;
            }

            PushStructure(fieldName);

            if (value.SymbolicId >= 0)
            {
                WriteInt32("SymbolicId", value.SymbolicId);
            }

            if (value.NamespaceUri >= 0)
            {
                WriteInt32("NamespaceUri", value.NamespaceUri);
            }

            if (value.Locale >= 0)
            {
                WriteInt32("Locale", value.Locale);
            }

            if (value.LocalizedText >= 0)
            {
                WriteInt32("LocalizedText", value.LocalizedText);
            }

            if (value.AdditionalInfo != null)
            {
                WriteSimpleField("AdditionalInfo"u8, value.AdditionalInfo);
            }

            if (value.InnerStatusCode != StatusCodes.Good)
            {
                WriteStatusCode("InnerStatusCode"u8, value.InnerStatusCode);
            }

            if (value.InnerDiagnosticInfo != null)
            {
                if (depth < DiagnosticInfo.MaxInnerDepth)
                {
                    WriteDiagnosticInfo("InnerDiagnosticInfo", value.InnerDiagnosticInfo, depth + 1);
                }
                else
                {
                    Utils.LogWarning("InnerDiagnosticInfo dropped because nesting exceeds maximum of {0}.",
                        DiagnosticInfo.MaxInnerDepth);
                }
            }

            PopStructure();
        }

        /// <summary>
        /// Encode the Matrix as Dimensions/Array element.
        /// Writes the matrix as a flattended array with dimensions.
        /// Validates the dimensions and array size.
        /// </summary>
        private void WriteArrayDimensionMatrix(string fieldName, BuiltInType builtInType, Matrix matrix)
        {
            // check if matrix is well formed
            (bool valid, int sizeFromDimensions) = Matrix.ValidateDimensions(true, matrix.Dimensions, Context.MaxArrayLength);

            if (!valid || (sizeFromDimensions != matrix.Elements.Length))
            {
                throw ServiceResultException.Create(StatusCodes.BadEncodingError,
                    "The number of elements in the matrix does not match the dimensions.");
            }

            PushStructure(fieldName);
            WriteInt32Array("Dimensions", matrix.Dimensions);
            WriteArray("Array", matrix.Elements, 1, builtInType);
            PopStructure();
        }

        /// <summary>
        /// Write multi dimensional array in structure.
        /// </summary>
        private void WriteStructureMatrix(
            string fieldName,
            Matrix matrix,
            int dim,
            ref int index,
            TypeInfo typeInfo)
        {
            // check if matrix is well formed
            (bool valid, int sizeFromDimensions) = Matrix.ValidateDimensions(true, matrix.Dimensions, Context.MaxArrayLength);

            if (!valid || (sizeFromDimensions != matrix.Elements.Length))
            {
                throw ServiceResultException.Create(StatusCodes.BadEncodingError,
                    "The number of elements in the matrix does not match the dimensions.");
            }

            CheckAndIncrementNestingLevel();

            try
            {
                int arrayLen = matrix.Dimensions[dim];
                if (dim == matrix.Dimensions.Length - 1)
                {
                    // Create a slice of values for the top dimension
                    var copy = Array.CreateInstance(matrix.Elements.GetType().GetElementType(), arrayLen);
                    Array.Copy(matrix.Elements, index, copy, 0, arrayLen);
                    WriteVariantContents(copy, TypeInfo.CreateArray(typeInfo.BuiltInType));
                    index += arrayLen;
                }
                else
                {
                    PushArray(fieldName);
                    for (int i = 0; i < arrayLen; i++)
                    {
                        WriteStructureMatrix(null, matrix, dim + 1, ref index, typeInfo);
                    }
                    PopArray();
                }
            }
            finally
            {
                m_nestingLevel--;
            }
        }

        /// <summary>
        /// Test and increment the nesting level.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckAndIncrementNestingLevel()
        {
            int maxEncodingNestingLevels = m_context.MaxEncodingNestingLevels;
            if (maxEncodingNestingLevels != 0 && m_nestingLevel > maxEncodingNestingLevels)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadEncodingLimitsExceeded,
                    "Maximum nesting level of {0} was exceeded",
                    maxEncodingNestingLevels);
            }
            m_nestingLevel++;
        }

        // The length of the DateTime string encoded by "o"
        internal const int DateTimeRoundTripKindLength = 28;
        // the index of the last digit which can be omitted if 0
        const int DateTimeRoundTripKindLastDigit = DateTimeRoundTripKindLength - 2;
        // the index of the first digit which can be omitted (7 digits total)
        const int DateTimeRoundTripKindFirstDigit = DateTimeRoundTripKindLastDigit - 7;

        /// <summary>
        /// Write Utc time in the format "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK".
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        internal static void ConvertUniversalTimeToString(DateTime value, Span<byte> valueUtf8String, out int bytesWritten)
        {
            // Note: "o" is a shortcut for "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK" and implicitly
            // uses invariant culture and gregorian calendar, but executes up to 10 times faster.
            // But in contrary to the explicit format string, trailing zeroes are not omitted!
            if (value.Kind != DateTimeKind.Utc)
            {
                value.ToUniversalTime().TryFormat(valueUtf8String, out bytesWritten, "o", CultureInfo.InvariantCulture);
            }
            else
            {
                value.TryFormat(valueUtf8String, out bytesWritten, "o", CultureInfo.InvariantCulture);
            }

            Debug.Assert(bytesWritten == DateTimeRoundTripKindLength);

            // check if trailing zeroes can be omitted
            int i = DateTimeRoundTripKindLastDigit;
            while (i > DateTimeRoundTripKindFirstDigit)
            {
                if (valueUtf8String[i] != (byte)'0')
                {
                    break;
                }
                i--;
            }

            if (i < DateTimeRoundTripKindLastDigit)
            {
                // check if the decimal point has to be removed too
                if (i == DateTimeRoundTripKindFirstDigit)
                {
                    i--;
                }
                valueUtf8String[i + 1] = (byte)'Z';
                bytesWritten = i + 2;
            }
        }
#else
        internal static string ConvertUniversalTimeToString(DateTime value)
        {
            // Note: "o" is a shortcut for "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK" and implicitly
            // uses invariant culture and gregorian calendar, but executes up to 10 times faster.
            // But in contrary to the explicit format string, trailing zeroes are not omitted!
            string valueString = value.ToUniversalTime().ToString("o");

            // check if trailing zeroes can be omitted
            int i = DateTimeRoundTripKindLastDigit;
            while (i > DateTimeRoundTripKindFirstDigit)
            {
                if (valueString[i] != '0')
                {
                    break;
                }
                i--;
            }

            if (i < DateTimeRoundTripKindLastDigit)
            {
                // check if the decimal point has to be removed too
                if (i == DateTimeRoundTripKindFirstDigit)
                {
                    i--;
                }
                valueString = valueString.Remove(i + 1, DateTimeRoundTripKindLastDigit - i);
            }

            return valueString;
        }
#endif
        #endregion
    }
}
