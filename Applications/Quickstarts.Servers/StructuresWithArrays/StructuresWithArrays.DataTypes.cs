/* ========================================================================
 * Copyright (c) 2005-2021 The OPC Foundation, Inc. All rights reserved.
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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace StructuresWithArrays
{
    #region Structure_A Class
    #if (!OPCUA_EXCLUDE_Structure_A)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_A : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_A()
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
            m_duration_Field = (double)0;
            m_double_Field = (double)0;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Duration_Field", IsRequired = false, Order = 1)]
        public double Duration_Field
        {
            get { return m_duration_Field;  }
            set { m_duration_Field = value; }
        }

        /// <remarks />
        [DataMember(Name = "Double_Field", IsRequired = false, Order = 2)]
        public double Double_Field
        {
            get { return m_double_Field;  }
            set { m_double_Field = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_A; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_A_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_A_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteDouble("Duration_Field", Duration_Field);
            encoder.WriteDouble("Double_Field", Double_Field);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Duration_Field = decoder.ReadDouble("Duration_Field");
            Double_Field = decoder.ReadDouble("Double_Field");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_A value = encodeable as Structure_A;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_duration_Field, value.m_duration_Field)) return false;
            if (!Utils.IsEqual(m_double_Field, value.m_double_Field)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_A)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_A clone = (Structure_A)base.MemberwiseClone();

            clone.m_duration_Field = (double)Utils.Clone(this.m_duration_Field);
            clone.m_double_Field = (double)Utils.Clone(this.m_double_Field);

            return clone;
        }
        #endregion

        #region Private Fields
        private double m_duration_Field;
        private double m_double_Field;
        #endregion
    }

    #region Structure_ACollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_A", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_A")]
    #if !NET_STANDARD
    public partial class Structure_ACollection : List<Structure_A>, ICloneable
    #else
    public partial class Structure_ACollection : List<Structure_A>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_ACollection() {}

        /// <remarks />
        public Structure_ACollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_ACollection(IEnumerable<Structure_A> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_ACollection(Structure_A[] values)
        {
            if (values != null)
            {
                return new Structure_ACollection(values);
            }

            return new Structure_ACollection();
        }

        /// <remarks />
        public static explicit operator Structure_A[](Structure_ACollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_ACollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_ACollection clone = new Structure_ACollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_A)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_B Class
    #if (!OPCUA_EXCLUDE_Structure_B)
    /// <remarks />
    /// <exclude />
    
    public enum Structure_BFields : uint
    {   
        None = 0,
        /// <remarks />
        Duration_Field = 1,
        /// <remarks />
        Int32_Field = 2
    }
        
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_B : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_B()
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
            EncodingMask = Structure_BFields.None;
            m_duration_Field = (double)0;
            m_int32_Field = (int)0;
        }
        #endregion

        #region Public Properties
        // <remarks />
        [DataMember(Name = "EncodingMask", IsRequired = true, Order = 0)]
        public Structure_BFields EncodingMask { get; set; }

        /// <remarks />
        [DataMember(Name = "Duration_Field", IsRequired = false, Order = 1)]
        public double Duration_Field
        {
            get { return m_duration_Field;  }
            set { m_duration_Field = value; }
        }

        /// <remarks />
        [DataMember(Name = "Int32_Field", IsRequired = false, Order = 2)]
        public int Int32_Field
        {
            get { return m_int32_Field;  }
            set { m_int32_Field = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_B; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_B_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_B_Encoding_DefaultXml;
            
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);
            encoder.WriteUInt32(nameof(EncodingMask), (uint)EncodingMask);

            if ((EncodingMask & Structure_BFields.Duration_Field) != 0) encoder.WriteDouble("Duration_Field", Duration_Field);
            encoder.WriteInt32("Int32_Field", Int32_Field);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            EncodingMask = (Structure_BFields)decoder.ReadUInt32(nameof(EncodingMask));

            if ((EncodingMask & Structure_BFields.Duration_Field) != 0) Duration_Field = decoder.ReadDouble("Duration_Field");
            Int32_Field = decoder.ReadInt32("Int32_Field");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_B value = encodeable as Structure_B;

            if (value == null)
            {
                return false;
            }

            if (value.EncodingMask != this.EncodingMask) return false;

            if ((EncodingMask & Structure_BFields.Duration_Field) != 0) if (!Utils.IsEqual(m_duration_Field, value.m_duration_Field)) return false;
            if (!Utils.IsEqual(m_int32_Field, value.m_int32_Field)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_B)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_B clone = (Structure_B)base.MemberwiseClone();

            clone.EncodingMask = this.EncodingMask;

            if ((EncodingMask & Structure_BFields.Duration_Field) != 0) clone.m_duration_Field = (double)Utils.Clone(this.m_duration_Field);
            clone.m_int32_Field = (int)Utils.Clone(this.m_int32_Field);

            return clone;
        }
        #endregion

        #region Private Fields
        private double m_duration_Field;
        private int m_int32_Field;
        #endregion
    }

    #region Structure_BCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_B", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_B")]
    #if !NET_STANDARD
    public partial class Structure_BCollection : List<Structure_B>, ICloneable
    #else
    public partial class Structure_BCollection : List<Structure_B>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_BCollection() {}

        /// <remarks />
        public Structure_BCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_BCollection(IEnumerable<Structure_B> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_BCollection(Structure_B[] values)
        {
            if (values != null)
            {
                return new Structure_BCollection(values);
            }

            return new Structure_BCollection();
        }

        /// <remarks />
        public static explicit operator Structure_B[](Structure_BCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_BCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_BCollection clone = new Structure_BCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_B)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_C Class
    #if (!OPCUA_EXCLUDE_Structure_C)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_C : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_C()
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
            m_int32_Field = (int)0;
            m_nodeId_Field = null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Int32_Field", IsRequired = false, Order = 1)]
        public int Int32_Field
        {
            get { return m_int32_Field;  }
            set { m_int32_Field = value; }
        }

        /// <remarks />
        [DataMember(Name = "NodeId_Field", IsRequired = false, Order = 2)]
        public NodeId NodeId_Field
        {
            get { return m_nodeId_Field;  }
            set { m_nodeId_Field = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_C; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_C_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_C_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteInt32("Int32_Field", Int32_Field);
            encoder.WriteNodeId("NodeId_Field", NodeId_Field);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Int32_Field = decoder.ReadInt32("Int32_Field");
            NodeId_Field = decoder.ReadNodeId("NodeId_Field");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_C value = encodeable as Structure_C;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_int32_Field, value.m_int32_Field)) return false;
            if (!Utils.IsEqual(m_nodeId_Field, value.m_nodeId_Field)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_C)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_C clone = (Structure_C)base.MemberwiseClone();

            clone.m_int32_Field = (int)Utils.Clone(this.m_int32_Field);
            clone.m_nodeId_Field = (NodeId)Utils.Clone(this.m_nodeId_Field);

            return clone;
        }
        #endregion

        #region Private Fields
        private int m_int32_Field;
        private NodeId m_nodeId_Field;
        #endregion
    }

    #region Structure_CCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_C", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_C")]
    #if !NET_STANDARD
    public partial class Structure_CCollection : List<Structure_C>, ICloneable
    #else
    public partial class Structure_CCollection : List<Structure_C>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_CCollection() {}

        /// <remarks />
        public Structure_CCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_CCollection(IEnumerable<Structure_C> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_CCollection(Structure_C[] values)
        {
            if (values != null)
            {
                return new Structure_CCollection(values);
            }

            return new Structure_CCollection();
        }

        /// <remarks />
        public static explicit operator Structure_C[](Structure_CCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_CCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_CCollection clone = new Structure_CCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_C)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_D Class
    #if (!OPCUA_EXCLUDE_Structure_D)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_D : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_D()
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
            m_scalar_BaseDataType = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_BaseDataType", IsRequired = false, Order = 1)]
        public Variant Scalar_BaseDataType
        {
            get { return m_scalar_BaseDataType;  }
            set { m_scalar_BaseDataType = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_D; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_D_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_D_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("Scalar_BaseDataType", Scalar_BaseDataType);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_BaseDataType = decoder.ReadVariant("Scalar_BaseDataType");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_D value = encodeable as Structure_D;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_BaseDataType, value.m_scalar_BaseDataType)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_D)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_D clone = (Structure_D)base.MemberwiseClone();

            clone.m_scalar_BaseDataType = (Variant)Utils.Clone(this.m_scalar_BaseDataType);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_scalar_BaseDataType;
        #endregion
    }

    #region Structure_DCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_D", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_D")]
    #if !NET_STANDARD
    public partial class Structure_DCollection : List<Structure_D>, ICloneable
    #else
    public partial class Structure_DCollection : List<Structure_D>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_DCollection() {}

        /// <remarks />
        public Structure_DCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_DCollection(IEnumerable<Structure_D> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_DCollection(Structure_D[] values)
        {
            if (values != null)
            {
                return new Structure_DCollection(values);
            }

            return new Structure_DCollection();
        }

        /// <remarks />
        public static explicit operator Structure_D[](Structure_DCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_DCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_DCollection clone = new Structure_DCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_D)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_E Class
    #if (!OPCUA_EXCLUDE_Structure_E)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_E : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_E()
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
            m_scalar_Structure = null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_Structure", IsRequired = false, Order = 1)]
        public ExtensionObject Scalar_Structure
        {
            get { return m_scalar_Structure;  }
            set { m_scalar_Structure = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_E; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_E_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_E_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteExtensionObject("Scalar_Structure", Scalar_Structure);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_Structure = decoder.ReadExtensionObject("Scalar_Structure");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_E value = encodeable as Structure_E;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_Structure, value.m_scalar_Structure)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_E)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_E clone = (Structure_E)base.MemberwiseClone();

            clone.m_scalar_Structure = (ExtensionObject)Utils.Clone(this.m_scalar_Structure);

            return clone;
        }
        #endregion

        #region Private Fields
        private ExtensionObject m_scalar_Structure;
        #endregion
    }

    #region Structure_ECollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_E", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_E")]
    #if !NET_STANDARD
    public partial class Structure_ECollection : List<Structure_E>, ICloneable
    #else
    public partial class Structure_ECollection : List<Structure_E>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_ECollection() {}

        /// <remarks />
        public Structure_ECollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_ECollection(IEnumerable<Structure_E> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_ECollection(Structure_E[] values)
        {
            if (values != null)
            {
                return new Structure_ECollection(values);
            }

            return new Structure_ECollection();
        }

        /// <remarks />
        public static explicit operator Structure_E[](Structure_ECollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_ECollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_ECollection clone = new Structure_ECollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_E)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_F Class
    #if (!OPCUA_EXCLUDE_Structure_F)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_F : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_F()
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
            m_scalar_ApplicationDescription = new Opc.Ua.ApplicationDescription();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_ApplicationDescription", IsRequired = false, Order = 1)]
        public Opc.Ua.ApplicationDescription Scalar_ApplicationDescription
        {
            get
            {
                return m_scalar_ApplicationDescription;
            }

            set
            {
                m_scalar_ApplicationDescription = value;

                if (value == null)
                {
                    m_scalar_ApplicationDescription = new Opc.Ua.ApplicationDescription();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_F; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_F_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_F_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteEncodeable("Scalar_ApplicationDescription", Scalar_ApplicationDescription, typeof(Opc.Ua.ApplicationDescription));

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)decoder.ReadEncodeable("Scalar_ApplicationDescription", typeof(Opc.Ua.ApplicationDescription));

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_F value = encodeable as Structure_F;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_ApplicationDescription, value.m_scalar_ApplicationDescription)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_F)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_F clone = (Structure_F)base.MemberwiseClone();

            clone.m_scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)Utils.Clone(this.m_scalar_ApplicationDescription);

            return clone;
        }
        #endregion

        #region Private Fields
        private Opc.Ua.ApplicationDescription m_scalar_ApplicationDescription;
        #endregion
    }

    #region Structure_FCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_F", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_F")]
    #if !NET_STANDARD
    public partial class Structure_FCollection : List<Structure_F>, ICloneable
    #else
    public partial class Structure_FCollection : List<Structure_F>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_FCollection() {}

        /// <remarks />
        public Structure_FCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_FCollection(IEnumerable<Structure_F> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_FCollection(Structure_F[] values)
        {
            if (values != null)
            {
                return new Structure_FCollection(values);
            }

            return new Structure_FCollection();
        }

        /// <remarks />
        public static explicit operator Structure_F[](Structure_FCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_FCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_FCollection clone = new Structure_FCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_F)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_G Class
    #if (!OPCUA_EXCLUDE_Structure_G)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_G : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_G()
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
            m_scalar_Structure_A = new Structure_A();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_Structure_A", IsRequired = false, Order = 1)]
        public Structure_A Scalar_Structure_A
        {
            get
            {
                return m_scalar_Structure_A;
            }

            set
            {
                m_scalar_Structure_A = value;

                if (value == null)
                {
                    m_scalar_Structure_A = new Structure_A();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_G; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_G_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_G_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteEncodeable("Scalar_Structure_A", Scalar_Structure_A, typeof(Structure_A));

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_Structure_A = (Structure_A)decoder.ReadEncodeable("Scalar_Structure_A", typeof(Structure_A));

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_G value = encodeable as Structure_G;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_Structure_A, value.m_scalar_Structure_A)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_G)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_G clone = (Structure_G)base.MemberwiseClone();

            clone.m_scalar_Structure_A = (Structure_A)Utils.Clone(this.m_scalar_Structure_A);

            return clone;
        }
        #endregion

        #region Private Fields
        private Structure_A m_scalar_Structure_A;
        #endregion
    }

    #region Structure_GCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_G", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_G")]
    #if !NET_STANDARD
    public partial class Structure_GCollection : List<Structure_G>, ICloneable
    #else
    public partial class Structure_GCollection : List<Structure_G>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_GCollection() {}

        /// <remarks />
        public Structure_GCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_GCollection(IEnumerable<Structure_G> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_GCollection(Structure_G[] values)
        {
            if (values != null)
            {
                return new Structure_GCollection(values);
            }

            return new Structure_GCollection();
        }

        /// <remarks />
        public static explicit operator Structure_G[](Structure_GCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_GCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_GCollection clone = new Structure_GCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_G)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_H Class
    #if (!OPCUA_EXCLUDE_Structure_H)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_H : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_H()
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
            m_scalar_Duration = (double)0;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_Duration", IsRequired = false, Order = 1)]
        public double Scalar_Duration
        {
            get { return m_scalar_Duration;  }
            set { m_scalar_Duration = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_H; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_H_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_H_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteDouble("Scalar_Duration", Scalar_Duration);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_Duration = decoder.ReadDouble("Scalar_Duration");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_H value = encodeable as Structure_H;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_Duration, value.m_scalar_Duration)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_H)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_H clone = (Structure_H)base.MemberwiseClone();

            clone.m_scalar_Duration = (double)Utils.Clone(this.m_scalar_Duration);

            return clone;
        }
        #endregion

        #region Private Fields
        private double m_scalar_Duration;
        #endregion
    }

    #region Structure_HCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_H", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_H")]
    #if !NET_STANDARD
    public partial class Structure_HCollection : List<Structure_H>, ICloneable
    #else
    public partial class Structure_HCollection : List<Structure_H>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_HCollection() {}

        /// <remarks />
        public Structure_HCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_HCollection(IEnumerable<Structure_H> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_HCollection(Structure_H[] values)
        {
            if (values != null)
            {
                return new Structure_HCollection(values);
            }

            return new Structure_HCollection();
        }

        /// <remarks />
        public static explicit operator Structure_H[](Structure_HCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_HCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_HCollection clone = new Structure_HCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_H)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_J Class
    #if (!OPCUA_EXCLUDE_Structure_J)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_J : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_J()
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
            m_scalar_MessageSecurityMode = MessageSecurityMode.Invalid;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_MessageSecurityMode", IsRequired = false, Order = 1)]
        public MessageSecurityMode Scalar_MessageSecurityMode
        {
            get { return m_scalar_MessageSecurityMode;  }
            set { m_scalar_MessageSecurityMode = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_J; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_J_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_J_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteEnumerated("Scalar_MessageSecurityMode", Scalar_MessageSecurityMode);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_MessageSecurityMode = (MessageSecurityMode)decoder.ReadEnumerated("Scalar_MessageSecurityMode", typeof(MessageSecurityMode));

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_J value = encodeable as Structure_J;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_MessageSecurityMode, value.m_scalar_MessageSecurityMode)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_J)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_J clone = (Structure_J)base.MemberwiseClone();

            clone.m_scalar_MessageSecurityMode = (MessageSecurityMode)Utils.Clone(this.m_scalar_MessageSecurityMode);

            return clone;
        }
        #endregion

        #region Private Fields
        private MessageSecurityMode m_scalar_MessageSecurityMode;
        #endregion
    }

    #region Structure_JCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_J", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_J")]
    #if !NET_STANDARD
    public partial class Structure_JCollection : List<Structure_J>, ICloneable
    #else
    public partial class Structure_JCollection : List<Structure_J>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_JCollection() {}

        /// <remarks />
        public Structure_JCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_JCollection(IEnumerable<Structure_J> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_JCollection(Structure_J[] values)
        {
            if (values != null)
            {
                return new Structure_JCollection(values);
            }

            return new Structure_JCollection();
        }

        /// <remarks />
        public static explicit operator Structure_J[](Structure_JCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_JCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_JCollection clone = new Structure_JCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_J)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_K Class
    #if (!OPCUA_EXCLUDE_Structure_K)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_K : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_K()
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
            m_s1D_Array_String_within_max_length = new StringCollection();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S1D_Array_String_within_max_length", IsRequired = false, Order = 1)]
        public StringCollection S1D_Array_String_within_max_length
        {
            get
            {
                return m_s1D_Array_String_within_max_length;
            }

            set
            {
                m_s1D_Array_String_within_max_length = value;

                if (value == null)
                {
                    m_s1D_Array_String_within_max_length = new StringCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_K; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_K_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_K_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteStringArray("S1D_Array_String_within_max_length", S1D_Array_String_within_max_length);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S1D_Array_String_within_max_length = decoder.ReadStringArray("S1D_Array_String_within_max_length");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_K value = encodeable as Structure_K;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s1D_Array_String_within_max_length, value.m_s1D_Array_String_within_max_length)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_K)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_K clone = (Structure_K)base.MemberwiseClone();

            clone.m_s1D_Array_String_within_max_length = (StringCollection)Utils.Clone(this.m_s1D_Array_String_within_max_length);

            return clone;
        }
        #endregion

        #region Private Fields
        private StringCollection m_s1D_Array_String_within_max_length;
        #endregion
    }

    #region Structure_KCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_K", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_K")]
    #if !NET_STANDARD
    public partial class Structure_KCollection : List<Structure_K>, ICloneable
    #else
    public partial class Structure_KCollection : List<Structure_K>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_KCollection() {}

        /// <remarks />
        public Structure_KCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_KCollection(IEnumerable<Structure_K> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_KCollection(Structure_K[] values)
        {
            if (values != null)
            {
                return new Structure_KCollection(values);
            }

            return new Structure_KCollection();
        }

        /// <remarks />
        public static explicit operator Structure_K[](Structure_KCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_KCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_KCollection clone = new Structure_KCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_K)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_L Class
    #if (!OPCUA_EXCLUDE_Structure_L)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_L : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_L()
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
            m_s1D_Array_of_BaseDataType = new VariantCollection();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S1D_Array_of_BaseDataType", IsRequired = false, Order = 1)]
        public VariantCollection S1D_Array_of_BaseDataType
        {
            get
            {
                return m_s1D_Array_of_BaseDataType;
            }

            set
            {
                m_s1D_Array_of_BaseDataType = value;

                if (value == null)
                {
                    m_s1D_Array_of_BaseDataType = new VariantCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_L; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_L_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_L_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariantArray("S1D_Array_of_BaseDataType", S1D_Array_of_BaseDataType);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S1D_Array_of_BaseDataType = decoder.ReadVariantArray("S1D_Array_of_BaseDataType");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_L value = encodeable as Structure_L;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s1D_Array_of_BaseDataType, value.m_s1D_Array_of_BaseDataType)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_L)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_L clone = (Structure_L)base.MemberwiseClone();

            clone.m_s1D_Array_of_BaseDataType = (VariantCollection)Utils.Clone(this.m_s1D_Array_of_BaseDataType);

            return clone;
        }
        #endregion

        #region Private Fields
        private VariantCollection m_s1D_Array_of_BaseDataType;
        #endregion
    }

    #region Structure_LCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_L", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_L")]
    #if !NET_STANDARD
    public partial class Structure_LCollection : List<Structure_L>, ICloneable
    #else
    public partial class Structure_LCollection : List<Structure_L>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_LCollection() {}

        /// <remarks />
        public Structure_LCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_LCollection(IEnumerable<Structure_L> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_LCollection(Structure_L[] values)
        {
            if (values != null)
            {
                return new Structure_LCollection(values);
            }

            return new Structure_LCollection();
        }

        /// <remarks />
        public static explicit operator Structure_L[](Structure_LCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_LCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_LCollection clone = new Structure_LCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_L)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_M Class
    #if (!OPCUA_EXCLUDE_Structure_M)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_M : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_M()
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
            m_s1D_Array_of_Structure__unknown_ = new ExtensionObjectCollection();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure__unknown_", IsRequired = false, Order = 1)]
        public ExtensionObjectCollection S1D_Array_of_Structure__unknown_
        {
            get
            {
                return m_s1D_Array_of_Structure__unknown_;
            }

            set
            {
                m_s1D_Array_of_Structure__unknown_ = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure__unknown_ = new ExtensionObjectCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_M; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_M_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_M_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteExtensionObjectArray("S1D_Array_of_Structure__unknown_", S1D_Array_of_Structure__unknown_);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S1D_Array_of_Structure__unknown_ = decoder.ReadExtensionObjectArray("S1D_Array_of_Structure__unknown_");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_M value = encodeable as Structure_M;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s1D_Array_of_Structure__unknown_, value.m_s1D_Array_of_Structure__unknown_)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_M)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_M clone = (Structure_M)base.MemberwiseClone();

            clone.m_s1D_Array_of_Structure__unknown_ = (ExtensionObjectCollection)Utils.Clone(this.m_s1D_Array_of_Structure__unknown_);

            return clone;
        }
        #endregion

        #region Private Fields
        private ExtensionObjectCollection m_s1D_Array_of_Structure__unknown_;
        #endregion
    }

    #region Structure_MCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_M", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_M")]
    #if !NET_STANDARD
    public partial class Structure_MCollection : List<Structure_M>, ICloneable
    #else
    public partial class Structure_MCollection : List<Structure_M>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_MCollection() {}

        /// <remarks />
        public Structure_MCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_MCollection(IEnumerable<Structure_M> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_MCollection(Structure_M[] values)
        {
            if (values != null)
            {
                return new Structure_MCollection(values);
            }

            return new Structure_MCollection();
        }

        /// <remarks />
        public static explicit operator Structure_M[](Structure_MCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_MCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_MCollection clone = new Structure_MCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_M)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_N Class
    #if (!OPCUA_EXCLUDE_Structure_N)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_N : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_N()
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
            m_s1D_Array_of_Structure__known_ = new ExtensionObjectCollection();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure__known_", IsRequired = false, Order = 1)]
        public ExtensionObjectCollection S1D_Array_of_Structure__known_
        {
            get
            {
                return m_s1D_Array_of_Structure__known_;
            }

            set
            {
                m_s1D_Array_of_Structure__known_ = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure__known_ = new ExtensionObjectCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_N; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_N_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_N_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteExtensionObjectArray("S1D_Array_of_Structure__known_", S1D_Array_of_Structure__known_);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S1D_Array_of_Structure__known_ = decoder.ReadExtensionObjectArray("S1D_Array_of_Structure__known_");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_N value = encodeable as Structure_N;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s1D_Array_of_Structure__known_, value.m_s1D_Array_of_Structure__known_)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_N)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_N clone = (Structure_N)base.MemberwiseClone();

            clone.m_s1D_Array_of_Structure__known_ = (ExtensionObjectCollection)Utils.Clone(this.m_s1D_Array_of_Structure__known_);

            return clone;
        }
        #endregion

        #region Private Fields
        private ExtensionObjectCollection m_s1D_Array_of_Structure__known_;
        #endregion
    }

    #region Structure_NCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_N", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_N")]
    #if !NET_STANDARD
    public partial class Structure_NCollection : List<Structure_N>, ICloneable
    #else
    public partial class Structure_NCollection : List<Structure_N>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_NCollection() {}

        /// <remarks />
        public Structure_NCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_NCollection(IEnumerable<Structure_N> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_NCollection(Structure_N[] values)
        {
            if (values != null)
            {
                return new Structure_NCollection(values);
            }

            return new Structure_NCollection();
        }

        /// <remarks />
        public static explicit operator Structure_N[](Structure_NCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_NCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_NCollection clone = new Structure_NCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_N)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_O Class
    #if (!OPCUA_EXCLUDE_Structure_O)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_O : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_O()
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
            m_s1D_Array_of_ApplicationDescription = new ApplicationDescriptionCollection();
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S1D_Array_of_ApplicationDescription", IsRequired = false, Order = 1)]
        public ApplicationDescriptionCollection S1D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s1D_Array_of_ApplicationDescription;
            }

            set
            {
                m_s1D_Array_of_ApplicationDescription = value;

                if (value == null)
                {
                    m_s1D_Array_of_ApplicationDescription = new ApplicationDescriptionCollection();
                }
            }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_O; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_O_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_O_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteEncodeableArray("S1D_Array_of_ApplicationDescription", S1D_Array_of_ApplicationDescription.ToArray(), typeof(Opc.Ua.ApplicationDescription));

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S1D_Array_of_ApplicationDescription = (Opc.Ua.ApplicationDescriptionCollection)decoder.ReadEncodeableArray("S1D_Array_of_ApplicationDescription", typeof(Opc.Ua.ApplicationDescription));

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_O value = encodeable as Structure_O;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s1D_Array_of_ApplicationDescription, value.m_s1D_Array_of_ApplicationDescription)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_O)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_O clone = (Structure_O)base.MemberwiseClone();

            clone.m_s1D_Array_of_ApplicationDescription = (ApplicationDescriptionCollection)Utils.Clone(this.m_s1D_Array_of_ApplicationDescription);

            return clone;
        }
        #endregion

        #region Private Fields
        private ApplicationDescriptionCollection m_s1D_Array_of_ApplicationDescription;
        #endregion
    }

    #region Structure_OCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_O", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_O")]
    #if !NET_STANDARD
    public partial class Structure_OCollection : List<Structure_O>, ICloneable
    #else
    public partial class Structure_OCollection : List<Structure_O>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_OCollection() {}

        /// <remarks />
        public Structure_OCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_OCollection(IEnumerable<Structure_O> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_OCollection(Structure_O[] values)
        {
            if (values != null)
            {
                return new Structure_OCollection(values);
            }

            return new Structure_OCollection();
        }

        /// <remarks />
        public static explicit operator Structure_O[](Structure_OCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_OCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_OCollection clone = new Structure_OCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_O)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_P Class
    #if (!OPCUA_EXCLUDE_Structure_P)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_P : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_P()
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
            m_s2D_Array_String_within_max_length = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S2D_Array_String_within_max_length", IsRequired = false, Order = 1)]
        public Variant S2D_Array_String_within_max_length
        {
            get { return m_s2D_Array_String_within_max_length;  }
            set { m_s2D_Array_String_within_max_length = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_P; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_P_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_P_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("S2D_Array_String_within_max_length", S2D_Array_String_within_max_length);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S2D_Array_String_within_max_length = decoder.ReadVariant("S2D_Array_String_within_max_length");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_P value = encodeable as Structure_P;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s2D_Array_String_within_max_length, value.m_s2D_Array_String_within_max_length)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_P)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_P clone = (Structure_P)base.MemberwiseClone();

            clone.m_s2D_Array_String_within_max_length = (Variant)Utils.Clone(this.m_s2D_Array_String_within_max_length);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_s2D_Array_String_within_max_length;
        #endregion
    }

    #region Structure_PCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_P", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_P")]
    #if !NET_STANDARD
    public partial class Structure_PCollection : List<Structure_P>, ICloneable
    #else
    public partial class Structure_PCollection : List<Structure_P>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_PCollection() {}

        /// <remarks />
        public Structure_PCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_PCollection(IEnumerable<Structure_P> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_PCollection(Structure_P[] values)
        {
            if (values != null)
            {
                return new Structure_PCollection(values);
            }

            return new Structure_PCollection();
        }

        /// <remarks />
        public static explicit operator Structure_P[](Structure_PCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_PCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_PCollection clone = new Structure_PCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_P)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_Q Class
    #if (!OPCUA_EXCLUDE_Structure_Q)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_Q : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_Q()
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
            m_s2D_Array_of_BaseDataType = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S2D_Array_of_BaseDataType", IsRequired = false, Order = 1)]
        public Variant S2D_Array_of_BaseDataType
        {
            get { return m_s2D_Array_of_BaseDataType;  }
            set { m_s2D_Array_of_BaseDataType = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_Q; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_Q_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_Q_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("S2D_Array_of_BaseDataType", S2D_Array_of_BaseDataType);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S2D_Array_of_BaseDataType = decoder.ReadVariant("S2D_Array_of_BaseDataType");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_Q value = encodeable as Structure_Q;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s2D_Array_of_BaseDataType, value.m_s2D_Array_of_BaseDataType)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_Q)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_Q clone = (Structure_Q)base.MemberwiseClone();

            clone.m_s2D_Array_of_BaseDataType = (Variant)Utils.Clone(this.m_s2D_Array_of_BaseDataType);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_s2D_Array_of_BaseDataType;
        #endregion
    }

    #region Structure_QCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_Q", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_Q")]
    #if !NET_STANDARD
    public partial class Structure_QCollection : List<Structure_Q>, ICloneable
    #else
    public partial class Structure_QCollection : List<Structure_Q>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_QCollection() {}

        /// <remarks />
        public Structure_QCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_QCollection(IEnumerable<Structure_Q> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_QCollection(Structure_Q[] values)
        {
            if (values != null)
            {
                return new Structure_QCollection(values);
            }

            return new Structure_QCollection();
        }

        /// <remarks />
        public static explicit operator Structure_Q[](Structure_QCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_QCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_QCollection clone = new Structure_QCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_Q)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_R Class
    #if (!OPCUA_EXCLUDE_Structure_R)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_R : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_R()
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
            m_s2D_Array_of_Structure__unknown_ = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Structure__unknown_", IsRequired = false, Order = 1)]
        public Variant S2D_Array_of_Structure__unknown_
        {
            get { return m_s2D_Array_of_Structure__unknown_;  }
            set { m_s2D_Array_of_Structure__unknown_ = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_R; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_R_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_R_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("S2D_Array_of_Structure__unknown_", S2D_Array_of_Structure__unknown_);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S2D_Array_of_Structure__unknown_ = decoder.ReadVariant("S2D_Array_of_Structure__unknown_");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_R value = encodeable as Structure_R;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s2D_Array_of_Structure__unknown_, value.m_s2D_Array_of_Structure__unknown_)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_R)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_R clone = (Structure_R)base.MemberwiseClone();

            clone.m_s2D_Array_of_Structure__unknown_ = (Variant)Utils.Clone(this.m_s2D_Array_of_Structure__unknown_);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_s2D_Array_of_Structure__unknown_;
        #endregion
    }

    #region Structure_RCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_R", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_R")]
    #if !NET_STANDARD
    public partial class Structure_RCollection : List<Structure_R>, ICloneable
    #else
    public partial class Structure_RCollection : List<Structure_R>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_RCollection() {}

        /// <remarks />
        public Structure_RCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_RCollection(IEnumerable<Structure_R> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_RCollection(Structure_R[] values)
        {
            if (values != null)
            {
                return new Structure_RCollection(values);
            }

            return new Structure_RCollection();
        }

        /// <remarks />
        public static explicit operator Structure_R[](Structure_RCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_RCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_RCollection clone = new Structure_RCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_R)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_S Class
    #if (!OPCUA_EXCLUDE_Structure_S)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_S : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_S()
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
            m_s2D_Array_of_Structure__known_ = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Structure__known_", IsRequired = false, Order = 1)]
        public Variant S2D_Array_of_Structure__known_
        {
            get { return m_s2D_Array_of_Structure__known_;  }
            set { m_s2D_Array_of_Structure__known_ = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_S; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_S_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_S_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("S2D_Array_of_Structure__known_", S2D_Array_of_Structure__known_);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S2D_Array_of_Structure__known_ = decoder.ReadVariant("S2D_Array_of_Structure__known_");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_S value = encodeable as Structure_S;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s2D_Array_of_Structure__known_, value.m_s2D_Array_of_Structure__known_)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_S)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_S clone = (Structure_S)base.MemberwiseClone();

            clone.m_s2D_Array_of_Structure__known_ = (Variant)Utils.Clone(this.m_s2D_Array_of_Structure__known_);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_s2D_Array_of_Structure__known_;
        #endregion
    }

    #region Structure_SCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_S", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_S")]
    #if !NET_STANDARD
    public partial class Structure_SCollection : List<Structure_S>, ICloneable
    #else
    public partial class Structure_SCollection : List<Structure_S>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_SCollection() {}

        /// <remarks />
        public Structure_SCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_SCollection(IEnumerable<Structure_S> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_SCollection(Structure_S[] values)
        {
            if (values != null)
            {
                return new Structure_SCollection(values);
            }

            return new Structure_SCollection();
        }

        /// <remarks />
        public static explicit operator Structure_S[](Structure_SCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_SCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_SCollection clone = new Structure_SCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_S)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region Structure_T Class
    #if (!OPCUA_EXCLUDE_Structure_T)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class Structure_T : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public Structure_T()
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
            m_s2D_Array_of_ApplicationDescription = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "S2D_Array_of_ApplicationDescription", IsRequired = false, Order = 1)]
        public Variant S2D_Array_of_ApplicationDescription
        {
            get { return m_s2D_Array_of_ApplicationDescription;  }
            set { m_s2D_Array_of_ApplicationDescription = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.Structure_T; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.Structure_T_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.Structure_T_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("S2D_Array_of_ApplicationDescription", S2D_Array_of_ApplicationDescription);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            S2D_Array_of_ApplicationDescription = decoder.ReadVariant("S2D_Array_of_ApplicationDescription");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            Structure_T value = encodeable as Structure_T;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_s2D_Array_of_ApplicationDescription, value.m_s2D_Array_of_ApplicationDescription)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (Structure_T)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_T clone = (Structure_T)base.MemberwiseClone();

            clone.m_s2D_Array_of_ApplicationDescription = (Variant)Utils.Clone(this.m_s2D_Array_of_ApplicationDescription);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_s2D_Array_of_ApplicationDescription;
        #endregion
    }

    #region Structure_TCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfStructure_T", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "Structure_T")]
    #if !NET_STANDARD
    public partial class Structure_TCollection : List<Structure_T>, ICloneable
    #else
    public partial class Structure_TCollection : List<Structure_T>
    #endif
    {
        #region Constructors
        /// <remarks />
        public Structure_TCollection() {}

        /// <remarks />
        public Structure_TCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public Structure_TCollection(IEnumerable<Structure_T> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator Structure_TCollection(Structure_T[] values)
        {
            if (values != null)
            {
                return new Structure_TCollection(values);
            }

            return new Structure_TCollection();
        }

        /// <remarks />
        public static explicit operator Structure_T[](Structure_TCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (Structure_TCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            Structure_TCollection clone = new Structure_TCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((Structure_T)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region LargeComplexStructure Class
    #if (!OPCUA_EXCLUDE_LargeComplexStructure)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays)]
    public partial class LargeComplexStructure : IEncodeable, IJsonEncodeable
    {
        #region Constructors
        /// <remarks />
        public LargeComplexStructure()
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
            m_scalar_BaseDataType = Variant.Null;
            m_scalar_Structure = null;
            m_scalar_ApplicationDescription = new Opc.Ua.ApplicationDescription();
            m_scalar_Structure_A = new Structure_A();
            m_scalar_Structure_B = new Structure_B();
            m_scalar_Boolean = true;
            m_scalar_Duration = (double)0;
            m_scalar_RolePermissionType = new Opc.Ua.RolePermissionType();
            m_scalar_String_no_max_length = Variant.Null;
            m_scalar_String_within_max_length = Variant.Null;
            m_s1D_Array_String_no_max_length = new StringCollection();
            m_s1D_Array_String_within_max_length = new StringCollection();
            m_s1D_Array_of_BaseDataType = new VariantCollection();
            m_s1D_Array_of_Structure__unknown_ = new ExtensionObjectCollection();
            m_s1D_Array_of_Structure__known_ = new ExtensionObjectCollection();
            m_s1D_Array_of_ApplicationDescription = new ApplicationDescriptionCollection();
            m_s1D_Array_of_Structure_A = new Structure_ACollection();
            m_s1D_Array_of_Structure_B = new Structure_BCollection();
            m_s1D_Array_of_Boolean = new BooleanCollection();
            m_s1D_Array_of_Duration = new DoubleCollection();
            m_s1D_Array_of_RolePermissionType = new RolePermissionTypeCollection();
            m_s2D_Array_of_BaseDataType = Variant.Null;
            m_s2D_Array_of_Structure = Variant.Null;
            m_s2D_Array_of_ApplicationDescription = Variant.Null;
            m_s2D_Array_of_Structure_A = Variant.Null;
            m_s2D_Array_of_Structure_B = Variant.Null;
            m_s2D_Array_of_Boolean = Variant.Null;
            m_s2D_Array_of_Duration = Variant.Null;
            m_s2D_Array_of_RolePermissionType = Variant.Null;
        }
        #endregion

        #region Public Properties
        /// <remarks />
        [DataMember(Name = "Scalar_BaseDataType", IsRequired = false, Order = 1)]
        public Variant Scalar_BaseDataType
        {
            get { return m_scalar_BaseDataType;  }
            set { m_scalar_BaseDataType = value; }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_Structure", IsRequired = false, Order = 2)]
        public ExtensionObject Scalar_Structure
        {
            get { return m_scalar_Structure;  }
            set { m_scalar_Structure = value; }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_ApplicationDescription", IsRequired = false, Order = 3)]
        public Opc.Ua.ApplicationDescription Scalar_ApplicationDescription
        {
            get
            {
                return m_scalar_ApplicationDescription;
            }

            set
            {
                m_scalar_ApplicationDescription = value;

                if (value == null)
                {
                    m_scalar_ApplicationDescription = new Opc.Ua.ApplicationDescription();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_Structure_A", IsRequired = false, Order = 4)]
        public Structure_A Scalar_Structure_A
        {
            get
            {
                return m_scalar_Structure_A;
            }

            set
            {
                m_scalar_Structure_A = value;

                if (value == null)
                {
                    m_scalar_Structure_A = new Structure_A();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_Structure_B", IsRequired = false, Order = 5)]
        public Structure_B Scalar_Structure_B
        {
            get
            {
                return m_scalar_Structure_B;
            }

            set
            {
                m_scalar_Structure_B = value;

                if (value == null)
                {
                    m_scalar_Structure_B = new Structure_B();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_Boolean", IsRequired = false, Order = 6)]
        public bool Scalar_Boolean
        {
            get { return m_scalar_Boolean;  }
            set { m_scalar_Boolean = value; }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_Duration", IsRequired = false, Order = 7)]
        public double Scalar_Duration
        {
            get { return m_scalar_Duration;  }
            set { m_scalar_Duration = value; }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_RolePermissionType", IsRequired = false, Order = 8)]
        public Opc.Ua.RolePermissionType Scalar_RolePermissionType
        {
            get
            {
                return m_scalar_RolePermissionType;
            }

            set
            {
                m_scalar_RolePermissionType = value;

                if (value == null)
                {
                    m_scalar_RolePermissionType = new Opc.Ua.RolePermissionType();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_String_no_max_length", IsRequired = false, Order = 9)]
        public Variant Scalar_String_no_max_length
        {
            get { return m_scalar_String_no_max_length;  }
            set { m_scalar_String_no_max_length = value; }
        }

        /// <remarks />
        [DataMember(Name = "Scalar_String_within_max_length", IsRequired = false, Order = 10)]
        public Variant Scalar_String_within_max_length
        {
            get { return m_scalar_String_within_max_length;  }
            set { m_scalar_String_within_max_length = value; }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_String_no_max_length", IsRequired = false, Order = 11)]
        public StringCollection S1D_Array_String_no_max_length
        {
            get
            {
                return m_s1D_Array_String_no_max_length;
            }

            set
            {
                m_s1D_Array_String_no_max_length = value;

                if (value == null)
                {
                    m_s1D_Array_String_no_max_length = new StringCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_String_within_max_length", IsRequired = false, Order = 12)]
        public StringCollection S1D_Array_String_within_max_length
        {
            get
            {
                return m_s1D_Array_String_within_max_length;
            }

            set
            {
                m_s1D_Array_String_within_max_length = value;

                if (value == null)
                {
                    m_s1D_Array_String_within_max_length = new StringCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_BaseDataType", IsRequired = false, Order = 13)]
        public VariantCollection S1D_Array_of_BaseDataType
        {
            get
            {
                return m_s1D_Array_of_BaseDataType;
            }

            set
            {
                m_s1D_Array_of_BaseDataType = value;

                if (value == null)
                {
                    m_s1D_Array_of_BaseDataType = new VariantCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure__unknown_", IsRequired = false, Order = 14)]
        public ExtensionObjectCollection S1D_Array_of_Structure__unknown_
        {
            get
            {
                return m_s1D_Array_of_Structure__unknown_;
            }

            set
            {
                m_s1D_Array_of_Structure__unknown_ = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure__unknown_ = new ExtensionObjectCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure__known_", IsRequired = false, Order = 15)]
        public ExtensionObjectCollection S1D_Array_of_Structure__known_
        {
            get
            {
                return m_s1D_Array_of_Structure__known_;
            }

            set
            {
                m_s1D_Array_of_Structure__known_ = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure__known_ = new ExtensionObjectCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_ApplicationDescription", IsRequired = false, Order = 16)]
        public ApplicationDescriptionCollection S1D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s1D_Array_of_ApplicationDescription;
            }

            set
            {
                m_s1D_Array_of_ApplicationDescription = value;

                if (value == null)
                {
                    m_s1D_Array_of_ApplicationDescription = new ApplicationDescriptionCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure_A", IsRequired = false, Order = 17)]
        public Structure_ACollection S1D_Array_of_Structure_A
        {
            get
            {
                return m_s1D_Array_of_Structure_A;
            }

            set
            {
                m_s1D_Array_of_Structure_A = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure_A = new Structure_ACollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Structure_B", IsRequired = false, Order = 18)]
        public Structure_BCollection S1D_Array_of_Structure_B
        {
            get
            {
                return m_s1D_Array_of_Structure_B;
            }

            set
            {
                m_s1D_Array_of_Structure_B = value;

                if (value == null)
                {
                    m_s1D_Array_of_Structure_B = new Structure_BCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Boolean", IsRequired = false, Order = 19)]
        public BooleanCollection S1D_Array_of_Boolean
        {
            get
            {
                return m_s1D_Array_of_Boolean;
            }

            set
            {
                m_s1D_Array_of_Boolean = value;

                if (value == null)
                {
                    m_s1D_Array_of_Boolean = new BooleanCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_Duration", IsRequired = false, Order = 20)]
        public DoubleCollection S1D_Array_of_Duration
        {
            get
            {
                return m_s1D_Array_of_Duration;
            }

            set
            {
                m_s1D_Array_of_Duration = value;

                if (value == null)
                {
                    m_s1D_Array_of_Duration = new DoubleCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S1D_Array_of_RolePermissionType", IsRequired = false, Order = 21)]
        public RolePermissionTypeCollection S1D_Array_of_RolePermissionType
        {
            get
            {
                return m_s1D_Array_of_RolePermissionType;
            }

            set
            {
                m_s1D_Array_of_RolePermissionType = value;

                if (value == null)
                {
                    m_s1D_Array_of_RolePermissionType = new RolePermissionTypeCollection();
                }
            }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_BaseDataType", IsRequired = false, Order = 22)]
        public Variant S2D_Array_of_BaseDataType
        {
            get { return m_s2D_Array_of_BaseDataType;  }
            set { m_s2D_Array_of_BaseDataType = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Structure", IsRequired = false, Order = 23)]
        public Variant S2D_Array_of_Structure
        {
            get { return m_s2D_Array_of_Structure;  }
            set { m_s2D_Array_of_Structure = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_ApplicationDescription", IsRequired = false, Order = 24)]
        public Variant S2D_Array_of_ApplicationDescription
        {
            get { return m_s2D_Array_of_ApplicationDescription;  }
            set { m_s2D_Array_of_ApplicationDescription = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Structure_A", IsRequired = false, Order = 25)]
        public Variant S2D_Array_of_Structure_A
        {
            get { return m_s2D_Array_of_Structure_A;  }
            set { m_s2D_Array_of_Structure_A = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Structure_B", IsRequired = false, Order = 26)]
        public Variant S2D_Array_of_Structure_B
        {
            get { return m_s2D_Array_of_Structure_B;  }
            set { m_s2D_Array_of_Structure_B = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Boolean", IsRequired = false, Order = 27)]
        public Variant S2D_Array_of_Boolean
        {
            get { return m_s2D_Array_of_Boolean;  }
            set { m_s2D_Array_of_Boolean = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_Duration", IsRequired = false, Order = 28)]
        public Variant S2D_Array_of_Duration
        {
            get { return m_s2D_Array_of_Duration;  }
            set { m_s2D_Array_of_Duration = value; }
        }

        /// <remarks />
        [DataMember(Name = "S2D_Array_of_RolePermissionType", IsRequired = false, Order = 29)]
        public Variant S2D_Array_of_RolePermissionType
        {
            get { return m_s2D_Array_of_RolePermissionType;  }
            set { m_s2D_Array_of_RolePermissionType = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId => DataTypeIds.LargeComplexStructure; 

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId => ObjectIds.LargeComplexStructure_Encoding_DefaultBinary;

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId => ObjectIds.LargeComplexStructure_Encoding_DefaultXml;
                    
        /// <summary cref="IJsonEncodeable.JsonEncodingId" />
        public virtual ExpandedNodeId JsonEncodingId => throw new NotSupportedException(); 

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            encoder.WriteVariant("Scalar_BaseDataType", Scalar_BaseDataType);
            encoder.WriteExtensionObject("Scalar_Structure", Scalar_Structure);
            encoder.WriteEncodeable("Scalar_ApplicationDescription", Scalar_ApplicationDescription, typeof(Opc.Ua.ApplicationDescription));
            encoder.WriteEncodeable("Scalar_Structure_A", Scalar_Structure_A, typeof(Structure_A));
            encoder.WriteEncodeable("Scalar_Structure_B", Scalar_Structure_B, typeof(Structure_B));
            encoder.WriteBoolean("Scalar_Boolean", Scalar_Boolean);
            encoder.WriteDouble("Scalar_Duration", Scalar_Duration);
            encoder.WriteEncodeable("Scalar_RolePermissionType", Scalar_RolePermissionType, typeof(Opc.Ua.RolePermissionType));
            encoder.WriteVariant("Scalar_String_no_max_length", Scalar_String_no_max_length);
            encoder.WriteVariant("Scalar_String_within_max_length", Scalar_String_within_max_length);
            encoder.WriteStringArray("S1D_Array_String_no_max_length", S1D_Array_String_no_max_length);
            encoder.WriteStringArray("S1D_Array_String_within_max_length", S1D_Array_String_within_max_length);
            encoder.WriteVariantArray("S1D_Array_of_BaseDataType", S1D_Array_of_BaseDataType);
            encoder.WriteExtensionObjectArray("S1D_Array_of_Structure__unknown_", S1D_Array_of_Structure__unknown_);
            encoder.WriteExtensionObjectArray("S1D_Array_of_Structure__known_", S1D_Array_of_Structure__known_);
            encoder.WriteEncodeableArray("S1D_Array_of_ApplicationDescription", S1D_Array_of_ApplicationDescription.ToArray(), typeof(Opc.Ua.ApplicationDescription));
            encoder.WriteEncodeableArray("S1D_Array_of_Structure_A", S1D_Array_of_Structure_A.ToArray(), typeof(Structure_A));
            encoder.WriteEncodeableArray("S1D_Array_of_Structure_B", S1D_Array_of_Structure_B.ToArray(), typeof(Structure_B));
            encoder.WriteBooleanArray("S1D_Array_of_Boolean", S1D_Array_of_Boolean);
            encoder.WriteDoubleArray("S1D_Array_of_Duration", S1D_Array_of_Duration);
            encoder.WriteEncodeableArray("S1D_Array_of_RolePermissionType", S1D_Array_of_RolePermissionType.ToArray(), typeof(Opc.Ua.RolePermissionType));
            encoder.WriteVariant("S2D_Array_of_BaseDataType", S2D_Array_of_BaseDataType);
            encoder.WriteVariant("S2D_Array_of_Structure", S2D_Array_of_Structure);
            encoder.WriteVariant("S2D_Array_of_ApplicationDescription", S2D_Array_of_ApplicationDescription);
            encoder.WriteVariant("S2D_Array_of_Structure_A", S2D_Array_of_Structure_A);
            encoder.WriteVariant("S2D_Array_of_Structure_B", S2D_Array_of_Structure_B);
            encoder.WriteVariant("S2D_Array_of_Boolean", S2D_Array_of_Boolean);
            encoder.WriteVariant("S2D_Array_of_Duration", S2D_Array_of_Duration);
            encoder.WriteVariant("S2D_Array_of_RolePermissionType", S2D_Array_of_RolePermissionType);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(StructuresWithArrays.Namespaces.StructuresWithArrays);

            Scalar_BaseDataType = decoder.ReadVariant("Scalar_BaseDataType");
            Scalar_Structure = decoder.ReadExtensionObject("Scalar_Structure");
            Scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)decoder.ReadEncodeable("Scalar_ApplicationDescription", typeof(Opc.Ua.ApplicationDescription));
            Scalar_Structure_A = (Structure_A)decoder.ReadEncodeable("Scalar_Structure_A", typeof(Structure_A));
            Scalar_Structure_B = (Structure_B)decoder.ReadEncodeable("Scalar_Structure_B", typeof(Structure_B));
            Scalar_Boolean = decoder.ReadBoolean("Scalar_Boolean");
            Scalar_Duration = decoder.ReadDouble("Scalar_Duration");
            Scalar_RolePermissionType = (Opc.Ua.RolePermissionType)decoder.ReadEncodeable("Scalar_RolePermissionType", typeof(Opc.Ua.RolePermissionType));
            Scalar_String_no_max_length = decoder.ReadVariant("Scalar_String_no_max_length");
            Scalar_String_within_max_length = decoder.ReadVariant("Scalar_String_within_max_length");
            S1D_Array_String_no_max_length = decoder.ReadStringArray("S1D_Array_String_no_max_length");
            S1D_Array_String_within_max_length = decoder.ReadStringArray("S1D_Array_String_within_max_length");
            S1D_Array_of_BaseDataType = decoder.ReadVariantArray("S1D_Array_of_BaseDataType");
            S1D_Array_of_Structure__unknown_ = decoder.ReadExtensionObjectArray("S1D_Array_of_Structure__unknown_");
            S1D_Array_of_Structure__known_ = decoder.ReadExtensionObjectArray("S1D_Array_of_Structure__known_");
            S1D_Array_of_ApplicationDescription = (Opc.Ua.ApplicationDescriptionCollection)decoder.ReadEncodeableArray("S1D_Array_of_ApplicationDescription", typeof(Opc.Ua.ApplicationDescription));
            S1D_Array_of_Structure_A = (Structure_ACollection)decoder.ReadEncodeableArray("S1D_Array_of_Structure_A", typeof(Structure_A));
            S1D_Array_of_Structure_B = (Structure_BCollection)decoder.ReadEncodeableArray("S1D_Array_of_Structure_B", typeof(Structure_B));
            S1D_Array_of_Boolean = decoder.ReadBooleanArray("S1D_Array_of_Boolean");
            S1D_Array_of_Duration = decoder.ReadDoubleArray("S1D_Array_of_Duration");
            S1D_Array_of_RolePermissionType = (Opc.Ua.RolePermissionTypeCollection)decoder.ReadEncodeableArray("S1D_Array_of_RolePermissionType", typeof(Opc.Ua.RolePermissionType));
            S2D_Array_of_BaseDataType = decoder.ReadVariant("S2D_Array_of_BaseDataType");
            S2D_Array_of_Structure = decoder.ReadVariant("S2D_Array_of_Structure");
            S2D_Array_of_ApplicationDescription = decoder.ReadVariant("S2D_Array_of_ApplicationDescription");
            S2D_Array_of_Structure_A = decoder.ReadVariant("S2D_Array_of_Structure_A");
            S2D_Array_of_Structure_B = decoder.ReadVariant("S2D_Array_of_Structure_B");
            S2D_Array_of_Boolean = decoder.ReadVariant("S2D_Array_of_Boolean");
            S2D_Array_of_Duration = decoder.ReadVariant("S2D_Array_of_Duration");
            S2D_Array_of_RolePermissionType = decoder.ReadVariant("S2D_Array_of_RolePermissionType");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            LargeComplexStructure value = encodeable as LargeComplexStructure;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_scalar_BaseDataType, value.m_scalar_BaseDataType)) return false;
            if (!Utils.IsEqual(m_scalar_Structure, value.m_scalar_Structure)) return false;
            if (!Utils.IsEqual(m_scalar_ApplicationDescription, value.m_scalar_ApplicationDescription)) return false;
            if (!Utils.IsEqual(m_scalar_Structure_A, value.m_scalar_Structure_A)) return false;
            if (!Utils.IsEqual(m_scalar_Structure_B, value.m_scalar_Structure_B)) return false;
            if (!Utils.IsEqual(m_scalar_Boolean, value.m_scalar_Boolean)) return false;
            if (!Utils.IsEqual(m_scalar_Duration, value.m_scalar_Duration)) return false;
            if (!Utils.IsEqual(m_scalar_RolePermissionType, value.m_scalar_RolePermissionType)) return false;
            if (!Utils.IsEqual(m_scalar_String_no_max_length, value.m_scalar_String_no_max_length)) return false;
            if (!Utils.IsEqual(m_scalar_String_within_max_length, value.m_scalar_String_within_max_length)) return false;
            if (!Utils.IsEqual(m_s1D_Array_String_no_max_length, value.m_s1D_Array_String_no_max_length)) return false;
            if (!Utils.IsEqual(m_s1D_Array_String_within_max_length, value.m_s1D_Array_String_within_max_length)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_BaseDataType, value.m_s1D_Array_of_BaseDataType)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Structure__unknown_, value.m_s1D_Array_of_Structure__unknown_)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Structure__known_, value.m_s1D_Array_of_Structure__known_)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_ApplicationDescription, value.m_s1D_Array_of_ApplicationDescription)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Structure_A, value.m_s1D_Array_of_Structure_A)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Structure_B, value.m_s1D_Array_of_Structure_B)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Boolean, value.m_s1D_Array_of_Boolean)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_Duration, value.m_s1D_Array_of_Duration)) return false;
            if (!Utils.IsEqual(m_s1D_Array_of_RolePermissionType, value.m_s1D_Array_of_RolePermissionType)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_BaseDataType, value.m_s2D_Array_of_BaseDataType)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_Structure, value.m_s2D_Array_of_Structure)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_ApplicationDescription, value.m_s2D_Array_of_ApplicationDescription)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_Structure_A, value.m_s2D_Array_of_Structure_A)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_Structure_B, value.m_s2D_Array_of_Structure_B)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_Boolean, value.m_s2D_Array_of_Boolean)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_Duration, value.m_s2D_Array_of_Duration)) return false;
            if (!Utils.IsEqual(m_s2D_Array_of_RolePermissionType, value.m_s2D_Array_of_RolePermissionType)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (LargeComplexStructure)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            LargeComplexStructure clone = (LargeComplexStructure)base.MemberwiseClone();

            clone.m_scalar_BaseDataType = (Variant)Utils.Clone(this.m_scalar_BaseDataType);
            clone.m_scalar_Structure = (ExtensionObject)Utils.Clone(this.m_scalar_Structure);
            clone.m_scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)Utils.Clone(this.m_scalar_ApplicationDescription);
            clone.m_scalar_Structure_A = (Structure_A)Utils.Clone(this.m_scalar_Structure_A);
            clone.m_scalar_Structure_B = (Structure_B)Utils.Clone(this.m_scalar_Structure_B);
            clone.m_scalar_Boolean = (bool)Utils.Clone(this.m_scalar_Boolean);
            clone.m_scalar_Duration = (double)Utils.Clone(this.m_scalar_Duration);
            clone.m_scalar_RolePermissionType = (Opc.Ua.RolePermissionType)Utils.Clone(this.m_scalar_RolePermissionType);
            clone.m_scalar_String_no_max_length = (Variant)Utils.Clone(this.m_scalar_String_no_max_length);
            clone.m_scalar_String_within_max_length = (Variant)Utils.Clone(this.m_scalar_String_within_max_length);
            clone.m_s1D_Array_String_no_max_length = (StringCollection)Utils.Clone(this.m_s1D_Array_String_no_max_length);
            clone.m_s1D_Array_String_within_max_length = (StringCollection)Utils.Clone(this.m_s1D_Array_String_within_max_length);
            clone.m_s1D_Array_of_BaseDataType = (VariantCollection)Utils.Clone(this.m_s1D_Array_of_BaseDataType);
            clone.m_s1D_Array_of_Structure__unknown_ = (ExtensionObjectCollection)Utils.Clone(this.m_s1D_Array_of_Structure__unknown_);
            clone.m_s1D_Array_of_Structure__known_ = (ExtensionObjectCollection)Utils.Clone(this.m_s1D_Array_of_Structure__known_);
            clone.m_s1D_Array_of_ApplicationDescription = (ApplicationDescriptionCollection)Utils.Clone(this.m_s1D_Array_of_ApplicationDescription);
            clone.m_s1D_Array_of_Structure_A = (Structure_ACollection)Utils.Clone(this.m_s1D_Array_of_Structure_A);
            clone.m_s1D_Array_of_Structure_B = (Structure_BCollection)Utils.Clone(this.m_s1D_Array_of_Structure_B);
            clone.m_s1D_Array_of_Boolean = (BooleanCollection)Utils.Clone(this.m_s1D_Array_of_Boolean);
            clone.m_s1D_Array_of_Duration = (DoubleCollection)Utils.Clone(this.m_s1D_Array_of_Duration);
            clone.m_s1D_Array_of_RolePermissionType = (RolePermissionTypeCollection)Utils.Clone(this.m_s1D_Array_of_RolePermissionType);
            clone.m_s2D_Array_of_BaseDataType = (Variant)Utils.Clone(this.m_s2D_Array_of_BaseDataType);
            clone.m_s2D_Array_of_Structure = (Variant)Utils.Clone(this.m_s2D_Array_of_Structure);
            clone.m_s2D_Array_of_ApplicationDescription = (Variant)Utils.Clone(this.m_s2D_Array_of_ApplicationDescription);
            clone.m_s2D_Array_of_Structure_A = (Variant)Utils.Clone(this.m_s2D_Array_of_Structure_A);
            clone.m_s2D_Array_of_Structure_B = (Variant)Utils.Clone(this.m_s2D_Array_of_Structure_B);
            clone.m_s2D_Array_of_Boolean = (Variant)Utils.Clone(this.m_s2D_Array_of_Boolean);
            clone.m_s2D_Array_of_Duration = (Variant)Utils.Clone(this.m_s2D_Array_of_Duration);
            clone.m_s2D_Array_of_RolePermissionType = (Variant)Utils.Clone(this.m_s2D_Array_of_RolePermissionType);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_scalar_BaseDataType;
        private ExtensionObject m_scalar_Structure;
        private Opc.Ua.ApplicationDescription m_scalar_ApplicationDescription;
        private Structure_A m_scalar_Structure_A;
        private Structure_B m_scalar_Structure_B;
        private bool m_scalar_Boolean;
        private double m_scalar_Duration;
        private Opc.Ua.RolePermissionType m_scalar_RolePermissionType;
        private Variant m_scalar_String_no_max_length;
        private Variant m_scalar_String_within_max_length;
        private StringCollection m_s1D_Array_String_no_max_length;
        private StringCollection m_s1D_Array_String_within_max_length;
        private VariantCollection m_s1D_Array_of_BaseDataType;
        private ExtensionObjectCollection m_s1D_Array_of_Structure__unknown_;
        private ExtensionObjectCollection m_s1D_Array_of_Structure__known_;
        private ApplicationDescriptionCollection m_s1D_Array_of_ApplicationDescription;
        private Structure_ACollection m_s1D_Array_of_Structure_A;
        private Structure_BCollection m_s1D_Array_of_Structure_B;
        private BooleanCollection m_s1D_Array_of_Boolean;
        private DoubleCollection m_s1D_Array_of_Duration;
        private RolePermissionTypeCollection m_s1D_Array_of_RolePermissionType;
        private Variant m_s2D_Array_of_BaseDataType;
        private Variant m_s2D_Array_of_Structure;
        private Variant m_s2D_Array_of_ApplicationDescription;
        private Variant m_s2D_Array_of_Structure_A;
        private Variant m_s2D_Array_of_Structure_B;
        private Variant m_s2D_Array_of_Boolean;
        private Variant m_s2D_Array_of_Duration;
        private Variant m_s2D_Array_of_RolePermissionType;
        #endregion
    }

    #region LargeComplexStructureCollection Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfLargeComplexStructure", Namespace = StructuresWithArrays.Namespaces.StructuresWithArrays, ItemName = "LargeComplexStructure")]
    #if !NET_STANDARD
    public partial class LargeComplexStructureCollection : List<LargeComplexStructure>, ICloneable
    #else
    public partial class LargeComplexStructureCollection : List<LargeComplexStructure>
    #endif
    {
        #region Constructors
        /// <remarks />
        public LargeComplexStructureCollection() {}

        /// <remarks />
        public LargeComplexStructureCollection(int capacity) : base(capacity) {}

        /// <remarks />
        public LargeComplexStructureCollection(IEnumerable<LargeComplexStructure> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <remarks />
        public static implicit operator LargeComplexStructureCollection(LargeComplexStructure[] values)
        {
            if (values != null)
            {
                return new LargeComplexStructureCollection(values);
            }

            return new LargeComplexStructureCollection();
        }

        /// <remarks />
        public static explicit operator LargeComplexStructure[](LargeComplexStructureCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <remarks />
        public object Clone()
        {
            return (LargeComplexStructureCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            LargeComplexStructureCollection clone = new LargeComplexStructureCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((LargeComplexStructure)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion
}