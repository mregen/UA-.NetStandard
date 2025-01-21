// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// A datachange returned in a NotificationMessage.
    /// Coexists with <see cref="MonitoredItemNotification"/>.
    /// </summary>
    public struct MonitoredItemNotificationStruct : IEncodeable
    {
        private const DataSetFieldContentMask DefaultMask =
            DataSetFieldContentMask.StatusCode |
            DataSetFieldContentMask.SourceTimestamp |
            DataSetFieldContentMask.SourcePicoSeconds |
            DataSetFieldContentMask.ServerTimestamp |
            DataSetFieldContentMask.ServerPicoSeconds;

        /// <summary>
        /// Creates a Monitored item Notification structure from a MonitoredItemNotification.
        /// </summary>
        public MonitoredItemNotificationStruct(MonitoredItemNotification notification)
        {
            m_clientHandle = notification.ClientHandle;
            m_value = new DataValueStruct(notification.Value);
            m_diagnosticInfo = null;
        }

        [OnDeserializing]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        private void Initialize()
        {
            m_clientHandle = (uint)0;
            m_value = new DataValueStruct();
            m_diagnosticInfo = null;
        }

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public ExpandedNodeId TypeId => DataTypeIds.MonitoredItemNotification;

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public ExpandedNodeId BinaryEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public ExpandedNodeId XmlEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultXml;

        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public ExpandedNodeId JsonEncodingId => ObjectIds.MonitoredItemNotification_Encoding_DefaultJson;

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            encoder.WriteUInt32("ClientHandle", ClientHandle);
            encoder.WriteDataValueStruct("Value", ref m_value, DefaultMask);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            ClientHandle = decoder.ReadUInt32("ClientHandle");
            decoder.ReadDataValueStruct("Value", ref m_value);

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public bool IsEqual(IEncodeable encodeable)
        {
            if (encodeable is MonitoredItemNotificationStruct valueStruct)
            {
                if (m_clientHandle != valueStruct.m_clientHandle) return false;
                if (!Utils.IsEqual(m_value, valueStruct.m_value)) return false;

                return true;
            }

            // support class version
            if (encodeable is MonitoredItemNotification value)
            {
                if (m_clientHandle != value.ClientHandle) return false;
                if (!Utils.IsEqual(m_value, value.Value)) return false;

                return true;
            }

            return false;
        }

        /// <summary cref="ICloneable.Clone" />
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            MonitoredItemNotificationStruct clone = new MonitoredItemNotificationStruct {
                m_clientHandle = this.m_clientHandle,
                m_value = this.m_value
            };

            return clone;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The diagnostic info associated with the notification.
        /// </summary>
        public DiagnosticInfo DiagnosticInfo
        {
            get { return m_diagnosticInfo; }
            set { m_diagnosticInfo = value; }
        }

        /// <remarks />
        [DataMember(Name = "ClientHandle", IsRequired = false, Order = 1)]
        public uint ClientHandle
        {
            get { return m_clientHandle; }
            set { m_clientHandle = value; }
        }

        /// <remarks />
        [DataMember(Name = "Value", IsRequired = false, Order = 2)]
        public DataValueStruct Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        #endregion

        #region Private Fields
        private uint m_clientHandle;
        private DataValueStruct m_value;
        private DiagnosticInfo m_diagnosticInfo;
        #endregion
    }
}
