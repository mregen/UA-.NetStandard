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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;

namespace Opc.Ua
{
    /// <summary>
    /// A structure that could contain value with any of the UA built-in data types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Variant is described in <b>Part 6 - Mappings, Section 6.2.2.15</b>, titled <b>Variant</b>
    /// <br/></para>
    /// <para>
    /// Variant is a data type in COM, but not within the .NET Framework. Therefore OPC UA has its own
    /// Variant type that supports all of the OPC UA data-types.
    /// <br/></para>
    /// </remarks>
    [DataContract(Namespace = Namespaces.OpcUaXsd)]
    [StructLayout(LayoutKind.Explicit)]
    public struct Variant : ICloneable, IFormattable, IEquatable<Variant>
    {
        #region Constructors
        /// <summary>
        /// Creates a deep copy of the value.
        /// </summary>
        /// <remarks>
        /// Creates a new Variant instance, while deep-copying the contents of the specified Variant
        /// </remarks>
        /// <param name="value">The Variant value to copy.</param>
        public Variant(Variant value)
        {
            Set(Utils.Clone(value.Value), value.m_typeInfo);
        }

        /// <summary>
        /// Constructs a Variant
        /// </summary>
        /// <param name="value">The value to store.</param>
        /// <param name="typeInfo">The type information for the value.</param>
        public Variant(object value, TypeInfo typeInfo) : this(value, typeInfo, false)
        {
        }

        /// <summary>
        /// Constructs a Variant
        /// </summary>
        /// <param name="value">The value to store.</param>
        /// <param name="typeInfo">The type information for the value.</param>
        /// <param name="assignDirect">If the new value should be directly assigned without checks.</param>
        private Variant(object value, TypeInfo typeInfo, bool assignDirect)
        {
            m_typeInfo = typeInfo;
            if (assignDirect)
            {
                m_value = value;
                return;
            }

            m_value = null;
            Set(value, typeInfo);

#if DEBUG
            // no sanity check possible for null values
            value = Value;
            if (value == null)
            {
                return;
            }

            var sanityCheck = TypeInfo.Construct(value);

            // except special case byte array vs. bytestring
            if (sanityCheck.BuiltInType == BuiltInType.ByteString &&
                sanityCheck.ValueRank == ValueRanks.Scalar &&
                typeInfo.BuiltInType == BuiltInType.Byte &&
                typeInfo.ValueRank == ValueRanks.OneDimension)
            {
                return;
            }

            // An enumeration can contain Int32
            if (sanityCheck.BuiltInType == BuiltInType.Int32 &&
                typeInfo.BuiltInType == BuiltInType.Enumeration)
            {
                return;
            }

            System.Diagnostics.Debug.Assert(
                sanityCheck.BuiltInType == m_typeInfo.BuiltInType,
                Utils.Format("{0} != {1}",
                sanityCheck.BuiltInType,
                typeInfo.BuiltInType));

            System.Diagnostics.Debug.Assert(
                sanityCheck.ValueRank == m_typeInfo.ValueRank,
                Utils.Format("{0} != {1}",
                sanityCheck.ValueRank,
                typeInfo.ValueRank));

#endif
        }

        /// <summary>
        /// Constructs a Variant
        /// </summary>
        /// <param name="value">The value to store.</param>
        /// <param name="typeInfo">The type information for the value.</param>
        public Variant(Array value, TypeInfo typeInfo)
        {
            m_value = null;
            m_typeInfo = typeInfo;
            SetArray(value, typeInfo);
        }

        /// <summary>
        /// Initializes the object with an object value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant instance while specifying the value.
        /// </remarks>
        /// <param name="value">The value to encode within the variant</param>
        public Variant(object value)
        {
            m_value = null;
            m_typeInfo = TypeInfo.Construct(value);
            Set(value, m_typeInfo);
        }

        /// <summary>
        /// Initializes the variant with an Array value and the type information.
        /// </summary>
        public Variant(Array value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Construct(value);
            SetArray(value, m_typeInfo);
        }

        /// <summary>
        /// Initializes the variant with matrix.
        /// </summary>
        /// <param name="value">The value to store within the variant</param>
        public Variant(Matrix value)
        {
            m_value = value;
            m_typeInfo = value.TypeInfo;
        }

        /// <summary>
        /// Initializes the object with a bool value.
        /// </summary>
        /// <remarks>
        /// Creates a new Variant with a Boolean value.
        /// </remarks>
        /// <param name="value">The value of the variant</param>
        public Variant(bool value)
        {
            m_value = null;
            m_boolean = value;
            m_typeInfo = TypeInfo.Scalars.Boolean;
        }

        /// <summary>
        /// Initializes the object with a sbyte value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="sbyte"/> value
        /// </remarks>
        /// <param name="value">The <see cref="sbyte"/> value of the Variant</param>
        public Variant(sbyte value)
        {
            m_value = null;
            m_sbyte = value;
            m_typeInfo = TypeInfo.Scalars.SByte;
        }

        /// <summary>
        /// Initializes the object with a byte value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="byte"/> value
        /// </remarks>
        /// <param name="value">The <see cref="byte"/> value of the Variant</param>
        public Variant(byte value)
        {
            m_value = null;
            m_byte = value;
            m_typeInfo = TypeInfo.Scalars.Byte;
        }

        /// <summary>
        /// Initializes the object with a short value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="short"/> value
        /// </remarks>
        /// <param name="value">The <see cref="short"/> value of the Variant</param>
        public Variant(short value)
        {
            m_value = null;
            m_int16 = value;
            m_typeInfo = TypeInfo.Scalars.Int16;
        }

        /// <summary>
        /// Initializes the object with a ushort value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ushort"/> value
        /// </remarks>
        /// <param name="value">The <see cref="ushort"/> value of the Variant</param>
        public Variant(ushort value)
        {
            m_value = null;
            m_uint16 = value;
            m_typeInfo = TypeInfo.Scalars.UInt16;
        }

        /// <summary>
        /// Initializes the object with an int value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="int"/> value
        /// </remarks>
        /// <param name="value">The <see cref="int"/> value of the Variant</param>
        public Variant(int value)
        {
            m_value = null;
            m_int32 = value;
            m_typeInfo = TypeInfo.Scalars.Int32;
        }

        /// <summary>
        /// Initializes the object with a uint value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="uint"/> value
        /// </remarks>
        /// <param name="value">The <see cref="uint"/> value of the Variant</param>
        public Variant(uint value)
        {
            m_value = null;
            m_uint32 = value;
            m_typeInfo = TypeInfo.Scalars.UInt32;
        }

        /// <summary>
        /// Initializes the object with a long value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="long"/> value
        /// </remarks>
        /// <param name="value">The <see cref="long"/> value of the Variant</param>
        public Variant(long value)
        {
            m_value = null;
            m_int64 = value;
            m_typeInfo = TypeInfo.Scalars.Int64;
        }

        /// <summary>
        /// Initializes the object with a ulong value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ulong"/> value
        /// </remarks>
        /// <param name="value">The <see cref="ulong"/> value of the Variant</param>
        public Variant(ulong value)
        {
            m_value = null;
            m_uint64 = value;
            m_typeInfo = TypeInfo.Scalars.UInt64;
        }

        /// <summary>
        /// Initializes the object with a float value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="float"/> value
        /// </remarks>
        /// <param name="value">The <see cref="float"/> value of the Variant</param>
        public Variant(float value)
        {
            m_value = null;
            m_float = value;
            m_typeInfo = TypeInfo.Scalars.Float;
        }

        /// <summary>
        /// Initializes the object with a double value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="double"/> value
        /// </remarks>
        /// <param name="value">The <see cref="double"/> value of the Variant</param>
        public Variant(double value)
        {
            m_value = null;
            m_double = value;
            m_typeInfo = TypeInfo.Scalars.Double;
        }

        /// <summary>
        /// Initializes the object with a string value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="string"/> value
        /// </remarks>
        /// <param name="value">The <see cref="string"/> value of the Variant</param>
        public Variant(string value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.String;
        }

        /// <summary>
        /// Initializes the object with a DateTime value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="DateTime"/> value
        /// </remarks>
        /// <param name="value">The <see cref="DateTime"/> value of the Variant</param>
        public Variant(DateTime value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.DateTime;
        }

        /// <summary>
        /// Initializes the object with a Guid value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="Guid"/> value
        /// </remarks>
        /// <param name="value">The <see cref="Guid"/> value of the Variant</param>
        public Variant(Guid value)
        {
            m_value = new Uuid(value);
            m_typeInfo = TypeInfo.Scalars.Guid;
        }

        /// <summary>
        /// Initializes the object with a Uuid value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="Uuid"/> value
        /// </remarks>
        /// <param name="value">The <see cref="Uuid"/> value of the Variant</param>
        public Variant(Uuid value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.Guid;
        }

        /// <summary>
        /// Initializes the object with a byte[] value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="byte"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="byte"/>-array value of the Variant</param>
        public Variant(byte[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.ByteString;
        }

        /// <summary>
        /// Initializes the object with a XmlElement value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="XmlElement"/> value
        /// </remarks>
        /// <param name="value">The <see cref="XmlElement"/> value of the Variant</param>
        public Variant(XmlElement value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.XmlElement;
        }

        /// <summary>
        /// Initializes the object with a NodeId value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="NodeId"/> value
        /// </remarks>
        /// <param name="value">The <see cref="NodeId"/> value of the Variant</param>
        public Variant(NodeId value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.NodeId;
        }

        /// <summary>
        /// Initializes the object with a ExpandedNodeId value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ExpandedNodeId"/> value
        /// </remarks>
        /// <param name="value">The <see cref="ExpandedNodeId"/> value of the Variant</param>
        public Variant(ExpandedNodeId value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.ExpandedNodeId;
        }

        /// <summary>
        /// Initializes the object with a StatusCode value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="StatusCode"/> value
        /// </remarks>
        /// <param name="value">The <see cref="StatusCode"/> value of the Variant</param>
        public Variant(StatusCode value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.StatusCode;
        }

        /// <summary>
        /// Initializes the object with a QualifiedName value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="QualifiedName"/> value
        /// </remarks>
        /// <param name="value">The <see cref="QualifiedName"/> value of the Variant</param>
        public Variant(QualifiedName value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.QualifiedName;
        }

        /// <summary>
        /// Initializes the object with a LocalizedText value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="LocalizedText"/> value
        /// </remarks>
        /// <param name="value">The <see cref="LocalizedText"/> value of the Variant</param>
        public Variant(LocalizedText value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.LocalizedText;
        }

        /// <summary>
        /// Initializes the object with a ExtensionObject value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ExtensionObject"/> value
        /// </remarks>
        /// <param name="value">The <see cref="ExtensionObject"/> value of the Variant</param>
        public Variant(ExtensionObject value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.ExtensionObject;
        }

        /// <summary>
        /// Initializes the object with a DataValue value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="DataValue"/> value
        /// </remarks>
        /// <param name="value">The <see cref="DataValue"/> value of the Variant</param>
        public Variant(DataValue value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Scalars.DataValue;
        }

        /// <summary>
        /// Initializes the object with a bool array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="bool"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="bool"/>-array value of the Variant</param>
        public Variant(bool[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Boolean;
        }

        /// <summary>
        /// Initializes the object with a sbyte array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="sbyte"/>-arrat value
        /// </remarks>
        /// <param name="value">The <see cref="sbyte"/>-array value of the Variant</param>
        public Variant(sbyte[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.SByte;
        }

        /// <summary>
        /// Initializes the object with a short array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="short"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="short"/>-array value of the Variant</param>
        public Variant(short[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Int16;
        }

        /// <summary>
        /// Initializes the object with a ushort array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ushort"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="ushort"/>-array value of the Variant</param>
        public Variant(ushort[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.UInt16;
        }

        /// <summary>
        /// Initializes the object with an int array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="int"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="int"/>-array value of the Variant</param>
        public Variant(int[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Int32;
        }

        /// <summary>
        /// Initializes the object with a uint array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="uint"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="uint"/>-array value of the Variant</param>
        public Variant(uint[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.UInt32;
        }

        /// <summary>
        /// Initializes the object with a long array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="long"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="long"/>-array value of the Variant</param>
        public Variant(long[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Int64;
        }

        /// <summary>
        /// Initializes the object with a ulong array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ulong"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="ulong"/>-array value of the Variant</param>
        public Variant(ulong[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.UInt64;
        }

        /// <summary>
        /// Initializes the object with a float array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="float"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="float"/>-array value of the Variant</param>
        public Variant(float[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Float;
        }

        /// <summary>
        /// Initializes the object with a double array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="double"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="double"/>-array value of the Variant</param>
        public Variant(double[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Double;
        }

        /// <summary>
        /// Initializes the object with a string array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="string"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="string"/>-array value of the Variant</param>
        public Variant(string[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.String;
        }

        /// <summary>
        /// Initializes the object with a DateTime array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="DateTime"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="DateTime"/>-array value of the Variant</param>
        public Variant(DateTime[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.DateTime;
        }

        /// <summary>
        /// Initializes the object with a Guid array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="Guid"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="Guid"/>-array value of the Variant</param>
        public Variant(Guid[] value)
        {
            m_value = null;
            m_typeInfo = TypeInfo.Arrays.Guid;
            Set(value);
        }

        /// <summary>
        /// Initializes the object with a Uuid array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="Uuid"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="Uuid"/>-array value of the Variant</param>
        public Variant(Uuid[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Guid;
        }

        /// <summary>
        /// Initializes the object with a byte[] array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a 2-d <see cref="byte"/>-array value
        /// </remarks>
        /// <param name="value">The 2-d <see cref="byte"/>-array value of the Variant</param>
        public Variant(byte[][] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.ByteString;
        }

        /// <summary>
        /// Initializes the object with a XmlElement array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="XmlElement"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="XmlElement"/>-array value of the Variant</param>
        public Variant(XmlElement[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.XmlElement;
        }

        /// <summary>
        /// Initializes the object with a NodeId array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="NodeId"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="NodeId"/>-array value of the Variant</param>
        public Variant(NodeId[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.NodeId;
        }

        /// <summary>
        /// Initializes the object with a ExpandedNodeId array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ExpandedNodeId"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="ExpandedNodeId"/>-array value of the Variant</param>
        public Variant(ExpandedNodeId[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.ExpandedNodeId;
        }

        /// <summary>
        /// Initializes the object with a StatusCode array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="StatusCode"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="StatusCode"/>-array value of the Variant</param>
        public Variant(StatusCode[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.StatusCode;
        }

        /// <summary>
        /// Initializes the object with a QualifiedName array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="QualifiedName"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="QualifiedName"/>-array value of the Variant</param>
        public Variant(QualifiedName[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.QualifiedName;
        }

        /// <summary>
        /// Initializes the object with a LocalizedText array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="LocalizedText"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="LocalizedText"/>-array value of the Variant</param>
        public Variant(LocalizedText[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.LocalizedText;
        }

        /// <summary>
        /// Initializes the object with a ExtensionObject array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="ExtensionObject"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="ExtensionObject"/>-array value of the Variant</param>
        public Variant(ExtensionObject[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.ExtensionObject;
        }

        /// <summary>
        /// Initializes the object with a DataValue array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="DataValue"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="DataValue"/>-array value of the Variant</param>
        public Variant(DataValue[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.DataValue;
        }

        /// <summary>
        /// Initializes the object with a Variant array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="Variant"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="Variant"/>-array value of the Variant</param>
        public Variant(Variant[] value)
        {
            m_value = value;
            m_typeInfo = TypeInfo.Arrays.Variant;
        }

        /// <summary>
        /// Initializes the object with an object array value.
        /// </summary>
        /// <remarks>
        /// Creates a new variant with a <see cref="object"/>-array value
        /// </remarks>
        /// <param name="value">The <see cref="object"/>-array value of the Variant</param>
        public Variant(object[] value)
        {
            m_value = null;
            m_typeInfo = TypeInfo.Arrays.Variant;
            Set(value);
        }

        /// <summary>
        /// Private set struct to default values and type.
        /// </summary>
        private Variant(TypeInfo typeInfo)
        {
            m_value = null;
            m_typeInfo = typeInfo;
        }

        /// <summary>
        /// Initializes the object and value type with a bool value.
        /// </summary>
        private Variant(object value, bool boolValue)
        {
            m_value = value;
            m_boolean = boolValue;
            m_typeInfo = TypeInfo.Scalars.Boolean;
        }

        /// <summary>
        /// Initializes the object and value type with a sbyte value.
        /// </summary>
        private Variant(object value, sbyte sbyteValue)
        {
            m_value = value;
            m_sbyte = sbyteValue;
            m_typeInfo = TypeInfo.Scalars.SByte;
        }

        /// <summary>
        /// Initializes the object and value type with a byte value.
        /// </summary>
        private Variant(object value, byte byteValue)
        {
            m_value = value;
            m_byte = byteValue;
            m_typeInfo = TypeInfo.Scalars.Byte;
        }

        /// <summary>
        /// Initializes the object and value type with a short value.
        /// </summary>
        private Variant(object value, short shortValue)
        {
            m_value = value;
            m_int16 = shortValue;
            m_typeInfo = TypeInfo.Scalars.Int16;
        }

        /// <summary>
        /// Initializes the object and value type with an ushort value.
        /// </summary>
        private Variant(object value, ushort ushortValue)
        {
            m_value = value;
            m_uint16 = ushortValue;
            m_typeInfo = TypeInfo.Scalars.UInt16;
        }

        /// <summary>
        /// Initializes the object and value type with an int value.
        /// </summary>
        private Variant(object value, int intValue)
        {
            m_value = value;
            m_int32 = intValue;
            m_typeInfo = TypeInfo.Scalars.Int32;
        }

        /// <summary>
        /// Initializes the object and value type with an uint value.
        /// </summary>
        private Variant(object value, uint uintValue)
        {
            m_value = value;
            m_uint32 = uintValue;
            m_typeInfo = TypeInfo.Scalars.UInt32;
        }

        /// <summary>
        /// Initializes the object and value type with a long value.
        /// </summary>
        private Variant(object value, long longValue)
        {
            m_value = value;
            m_int64 = longValue;
            m_typeInfo = TypeInfo.Scalars.Int64;
        }

        /// <summary>
        /// Initializes the object and value type with an ulong value.
        /// </summary>
        private Variant(object value, ulong ulongValue)
        {
            m_value = value;
            m_uint64 = ulongValue;
            m_typeInfo = TypeInfo.Scalars.UInt64;
        }

        /// <summary>
        /// Initializes the object and value type with a float value.
        /// </summary>
        private Variant(object value, float floatValue)
        {
            m_value = value;
            m_float = floatValue;
            m_typeInfo = TypeInfo.Scalars.Float;
        }

        /// <summary>
        /// Initializes the object and value type with a double value.
        /// </summary>
        private Variant(object value, double doubleValue)
        {
            m_value = value;
            m_double = doubleValue;
            m_typeInfo = TypeInfo.Scalars.Double;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The value stored in the object.
        /// </summary>
        /// <remarks>
        /// The value stored within the Variant object.
        /// </remarks>
        [DataMember(Name = "Value", Order = 1)]
        private XmlElement XmlEncodedValue
        {
            get
            {
                // create encoder.
                using (XmlEncoder encoder = new XmlEncoder(MessageContextExtension.CurrentContext))
                {
                    // write value.
                    encoder.WriteVariantContents(this);

                    // create document from encoder.
                    XmlDocument document = new XmlDocument();
                    document.LoadInnerXml(encoder.CloseAndReturnText());

                    // return element.
                    return document.DocumentElement;
                }
            }

            set
            {
                // check for null values.
                if (value == null)
                {
                    this = Variant.Null;
                    return;
                }

                // create decoder.
                using (XmlDecoder decoder = new XmlDecoder(value, MessageContextExtension.CurrentContext))
                {
                    try
                    {
                        // read value.
                        object body = decoder.ReadVariantContents(out TypeInfo typeInfo);
                        Set(body, typeInfo);
                    }
                    catch (Exception e)
                    {
                        throw ServiceResultException.Create(
                            StatusCodes.BadDecodingError,
                            e,
                            "Error decoding Variant value.");
                    }
                    finally
                    {
                        // close decoder.
                        decoder.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Whether the BuiltInType is stored as a value type.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValueType()
        {
            return
                m_typeInfo != null &&
                m_typeInfo.ValueRank == ValueRanks.Scalar &&
                m_typeInfo.BuiltInType >= BuiltInType.Boolean &&
                m_typeInfo.BuiltInType <= BuiltInType.Double;
        }

        /// <summary>
        /// The value stored in the object.
        /// </summary>
        /// <remarks>
        /// The value stored -as <see cref="Object"/>- within the Variant object.
        /// </remarks>
        public object Value
        {
            get
            {
                if (m_value == null &&
                    m_typeInfo != null && m_typeInfo.ValueRank == ValueRanks.Scalar)
                {
                    // If this codepath is hit, the advantage of storing the value directly
                    // in the struct is lost. We implicitly allocate a new System.Object
                    // to store and return the value as object.
                    // To avoid multiple allocations set the m_value object once.
                    // This assignment is also the reason why m_value is not readonly.
                    switch (m_typeInfo.BuiltInType)
                    {
                        case BuiltInType.Null: break;
                        case BuiltInType.Boolean: m_value = m_boolean; break;
                        case BuiltInType.SByte: m_value = m_sbyte; break;
                        case BuiltInType.Byte: m_value = m_byte; break;
                        case BuiltInType.Int16: m_value = m_int16; break;
                        case BuiltInType.UInt16: m_value = m_uint16; break;
                        case BuiltInType.Int32: m_value = m_int32; break;
                        case BuiltInType.UInt32: m_value = m_uint32; break;
                        case BuiltInType.Int64: m_value = m_int64; break;
                        case BuiltInType.UInt64: m_value = m_uint64; break;
                        case BuiltInType.Float: m_value = m_float; break;
                        case BuiltInType.Double: m_value = m_double; break;
                    }
                }
                return m_value;
            }
        }

        /// <summary>
        /// The Boolean value stored as ValueType.
        /// Calling code must ensure that the BuiltInType is Boolean and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Boolean"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public bool ValueBoolean
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Boolean) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_boolean;
            }
        }

        /// <summary>
        /// The SByte value stored in the object.
        /// Calling code must ensure that the BuiltInType is SByte and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.SByte"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public sbyte ValueSByte
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.SByte) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_sbyte;
            }
        }

        /// <summary>
        /// The Byte value stored in the object.
        /// Calling code must ensure that the BuiltInType is Byte and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Byte"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public byte ValueByte
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Byte) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_byte;
            }
        }

        /// <summary>
        /// The Int16 value stored in the object.
        /// Calling code must ensure that the BuiltInType is int16 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Int16"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public short ValueInt16
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Int16) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_int16;
            }
        }

        /// <summary>
        /// The UInt16 value stored in the object.
        /// Calling code must ensure that the BuiltInType is UInt16 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.UInt16"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public ushort ValueUInt16
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.UInt16) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_uint16;
            }
        }

        /// <summary>
        /// The Int32 value stored in the object.
        /// Calling code must ensure that the BuiltInType is Int32 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Int32"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public int ValueInt32
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Int32) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_int32;
            }
        }

        /// <summary>
        /// The UInt32 value stored in the object.
        /// Calling code must ensure that the BuiltInType is UInt32 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.UInt32"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public uint ValueUInt32
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.UInt32) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_uint32;
            }
        }

        /// <summary>
        /// The Int64 value stored in the object.
        /// Calling code must ensure that the BuiltInType is Int64 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Int64"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public long ValueInt64
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Int64) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_int64;
            }
        }

        /// <summary>
        /// The UInt64 value stored in the object.
        /// Calling code must ensure that the BuiltInType is UInt64 and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.UInt64"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public ulong ValueUInt64
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.UInt64) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_uint64;
            }
        }

        /// <summary>
        /// The float value stored in the object.
        /// Calling code must ensure that the BuiltInType is Float and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Float"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public float ValueFloat
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Float) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_float;
            }
        }

        /// <summary>
        /// The Double value stored in the object.
        /// Calling code must ensure that the BuiltInType is Double and ValueRank is Scalar.
        /// </summary>
        /// <remarks>
        /// In DEBUG build, this property will throw an exception if the <see cref="BuiltInType"/>
        /// is not a <see cref="BuiltInType.Double"/> or if the value rank is not a <see cref="ValueRanks.Scalar"/>.
        /// </remarks>
        [JsonIgnore]
        public double ValueDouble
        {
            get
            {
#if DEBUG
                if (!(m_typeInfo?.BuiltInType == BuiltInType.Double) || m_typeInfo.ValueRank != ValueRanks.Scalar)
                {
                    throw ThrowOnFailedDirectAccess();
                }
#endif
                return m_double;
            }
        }

        /// <summary>
        /// The type information for the Variant.
        /// </summary>
        public readonly TypeInfo TypeInfo => m_typeInfo;
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <remarks>
        /// Returns the string representation of the object.
        /// </remarks>
        /// <exception cref="FormatException">Thrown when the 'format' argument is NOT null.</exception>
        /// <param name="format">(Unused) Always pass a NULL value</param>
        /// <param name="formatProvider">The format-provider to use. If unsure, pass an empty string or null</param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                StringBuilder buffer = new StringBuilder();
                AppendFormat(buffer, Value, formatProvider);
                return buffer.ToString();
            }

            throw new FormatException(Utils.Format("Invalid format string: '{0}'.", format));
        }

        /// <summary>
        /// Append a ByteString as a hex string.
        /// </summary>
        private static void AppendByteString(StringBuilder buffer, byte[] bytes, IFormatProvider formatProvider)
        {
            if (bytes != null)
            {
                for (int ii = 0; ii < bytes.Length; ii++)
                {
                    buffer.AppendFormat(formatProvider, "{0:X2}", bytes[ii]);
                }
            }
            else
            {
                buffer.Append("(null)");
            }
        }

        /// <summary>
        /// Formats a value as a string.
        /// </summary>
        private void AppendFormat(StringBuilder buffer, object value, IFormatProvider formatProvider)
        {
            // check for null.
            if (value == null || m_typeInfo == null)
            {
                buffer.Append("(null)");
                return;
            }

            // convert byte string to hexstring.
            if (m_typeInfo.BuiltInType == BuiltInType.ByteString && m_typeInfo.ValueRank < 0)
            {
                byte[] bytes = (byte[])value;
                Variant.AppendByteString(buffer, bytes, formatProvider);
                return;
            }

            // convert XML element to string.
            if (m_typeInfo.BuiltInType == BuiltInType.XmlElement && m_typeInfo.ValueRank < 0)
            {
                XmlElement xml = (XmlElement)value;
                buffer.AppendFormat(formatProvider, "{0}", xml.OuterXml);
                return;
            }

            // recusrively write individual elements of an array.
            if (value is Array array && m_typeInfo.ValueRank <= 1)
            {
                buffer.Append('{');

                if (m_typeInfo.BuiltInType == BuiltInType.ByteString)
                {
                    if (array.Length > 0)
                    {
                        byte[] bytes = (byte[])array.GetValue(0);
                        Variant.AppendByteString(buffer, bytes, formatProvider);
                    }

                    for (int ii = 1; ii < array.Length; ii++)
                    {
                        buffer.Append('|');
                        byte[] bytes = (byte[])array.GetValue(ii);
                        Variant.AppendByteString(buffer, bytes, formatProvider);
                    }
                }
                else
                {
                    if (array.Length > 0)
                    {
                        AppendFormat(buffer, array.GetValue(0), formatProvider);
                    }

                    for (int ii = 1; ii < array.Length; ii++)
                    {
                        buffer.Append('|');
                        AppendFormat(buffer, array.GetValue(ii), formatProvider);
                    }
                }
                buffer.Append('}');
                return;
            }

            // let the object format itself.
            buffer.AppendFormat(formatProvider, "{0}", value);
        }
        #endregion

        #region ICloneable Members
        /// <inheritdoc/>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Makes a deep copy of the object.
        /// </summary>
        public new object MemberwiseClone()
        {
            return new Variant(Utils.Clone(this.Value));
        }
        #endregion

        #region Static Operators
        /// <summary>
        /// Returns true if the objects are not equal.
        /// </summary>
        public static bool operator ==(Variant a, Variant b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Returns true if the objects are not equal.
        /// </summary>
        public static bool operator !=(Variant a, Variant b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Converts a bool value to an Variant object.
        /// </summary>
        public static implicit operator Variant(bool value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a sbyte value to an Variant object.
        /// </summary>
        public static implicit operator Variant(sbyte value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a byte value to an Variant object.
        /// </summary>
        public static implicit operator Variant(byte value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a short value to an Variant object.
        /// </summary>
        public static implicit operator Variant(short value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ushort value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ushort value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a int value to an Variant object.
        /// </summary>
        public static implicit operator Variant(int value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a uint value to an Variant object.
        /// </summary>
        public static implicit operator Variant(uint value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a long value to an Variant object.
        /// </summary>
        public static implicit operator Variant(long value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ulong value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ulong value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a float value to an Variant object.
        /// </summary>
        public static implicit operator Variant(float value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a double value to an Variant object.
        /// </summary>
        public static implicit operator Variant(double value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a string value to an Variant object.
        /// </summary>
        public static implicit operator Variant(string value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a DateTime value to an Variant object.
        /// </summary>
        public static implicit operator Variant(DateTime value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a Guid value to an Variant object.
        /// </summary>
        public static implicit operator Variant(Guid value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a Uuid value to an Variant object.
        /// </summary>
        public static implicit operator Variant(Uuid value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a byte[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(byte[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a XmlElement value to an Variant object.
        /// </summary>
        public static implicit operator Variant(XmlElement value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a NodeId value to an Variant object.
        /// </summary>
        public static implicit operator Variant(NodeId value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ExpandedNodeId value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ExpandedNodeId value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a StatusCode value to an Variant object.
        /// </summary>
        public static implicit operator Variant(StatusCode value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a QualifiedName value to an Variant object.
        /// </summary>
        public static implicit operator Variant(QualifiedName value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a LocalizedText value to an Variant object.
        /// </summary>
        public static implicit operator Variant(LocalizedText value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ExtensionObject value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ExtensionObject value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a DataValue value to an Variant object.
        /// </summary>
        public static implicit operator Variant(DataValue value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a bool[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(bool[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a sbyte[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(sbyte[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a short[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(short[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ushort[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ushort[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a int[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(int[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a uint[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(uint[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a long[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(long[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ulong[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ulong[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a float[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(float[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a double[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(double[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a string []value to an Variant object.
        /// </summary>
        public static implicit operator Variant(string[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a DateTime[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(DateTime[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a Guid[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(Guid[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a Uuid[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(Uuid[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a byte[][] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(byte[][] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a XmlElement[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(XmlElement[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a NodeId[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(NodeId[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ExpandedNodeId[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ExpandedNodeId[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a StatusCode[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(StatusCode[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a QualifiedName[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(QualifiedName[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a LocalizedText[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(LocalizedText[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a ExtensionObject[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(ExtensionObject[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a DataValue[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(DataValue[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts a Variant[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(Variant[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Converts an object[] value to an Variant object.
        /// </summary>
        public static implicit operator Variant(object[] value)
        {
            return new Variant(value);
        }

        /// <summary>
        /// Determines if the specified Variant is equal to this Variant.
        /// Implements <see cref="IEquatable{Variant}.Equals(Variant)"/>.
        /// </summary>
        public bool Equals(Variant other)
        {
            bool isNull = this.IsNull;
            bool otherIsNull = other.IsNull;
            if (isNull && otherIsNull)
            {
                return true;
            }

            if (isNull || otherIsNull)
            {
                return false;
            }

            // here both TypeInfo are not null
            Debug.Assert(this.m_typeInfo != null || other.m_typeInfo != null);

            if (AliasedBuiltInType(this.m_typeInfo.BuiltInType) != AliasedBuiltInType(other.m_typeInfo.BuiltInType) ||
                this.m_typeInfo.ValueRank != other.m_typeInfo.ValueRank)
            {
                return false;
            }

            if (this.IsValueType())
            {
                switch (this.m_typeInfo.BuiltInType)
                {
                    case BuiltInType.Boolean: return m_boolean == other.m_boolean;
                    case BuiltInType.SByte: return m_sbyte == other.m_sbyte;
                    case BuiltInType.Byte: return m_byte == other.m_byte;
                    case BuiltInType.Int16: return m_int16 == other.m_int16;
                    case BuiltInType.UInt16: return m_uint16 == other.m_uint16;
                    case BuiltInType.Int32: return m_int32 == other.m_int32;
                    case BuiltInType.UInt32: return m_uint32 == other.m_uint32;
                    case BuiltInType.Int64: return m_int64 == other.m_int64;
                    case BuiltInType.UInt64: return m_uint64 == other.m_uint64;
                    case BuiltInType.Float: return m_float == other.m_float;
                    case BuiltInType.Double: return m_double == other.m_double;
                    default: return false;
                }
            }

            return Utils.IsEqual(Value, other.Value);
        }
        #endregion

        #region Static Fields and Methods
        /// <summary>
        /// A constant containing a null Variant structure.
        /// </summary>
        public static readonly Variant Null = new Variant();

        /// <summary>
        /// Returns if the Variant is a Null value.
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (!this.IsValueType())
                {
                    return this.m_value == null;
                }

                return false;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Determines if the specified object is equal to the object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Variant variant)
            {
                return Equals((Variant)variant);
            }

            return false;
        }

        /// <summary>
        /// Returns a unique hashcode for the object.
        /// </summary>
        public override int GetHashCode()
        {
            if (IsNull)
            {
                return 0;
            }

            HashCode hash = new HashCode();
            if (m_typeInfo != null)
            {
                hash.Add(AliasedBuiltInType(m_typeInfo.BuiltInType));
                hash.Add(m_typeInfo.ValueRank);
            }

            if (IsValueType())
            {
                switch (m_typeInfo.BuiltInType)
                {
                    case BuiltInType.Boolean: hash.Add(m_boolean); break;
                    case BuiltInType.SByte: hash.Add(m_sbyte); break;
                    case BuiltInType.Byte: hash.Add(m_byte); break;
                    case BuiltInType.Int16: hash.Add(m_int16); break;
                    case BuiltInType.UInt16: hash.Add(m_uint16); break;
                    case BuiltInType.Int32: hash.Add(m_int32); break;
                    case BuiltInType.UInt32: hash.Add(m_uint32); break;
                    case BuiltInType.Int64: hash.Add(m_int64); break;
                    case BuiltInType.UInt64: hash.Add(m_uint64); break;
                    case BuiltInType.Float: hash.Add(m_float); break;
                    case BuiltInType.Double: hash.Add(m_double); break;
                }
                return hash.ToHashCode();
            }

            object value = Value;
            if (value != null)
            {
                hash.Add(value);
            }

            return hash.ToHashCode();
        }

        /// <summary>
        /// Converts the value to a human readable string.
        /// </summary>
        public override string ToString()
        {
            return ToString(null, null);
        }
        #endregion

        #region Private Set Methods
        /// <summary>
        /// Initializes the object with a bool value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="bool"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="bool"/> value to set this Variant to</param>
        private void Set(bool value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a sbyte value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="sbyte"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="sbyte"/> value to set this Variant to</param>
        private void Set(sbyte value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a byte value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="byte"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="byte"/> value to set this Variant to</param>
        private void Set(byte value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a short value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="short"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="short"/> value to set this Variant to</param>
        private void Set(short value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ushort value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ushort"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="ushort"/> value to set this Variant to</param>
        private void Set(ushort value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with an int value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="int"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="int"/> value to set this Variant to</param>
        private void Set(int value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a uint value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="uint"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="uint"/> value to set this Variant to</param>
        private void Set(uint value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a long value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="long"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="long"/> value to set this Variant to</param>
        private void Set(long value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ulong value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ulong"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="ulong"/> value to set this Variant to</param>
        private void Set(ulong value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a float value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="float"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="float"/> value to set this Variant to</param>
        private void Set(float value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a double value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="double"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="double"/> value to set this Variant to</param>
        private void Set(double value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a string value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="string"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="string"/> value to set this Variant to</param>
        private void Set(string value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a DateTime value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="DateTime"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="DateTime"/> value to set this Variant to</param>
        private void Set(DateTime value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a Guid value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="Guid"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="Guid"/> value to set this Variant to</param>
        private void Set(Guid value)
        {
            this = new Variant(new Uuid(value));
        }

        /// <summary>
        /// Initializes the object with a Uuid value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="Uuid"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="Uuid"/> value to set this Variant to</param>
        private void Set(Uuid value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a byte[] value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="byte"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="byte"/>-array value to set this Variant to</param>
        private void Set(byte[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a XmlElement value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="XmlElement"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="XmlElement"/> value to set this Variant to</param>
        private void Set(XmlElement value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a NodeId value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="NodeId"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="NodeId"/> value to set this Variant to</param>
        private void Set(NodeId value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ExpandedNodeId value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ExpandedNodeId"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="ExpandedNodeId"/> value to set this Variant to</param>
        private void Set(ExpandedNodeId value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a StatusCode value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="StatusCode"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="StatusCode"/> value to set this Variant to</param>
        private void Set(StatusCode value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a QualifiedName value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="QualifiedName"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="QualifiedName"/> value to set this Variant to</param>
        private void Set(QualifiedName value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a LocalizedText value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="LocalizedText"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="LocalizedText"/> value to set this Variant to</param>
        private void Set(LocalizedText value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ExtensionObject value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ExtensionObject"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="ExtensionObject"/> value to set this Variant to</param>
        private void Set(ExtensionObject value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a DataValue value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="DataValue"/> value.
        /// </remarks>
        /// <param name="value">The <see cref="DataValue"/> value to set this Variant to</param>
        private void Set(DataValue value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a bool array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="bool"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="bool"/>-array value to set this Variant to</param>
        private void Set(bool[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a sbyte array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="sbyte"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="sbyte"/>-array value to set this Variant to</param>
        private void Set(sbyte[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a short array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="short"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="short"/>-array value to set this Variant to</param>
        private void Set(short[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ushort array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ushort"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="ushort"/>-array value to set this Variant to</param>
        private void Set(ushort[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with an int array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="int"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="int"/>-array value to set this Variant to</param>
        private void Set(int[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a uint array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="uint"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="uint"/>-array value to set this Variant to</param>
        private void Set(uint[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a long array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="long"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="long"/>-array value to set this Variant to</param>
        private void Set(long[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ulong array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ulong"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="ulong"/>-array value to set this Variant to</param>
        private void Set(ulong[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a float array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="float"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="float"/>-array value to set this Variant to</param>
        private void Set(float[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a double array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="double"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="double"/>-array value to set this Variant to</param>
        private void Set(double[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a string array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="string"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="string"/>-array value to set this Variant to</param>
        private void Set(string[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a DateTime array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="DateTime"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="DateTime"/>-array value to set this Variant to</param>
        private void Set(DateTime[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a Guid array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="Guid"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="Guid"/>-array value to set this Variant to</param>
        private void Set(Guid[] value)
        {
            if (value != null)
            {
                Uuid[] uuids = new Uuid[value.Length];

                for (int ii = 0; ii < value.Length; ii++)
                {
                    uuids[ii] = new Uuid(value[ii]);
                }

                this = new Variant(uuids);
            }
            else
            {
                this = new Variant((Uuid[])null);
            }
        }

        /// <summary>
        /// Initializes the object with a Uuid array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="Uuid"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="Uuid"/>-array value to set this Variant to</param>
        private void Set(Uuid[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a byte[] array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a 2-d <see cref="byte"/>-array value.
        /// </remarks>
        /// <param name="value">The 2-d <see cref="byte"/>-array value to set this Variant to</param>
        private void Set(byte[][] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a XmlElement array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="XmlElement"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="XmlElement"/>-array value to set this Variant to</param>
        private void Set(XmlElement[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a NodeId array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="NodeId"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="NodeId"/>-array value to set this Variant to</param>
        private void Set(NodeId[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ExpandedNodeId array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ExpandedNodeId"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="ExpandedNodeId"/>-array value to set this Variant to</param>
        private void Set(ExpandedNodeId[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a StatusCode array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="StatusCode"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="StatusCode"/>-array value to set this Variant to</param>
        private void Set(StatusCode[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a QualifiedName array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="QualifiedName"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="QualifiedName"/>-array value to set this Variant to</param>
        private void Set(QualifiedName[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a LocalizedText array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="LocalizedText"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="LocalizedText"/>-array value to set this Variant to</param>
        private void Set(LocalizedText[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a ExtensionObject array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="ExtensionObject"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="ExtensionObject"/>-array value to set this Variant to</param>
        private void Set(ExtensionObject[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a DataValue array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="DataValue"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="DataValue"/>-array value to set this Variant to</param>
        private void Set(DataValue[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with a Variant array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with a <see cref="Variant"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="Variant"/>-array value to set this Variant to</param>
        private void Set(Variant[] value)
        {
            this = new Variant(value);
        }

        /// <summary>
        /// Initializes the object with an object array value.
        /// </summary>
        /// <remarks>
        /// Initializes the object with an <see cref="object"/>-array value.
        /// </remarks>
        /// <param name="value">The <see cref="object"/>-array value to set this Variant to</param>
        private void Set(object[] value)
        {
            if (value != null)
            {
                Variant[] anyValues = new Variant[value.Length];

                for (int ii = 0; ii < value.Length; ii++)
                {
                    anyValues[ii] = new Variant(value[ii]);
                }

                this = new Variant(anyValues);
            }
            else
            {
                this = new Variant((Variant[])null);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the aliased <see cref="BuiltInType"/> for compares and hashcode calculations..
        /// </summary>
        /// <remarks>
        /// <see cref="BuiltInType.Enumeration"/> is treated as <see cref="BuiltInType.Int32"/>.
        /// </remarks>
        private static BuiltInType AliasedBuiltInType(BuiltInType builtInType)
        {
            return builtInType == BuiltInType.Enumeration ? BuiltInType.Int32 : builtInType;
        }

        /// <summary>
        /// Stores a scalar value in the variant.
        /// </summary>
        private void SetScalar(object value, TypeInfo typeInfo)
        {
            switch (typeInfo.BuiltInType)
            {
                // handle special types that can be converted to something the variant supports.
                case BuiltInType.Null:
                {
                    // check for enumerated value.
                    if (value.GetType().GetTypeInfo().IsEnum)
                    {
                        Set(Convert.ToInt32(value, CultureInfo.InvariantCulture));
                        return;
                    }

                    // check for matrix
                    if (value is Matrix matrix)
                    {
                        this = new Variant(matrix);
                        return;
                    }

                    // not supported.
                    throw new ServiceResultException(
                        StatusCodes.BadNotSupported,
                        Utils.Format("The type '{0}' cannot be stored in a Variant object.", value.GetType().FullName));
                }

                // convert Guids to Uuids.
                case BuiltInType.Guid:
                {
                    var guid = value as Guid?;
                    if (guid != null)
                    {
                        this = new Variant(new Uuid(guid.Value));
                        return;
                    }

                    this = new Variant((Uuid)value);
                    return;
                }

                // convert encodeables to extension objects.
                case BuiltInType.ExtensionObject:
                {
                    if (value is IEncodeable encodeable)
                    {
                        this = new Variant(new ExtensionObject(encodeable));
                        return;
                    }

                    this = new Variant(value, typeInfo, true);
                    return;
                }

                // convert encodeables to extension objects.
                case BuiltInType.Variant:
                {
                    this = new Variant(((Variant)value).Value);
                    return;
                }

                case BuiltInType.Boolean:
                {
                    this = new Variant(value, (bool)value);
                    return;
                }

                case BuiltInType.Byte:
                {
                    this = new Variant(value, (byte)value);
                    return;
                }

                case BuiltInType.SByte:
                {
                    this = new Variant(value, (sbyte)value);
                    return;
                }

                case BuiltInType.Int16:
                {
                    this = new Variant(value, (short)value);
                    return;
                }

                case BuiltInType.UInt16:
                {
                    this = new Variant(value, (ushort)value);
                    return;
                }

                case BuiltInType.Int32:
                {
                    this = new Variant(value, (int)value);
                    return;
                }

                case BuiltInType.UInt32:
                {
                    this = new Variant(value, (uint)value);
                    return;
                }

                case BuiltInType.Int64:
                {
                    this = new Variant(value, (long)value);
                    return;
                }

                case BuiltInType.UInt64:
                {
                    this = new Variant(value, (ulong)value);
                    return;
                }

                case BuiltInType.Float:
                {
                    this = new Variant(value, (float)value);
                    return;
                }

                case BuiltInType.Double:
                {
                    this = new Variant(value, (double)value);
                    return;
                }

                // just save the value.
                default:
                {
                    this = new Variant(value, typeInfo, true);
                    return;
                }
            }
        }

        /// <summary>
        /// Stores a on dimensional array value in the variant.
        /// </summary>
        private void SetArray(Array array, TypeInfo typeInfo)
        {
            switch (typeInfo.BuiltInType)
            {
                // handle special types that can be converted to something the variant supports.
                case BuiltInType.Null:
                {
                    // check for enumerated value.
                    if (array.GetType().GetElementType().GetTypeInfo().IsEnum)
                    {
                        int[] values = new int[array.Length];

                        for (int ii = 0; ii < array.Length; ii++)
                        {
                            values[ii] = Convert.ToInt32(array.GetValue(ii), CultureInfo.InvariantCulture);
                        }

                        Set(values);
                        return;
                    }

                    // not supported.
                    throw new ServiceResultException(
                        StatusCodes.BadNotSupported,
                        Utils.Format("The type '{0}' cannot be stored in a Variant object.", array.GetType().FullName));
                }

                // convert Guids to Uuids.
                case BuiltInType.Guid:
                {
                    if (array is Guid[] guids)
                    {
                        Set(guids);
                        return;
                    }

                    Set((Uuid[])array);
                    return;
                }

                // convert encodeables to extension objects.
                case BuiltInType.ExtensionObject:
                {
                    if (array is IEncodeable[] encodeables)
                    {
                        ExtensionObject[] extensions = new ExtensionObject[encodeables.Length];

                        for (int ii = 0; ii < encodeables.Length; ii++)
                        {
                            extensions[ii] = new ExtensionObject(encodeables[ii]);
                        }

                        Set(extensions);
                        return;
                    }

                    Set((ExtensionObject[])array);
                    return;
                }

                // convert objects to variants objects.
                case BuiltInType.Variant:
                {
                    if (array is object[] objects)
                    {
                        Variant[] variants = new Variant[objects.Length];

                        for (int ii = 0; ii < objects.Length; ii++)
                        {
                            variants[ii] = new Variant(objects[ii]);
                        }

                        Set(variants);
                        return;
                    }

                    Set((Variant[])array);
                    return;
                }

                // just save the value.
                default:
                {
                    this = new Variant(array, typeInfo, true);
                    return;
                }
            }
        }

        /// <summary>
        /// Initializes the object with a collection.
        /// </summary>
        private void SetList(IList value, TypeInfo typeInfo)
        {
            Array array = TypeInfo.CreateArray(typeInfo.BuiltInType, value.Count);

            for (int ii = 0; ii < value.Count; ii++)
            {
                if (typeInfo.BuiltInType == BuiltInType.ExtensionObject)
                {
                    if (value[ii] is IEncodeable encodeable)
                    {
                        array.SetValue(new ExtensionObject(encodeable), ii);
                        continue;
                    }
                }

                array.SetValue(value[ii], ii);
            }

            SetArray(array, typeInfo);
        }

        /// <summary>
        /// Initializes the object with an object.
        /// </summary>
        private void Set(object value, TypeInfo typeInfo)
        {
            // check for null values.
            if (value == null)
            {
                this = new Variant(typeInfo);
                return;
            }

            // handle scalar values.
            if (typeInfo.ValueRank < 0)
            {
                SetScalar(value, typeInfo);
                return;
            }

            Array array = value as Array;

            // handle one dimensional arrays.
            if (typeInfo.ValueRank <= 1)
            {
                // handle arrays.
                if (array != null)
                {
                    SetArray(array, typeInfo);
                    return;
                }

                // handle lists.
                if (value is IList list)
                {
                    SetList(list, typeInfo);
                    return;
                }
            }

            // handle multidimensional array.
            if (array != null)
            {
                this = new Variant(new Matrix(array, typeInfo.BuiltInType));
                return;
            }

            // handle matrix.
            if (value is Matrix matrix)
            {
                this = new Variant(matrix);
                return;
            }

            // not supported.
            throw new ServiceResultException(
                StatusCodes.BadNotSupported,
                Utils.Format("Arrays of the type '{0}' cannot be stored in a Variant object.", value.GetType().FullName));
        }

        /// <summary>
        /// Throws an exception if the Variant type does not match the expected type.
        /// </summary>
        private InvalidOperationException ThrowOnFailedDirectAccess([CallerMemberName] string callerMembername = "")
        {
            return new InvalidOperationException(Utils.Format("Failed to directly access the Variant value using {0} because the BuiltInType is {1}",
                callerMembername, m_typeInfo != null ? Enum.GetName(m_typeInfo.BuiltInType) : "(null)"));
        }
        #endregion

        #region Private Members
        [FieldOffset(0)]
        private readonly bool m_boolean;
        [FieldOffset(0)]
        private readonly sbyte m_sbyte;
        [FieldOffset(0)]
        private readonly byte m_byte;
        [FieldOffset(0)]
        private readonly short m_int16;
        [FieldOffset(0)]
        private readonly ushort m_uint16;
        [FieldOffset(0)]
        private readonly int m_int32;
        [FieldOffset(0)]
        private readonly uint m_uint32;
        [FieldOffset(0)]
        private readonly long m_int64;
        [FieldOffset(0)]
        private readonly ulong m_uint64;
        [FieldOffset(0)]
        private readonly float m_float;
        [FieldOffset(0)]
        private readonly double m_double;
        [FieldOffset(8)]
        private readonly TypeInfo m_typeInfo;
        [FieldOffset(16)]
        private object m_value;
        #endregion
    }

    #region VariantCollection Class
    /// <summary>
    /// A collection of Variant objects.
    /// </summary>
    [CollectionDataContract(Name = "ListOfVariant", Namespace = Namespaces.OpcUaXsd, ItemName = "Variant")]
    public partial class VariantCollection : List<Variant>, ICloneable
    {
        /// <summary>
        /// Initializes an empty collection.
        /// </summary>
        public VariantCollection() { }

        /// <summary>
        /// Initializes the collection from another collection.
        /// </summary>
        /// <remarks>
        /// Provides a strongly-typed collection of <see cref="Variant"/> objects.
        /// </remarks>
        public VariantCollection(IEnumerable<Variant> collection) : base(collection) { }

        /// <summary>
        /// Initializes the collection with the specified capacity.
        /// </summary>
        /// <param name="capacity">The capacity to constrain the collection to</param>
        public VariantCollection(int capacity) : base(capacity) { }

        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        /// <remarks>
        /// Converts an array of <see cref="Variant"/> to a collection.
        /// </remarks>
        /// <param name="values">An array of <see cref="Variant"/> to convert to a collection</param>
        public static VariantCollection ToVariantCollection(Variant[] values)
        {
            if (values != null)
            {
                return new VariantCollection(values);
            }

            return new VariantCollection();
        }

        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        /// <remarks>
        /// Converts an array of <see cref="Variant"/> to a collection.
        /// </remarks>
        /// <param name="values">An array of <see cref="Variant"/> to convert to a collection</param>
        public static implicit operator VariantCollection(Variant[] values)
        {
            return ToVariantCollection(values);
        }

        #region ICloneable
        /// <inheritdoc/>
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public new object MemberwiseClone()
        {
            VariantCollection clone = new VariantCollection(this.Count);

            foreach (Variant element in this)
            {
                clone.Add((Variant)Utils.Clone(element));
            }

            return clone;
        }
        #endregion
    }//class
    #endregion

}//namespace
