// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Opc.Ua
{
    /// <summary>
    /// A struct that stores the valueStruct of variable with an optional status code and timestamps.
    /// Can be used instead of <see cref="DataValue"/> for efficient storage in Arrays.
    /// </summary>
    [DataContract(Namespace = Namespaces.OpcUaXsd)]
    public struct DataValueStruct : IFormattable, IEquatable<DataValueStruct>
    {
        #region Constructors
        /// <summary>
        /// Initialize the struct.
        /// </summary>
        public DataValueStruct()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the struct from a DataValue.
        /// </summary>
        public DataValueStruct(DataValue value)
        {
            m_value = value.WrappedValue;
            m_statusCode = value.StatusCode;
            m_sourceTimestamp = value.SourceTimestamp;
            m_sourcePicoseconds = value.SourcePicoseconds;
            m_serverTimestamp = value.ServerTimestamp;
            m_serverPicoseconds = value.ServerPicoseconds;
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        [OnDeserializing()]
        private void Initialize()
        {
            m_value = Variant.Null;
            m_statusCode = StatusCodes.Good;
            m_sourceTimestamp = DateTime.MinValue;
            m_serverTimestamp = DateTime.MinValue;
            m_sourcePicoseconds = 0;
            m_serverPicoseconds = 0;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Determines if the specified object is equal to the object.
        /// </summary>
        /// <param name="obj">The object to compare to *this*</param>
        public override bool Equals(object obj)
        {
            if (obj is DataValueStruct valueStruct)
            {
                if (m_statusCode != valueStruct.m_statusCode)
                {
                    return false;
                }

                if (m_serverTimestamp != valueStruct.m_serverTimestamp)
                {
                    return false;
                }

                if (m_sourceTimestamp != valueStruct.m_sourceTimestamp)
                {
                    return false;
                }

                if (m_serverPicoseconds != valueStruct.m_serverPicoseconds)
                {
                    return false;
                }

                if (m_sourcePicoseconds != valueStruct.m_sourcePicoseconds)
                {
                    return false;
                }

                return Utils.IsEqual(m_value.Value, valueStruct.m_value.Value);
            }
            else if (obj is DataValue value)
            {
                if (m_statusCode != value.StatusCode)
                {
                    return false;
                }

                if (m_serverTimestamp != value.ServerTimestamp)
                {
                    return false;
                }

                if (m_sourceTimestamp != value.SourceTimestamp)
                {
                    return false;
                }

                if (m_serverPicoseconds != value.ServerPicoseconds)
                {
                    return false;
                }

                if (m_sourcePicoseconds != value.SourcePicoseconds)
                {
                    return false;
                }

                return Utils.IsEqual(m_value.Value, value.WrappedValue);
            }


            return false;
        }

        /// <summary>
        /// Determines if the specified object is equal to the object.
        /// </summary>
        /// <param name="other">The DataValue to compare to *this*</param>
        public bool Equals(DataValueStruct other)
        {
            if (m_statusCode != other.StatusCode)
            {
                return false;
            }

            if (m_serverTimestamp != other.ServerTimestamp)
            {
                return false;
            }

            if (m_sourceTimestamp != other.SourceTimestamp)
            {
                return false;
            }

            if (m_serverPicoseconds != other.ServerPicoseconds)
            {
                return false;
            }

            if (m_sourcePicoseconds != other.SourcePicoseconds)
            {
                return false;
            }

            return Utils.IsEqual(m_value.Value, other.Value);
        }

        /// <summary>
        /// Returns a unique hashcode for the object.
        /// </summary>
        public override int GetHashCode()
        {
            if (m_value.Value != null)
            {
                return m_value.Value.GetHashCode();
            }

            return m_statusCode.GetHashCode();
        }

        /// <summary>
        /// Converts the value to a human readable string.
        /// </summary>
        public override string ToString()
        {
            return ToString(null, null);
        }
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <param name="format">Not used, ALWAYS specify a null/nothing value</param>
        /// <param name="formatProvider">The format string, ALWAYS specify a null/nothing value</param>
        /// <exception cref="FormatException">Thrown when the format is NOT null/nothing</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return string.Format(formatProvider, "{0}", m_value);
            }

            throw new FormatException(Utils.Format("Invalid format string: '{0}'.", format));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The value of data DataValue Variant.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public object Value
        {
            get { return m_value.Value; }
            set { m_value.Value = value; }
        }

        /// <summary>
        /// The Variant of the DataValue.
        /// </summary>
        [DataMember(Name = "Value", Order = 1, IsRequired = false)]
        public Variant WrappedValue
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// The status code associated with the value.
        /// </summary>
        [DataMember(Order = 2, IsRequired = false)]
        public StatusCode StatusCode
        {
            get { return m_statusCode; }
            set { m_statusCode = value; }
        }

        /// <summary>
        /// The source timestamp associated with the value.
        /// </summary>
        [DataMember(Order = 3, IsRequired = false)]
        public DateTime SourceTimestamp
        {
            get { return m_sourceTimestamp; }
            set { m_sourceTimestamp = value; }
        }

        /// <summary>
        /// Additional resolution for the source timestamp.
        /// </summary>
        [DataMember(Order = 4, IsRequired = false)]
        public ushort SourcePicoseconds
        {
            get { return m_sourcePicoseconds; }
            set { m_sourcePicoseconds = value; }
        }

        /// <summary>
        /// The server timestamp associated with the value.
        /// </summary>
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime ServerTimestamp
        {
            get { return m_serverTimestamp; }
            set { m_serverTimestamp = value; }
        }

        /// <summary>
        /// Additional resolution for the server timestamp.
        /// </summary>
        [DataMember(Order = 6, IsRequired = false)]
        public ushort ServerPicoseconds
        {
            get { return m_serverPicoseconds; }
            set { m_serverPicoseconds = value; }
        }
        #endregion

        #region Private Fields
        private Variant m_value;
        private DateTime m_sourceTimestamp;
        private DateTime m_serverTimestamp;
        private StatusCode m_statusCode;
        private ushort m_sourcePicoseconds;
        private ushort m_serverPicoseconds;
        #endregion
    }

}//namespace
