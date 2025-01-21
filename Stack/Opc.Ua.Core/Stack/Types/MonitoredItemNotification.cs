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

namespace Opc.Ua
{
    /// <summary>
    /// A datachange returned in a NotificationMessage.
    /// Coexists with <see cref="MonitoredItemNotificationStruct"/> and fixes issues
    /// with equal operators.
    /// </summary>
	public class MonitoredItemNotification : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public MonitoredItemNotification()
        {
            Initialize();
        }

        [OnDeserializing]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        private void Initialize()
        {
            m_clientHandle = (uint)0;
            m_value = new DataValue();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "ClientHandle", IsRequired = false, Order = 1)]
        public uint ClientHandle
        {
            get { return m_clientHandle; }
            set { m_clientHandle = value; }
        }

        /// <remarks />
        [DataMember(Name = "Value", IsRequired = false, Order = 2)]
        public DataValue Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        #endregion

        #region Extra Public Members
#if LEGACY
        /// <summary>
        /// The notification message that the item belongs to.
        /// </summary>
        public NotificationMessage Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
#endif

        /// <summary>
        /// The diagnostic info associated with the notification.
        /// </summary>
        public DiagnosticInfo DiagnosticInfo
        {
            get { return m_diagnosticInfo; }
            set { m_diagnosticInfo = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.MonitoredItemNotification;

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultXml;

        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultJson;

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            encoder.WriteUInt32("ClientHandle", ClientHandle);
            encoder.WriteDataValue("Value", Value);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            ClientHandle = decoder.ReadUInt32("ClientHandle");
            Value = decoder.ReadDataValue("Value");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            if (encodeable is MonitoredItemNotification value)
            {
                if (!Utils.IsEqual(m_clientHandle, value.m_clientHandle)) return false;
                if (!Utils.IsEqual(m_value, value.m_value)) return false;
                return true;
            }

            if (encodeable is MonitoredItemNotificationStruct structValue)
            {
                if (m_clientHandle != structValue.ClientHandle) return false;
                if (m_value?.Equals(structValue.Value) != true) return false;
                return true;
            }

            return false;
        }

        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (MonitoredItemNotification)this.MemberwiseClone();
        }

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            MonitoredItemNotification clone = (MonitoredItemNotification)base.MemberwiseClone();

            clone.m_clientHandle = (uint)Utils.Clone(this.m_clientHandle);
            clone.m_value = (DataValue)Utils.Clone(this.m_value);

            return clone;
        }
        #endregion

        #region Private Fields
        private uint m_clientHandle;
        private DataValue m_value;
#if LEGACY
        private NotificationMessage m_message;
#endif
        private DiagnosticInfo m_diagnosticInfo;
        #endregion
    }
}
