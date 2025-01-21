// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Opc.Ua.Buffers;

namespace Opc.Ua
{
    #region DataChangeNotification Class
    /// <remarks />
    /// <exclude />
    [DataContract(Namespace = Opc.Ua.Namespaces.OpcUaXsd)]
    public sealed class DataChangeNotification : Opc.Ua.NotificationData
    {
        #region Constructors
        /// <remarks />
        public DataChangeNotification()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor used when a memory owner exists.
        /// </summary>
        public DataChangeNotification(
            IArraySegmentOwner<MonitoredItemNotificationStruct> monitoredItems,
            DiagnosticInfoCollection diagnosticInfos)
        {
            m_monitoredItems = monitoredItems;
            m_diagnosticInfos = diagnosticInfos;
            m_disposed = false;
        }

        [OnDeserializing]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        private void Initialize()
        {
            m_monitoredItems = null;
            m_diagnosticInfos = null;
            m_disposed = false;
        }
        #endregion

        #region IDisposable Members
        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_monitoredItems?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "MonitoredItems", IsRequired = false, Order = 1)]
        public ArraySegment<MonitoredItemNotificationStruct> MonitoredItems
        {
            get
            {
                return m_monitoredItems != null ? m_monitoredItems.Segment : new ArraySegment<MonitoredItemNotificationStruct>();
            }

            init
            {
                var monitoredItems = m_monitoredItems;
                m_monitoredItems = AllocatedArraySegment<MonitoredItemNotificationStruct>.Create(value);
                if (!m_disposed)
                {
                    m_disposed = false;
                    monitoredItems?.Dispose();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "DiagnosticInfos", IsRequired = false, Order = 2)]
        public DiagnosticInfoCollection DiagnosticInfos
        {
            get
            {
                return m_diagnosticInfos;
            }

            set
            {
                m_diagnosticInfos = value;

                if (value == null)
                {
                    m_diagnosticInfos = new DiagnosticInfoCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public override ExpandedNodeId TypeId => DataTypeIds.DataChangeNotification;

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public override ExpandedNodeId BinaryEncodingId => ObjectIds.DataChangeNotification_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public override ExpandedNodeId XmlEncodingId => ObjectIds.DataChangeNotification_Encoding_DefaultXml;

        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public override ExpandedNodeId JsonEncodingId => ObjectIds.DataChangeNotification_Encoding_DefaultJson;

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public override void Encode(IEncoder encoder)
        {
            base.Encode(encoder);

            encoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            encoder.WriteEncodeableArray<MonitoredItemNotificationStruct>("MonitoredItems", MonitoredItems);
            encoder.WriteDiagnosticInfoArray("DiagnosticInfos", DiagnosticInfos);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public override void Decode(IDecoder decoder)
        {
            base.Decode(decoder);

            decoder.PushNamespace(Opc.Ua.Namespaces.OpcUaXsd);

            var monitoredItems = m_monitoredItems;
            m_monitoredItems = decoder.ReadEncodeableArray<MonitoredItemNotificationStruct>("MonitoredItems");
            DiagnosticInfos = decoder.ReadDiagnosticInfoArray("DiagnosticInfos");

            m_disposed = false;
            monitoredItems?.Dispose();

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public override bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            DataChangeNotification value = encodeable as DataChangeNotification;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_monitoredItems, value.m_monitoredItems)) return false;
            if (!Utils.IsEqual(m_diagnosticInfos, value.m_diagnosticInfos)) return false;

            return base.IsEqual(encodeable);
        }

        /// <summary cref="ICloneable.Clone" />
        public override object Clone()
        {
            return (DataChangeNotification)this.MemberwiseClone();
        }

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            DataChangeNotification clone = (DataChangeNotification)base.MemberwiseClone();

            clone.m_monitoredItems = ArrayPoolArraySegment<MonitoredItemNotificationStruct>.Rent(m_monitoredItems.Segment.Count);
            m_monitoredItems.Segment.CopyTo(clone.m_monitoredItems.Segment);
            clone.m_diagnosticInfos = (DiagnosticInfoCollection)Utils.Clone(this.m_diagnosticInfos);

            return clone;
        }
        #endregion

        #region Private Fields
        private IArraySegmentOwner<MonitoredItemNotificationStruct> m_monitoredItems;
        private DiagnosticInfoCollection m_diagnosticInfos;
        private bool m_disposed;
        #endregion
    }
    #endregion
}
