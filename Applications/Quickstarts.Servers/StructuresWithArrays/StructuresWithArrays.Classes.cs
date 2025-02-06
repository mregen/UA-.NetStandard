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
    #region Structure_A_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_A_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_A_TypeState : BaseDataVariableState<Structure_A>
    {
        #region Constructors
        /// <remarks />
        public Structure_A_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_A_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0FfVHlwZUluc3RhbmNlAQHRBwEB0QfRBwAAAQHoA/////8BAf////8CAAAA" +
           "FWDJCgIAAAAOAAAARHVyYXRpb25fRmllbGQBAA4AAABEdXJhdGlvbiBGaWVsZAEBcBcALwA/cBcAAAEA" +
           "IgH/////AwP/////AAAAABVgyQoCAAAADAAAAERvdWJsZV9GaWVsZAEADAAAAERvdWJsZSBGaWVsZAEB" +
           "cRcALwA/cRcAAAAL/////wMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<double> Duration_Field
        {
            get
            {
                return m_duration_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_duration_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_duration_Field = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<double> Double_Field
        {
            get
            {
                return m_double_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_double_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_double_Field = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_duration_Field != null)
            {
                children.Add(m_duration_Field);
            }

            if (m_double_Field != null)
            {
                children.Add(m_double_Field);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Duration_Field:
                {
                    if (createOrReplace)
                    {
                        if (Duration_Field == null)
                        {
                            if (replacement == null)
                            {
                                Duration_Field = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Duration_Field = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Duration_Field;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Double_Field:
                {
                    if (createOrReplace)
                    {
                        if (Double_Field == null)
                        {
                            if (replacement == null)
                            {
                                Double_Field = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Double_Field = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Double_Field;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<double> m_duration_Field;
        private BaseDataVariableState<double> m_double_Field;
        #endregion
    }

    #region Structure_A_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_A_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_A_TypeValue(Structure_A_TypeState variable, Structure_A value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_A();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_A_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_A Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_A_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Duration_Field;
                instance.OnReadValue = OnRead_Duration_Field;
                instance.OnSimpleWriteValue = OnWrite_Duration_Field;
                updateList.Add(instance);
                instance = m_variable.Double_Field;
                instance.OnReadValue = OnRead_Double_Field;
                instance.OnSimpleWriteValue = OnWrite_Double_Field;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_A)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Duration_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_Duration_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Duration_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Duration_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Duration_Field = (double)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Double_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_Double_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Double_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Double_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Double_Field = (double)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_A m_value;
        private Structure_A_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_B_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_B_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_B_TypeState : BaseDataVariableState<Structure_B>
    {
        #region Constructors
        /// <remarks />
        public Structure_B_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_B_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (Duration_Field != null)
            {
                Duration_Field.Initialize(context, Duration_Field_InitializationString);
            }
        }

        #region Initialization String
        private const string Duration_Field_InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgyQoCAAAA" +
           "DgAAAER1cmF0aW9uX0ZpZWxkAQAOAAAARHVyYXRpb24gRmllbGQBAXIXAC8AP3IXAAABACIB/////wMD" +
           "/////wAAAAA=";

        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0JfVHlwZUluc3RhbmNlAQHSBwEB0gfSBwAAAQHpA/////8BAf////8CAAAA" +
           "FWDJCgIAAAAOAAAARHVyYXRpb25fRmllbGQBAA4AAABEdXJhdGlvbiBGaWVsZAEBchcALwA/chcAAAEA" +
           "IgH/////AwP/////AAAAABVgyQoCAAAACwAAAEludDMyX0ZpZWxkAQALAAAASW50MzIgRmllbGQBAXMX" +
           "AC8AP3MXAAAABv////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<double> Duration_Field
        {
            get
            {
                return m_duration_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_duration_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_duration_Field = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<int> Int32_Field
        {
            get
            {
                return m_int32_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_int32_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_int32_Field = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_duration_Field != null)
            {
                children.Add(m_duration_Field);
            }

            if (m_int32_Field != null)
            {
                children.Add(m_int32_Field);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Duration_Field:
                {
                    if (createOrReplace)
                    {
                        if (Duration_Field == null)
                        {
                            if (replacement == null)
                            {
                                Duration_Field = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Duration_Field = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Duration_Field;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Int32_Field:
                {
                    if (createOrReplace)
                    {
                        if (Int32_Field == null)
                        {
                            if (replacement == null)
                            {
                                Int32_Field = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                Int32_Field = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = Int32_Field;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<double> m_duration_Field;
        private BaseDataVariableState<int> m_int32_Field;
        #endregion
    }

    #region Structure_B_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_B_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_B_TypeValue(Structure_B_TypeState variable, Structure_B value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_B();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_B_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_B Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_B_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Duration_Field;
                instance.OnReadValue = OnRead_Duration_Field;
                instance.OnSimpleWriteValue = OnWrite_Duration_Field;
                updateList.Add(instance);
                instance = m_variable.Int32_Field;
                instance.OnReadValue = OnRead_Int32_Field;
                instance.OnSimpleWriteValue = OnWrite_Int32_Field;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_B)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Duration_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_Duration_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Duration_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Duration_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Duration_Field = (double)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Int32_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_Int32_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Int32_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Int32_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Int32_Field = (int)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_B m_value;
        private Structure_B_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_C_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_C_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_C_TypeState : BaseDataVariableState<Structure_C>
    {
        #region Constructors
        /// <remarks />
        public Structure_C_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_C_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_C, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0NfVHlwZUluc3RhbmNlAQHTBwEB0wfTBwAAAQHqA/////8BAf////8CAAAA" +
           "FWDJCgIAAAALAAAASW50MzJfRmllbGQBAAsAAABJbnQzMiBGaWVsZAEBdBcALwA/dBcAAAAG/////wMD" +
           "/////wAAAAAVYMkKAgAAAAwAAABOb2RlSWRfRmllbGQBAAwAAABOb2RlSWQgRmllbGQBAXUXAC8AP3UX" +
           "AAAAEf////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<int> Int32_Field
        {
            get
            {
                return m_int32_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_int32_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_int32_Field = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<NodeId> NodeId_Field
        {
            get
            {
                return m_nodeId_Field;
            }

            set
            {
                if (!Object.ReferenceEquals(m_nodeId_Field, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_nodeId_Field = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_int32_Field != null)
            {
                children.Add(m_int32_Field);
            }

            if (m_nodeId_Field != null)
            {
                children.Add(m_nodeId_Field);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Int32_Field:
                {
                    if (createOrReplace)
                    {
                        if (Int32_Field == null)
                        {
                            if (replacement == null)
                            {
                                Int32_Field = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                Int32_Field = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = Int32_Field;
                    break;
                }

                case StructuresWithArrays.BrowseNames.NodeId_Field:
                {
                    if (createOrReplace)
                    {
                        if (NodeId_Field == null)
                        {
                            if (replacement == null)
                            {
                                NodeId_Field = new BaseDataVariableState<NodeId>(this);
                            }
                            else
                            {
                                NodeId_Field = (BaseDataVariableState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = NodeId_Field;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<int> m_int32_Field;
        private BaseDataVariableState<NodeId> m_nodeId_Field;
        #endregion
    }

    #region Structure_C_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_C_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_C_TypeValue(Structure_C_TypeState variable, Structure_C value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_C();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_C_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_C Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_C_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Int32_Field;
                instance.OnReadValue = OnRead_Int32_Field;
                instance.OnSimpleWriteValue = OnWrite_Int32_Field;
                updateList.Add(instance);
                instance = m_variable.NodeId_Field;
                instance.OnReadValue = OnRead_NodeId_Field;
                instance.OnSimpleWriteValue = OnWrite_NodeId_Field;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_C)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Int32_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_Int32_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Int32_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Int32_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Int32_Field = (int)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region NodeId_Field Access Methods
        /// <remarks />
        private ServiceResult OnRead_NodeId_Field(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.NodeId_Field;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_NodeId_Field(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.NodeId_Field = (NodeId)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_C m_value;
        private Structure_C_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_D_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_D_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_D_TypeState : BaseDataVariableState<Structure_D>
    {
        #region Constructors
        /// <remarks />
        public Structure_D_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_D_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_D, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0RfVHlwZUluc3RhbmNlAQHUBwEB1AfUBwAAAQHrA/////8BAf////8BAAAA" +
           "FWDJCgIAAAATAAAAU2NhbGFyX0Jhc2VEYXRhVHlwZQEAEwAAAFNjYWxhciBCYXNlRGF0YVR5cGUBAXYX" +
           "AC8AP3YXAAAAGP////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState Scalar_BaseDataType
        {
            get
            {
                return m_scalar_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_BaseDataType = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_BaseDataType != null)
            {
                children.Add(m_scalar_BaseDataType);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_BaseDataType = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Scalar_BaseDataType = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Scalar_BaseDataType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_scalar_BaseDataType;
        #endregion
    }

    #region Structure_D_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_D_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_D_TypeValue(Structure_D_TypeState variable, Structure_D value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_D();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_D_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_D Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_D_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_BaseDataType;
                instance.OnReadValue = OnRead_Scalar_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_Scalar_BaseDataType;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_D)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_BaseDataType = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_D m_value;
        private Structure_D_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_E_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_E_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_E_TypeState : BaseDataVariableState<Structure_E>
    {
        #region Constructors
        /// <remarks />
        public Structure_E_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_E_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_E, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0VfVHlwZUluc3RhbmNlAQHVBwEB1QfVBwAAAQHsA/////8BAf////8BAAAA" +
           "FWDJCgIAAAAQAAAAU2NhbGFyX1N0cnVjdHVyZQEAEAAAAFNjYWxhciBTdHJ1Y3R1cmUBAXcXAC8AP3cX" +
           "AAAAFv////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<ExtensionObject> Scalar_Structure
        {
            get
            {
                return m_scalar_Structure;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Structure, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Structure = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_Structure != null)
            {
                children.Add(m_scalar_Structure);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_Structure:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Structure == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Structure = new BaseDataVariableState<ExtensionObject>(this);
                            }
                            else
                            {
                                Scalar_Structure = (BaseDataVariableState<ExtensionObject>)replacement;
                            }
                        }
                    }

                    instance = Scalar_Structure;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<ExtensionObject> m_scalar_Structure;
        #endregion
    }

    #region Structure_E_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_E_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_E_TypeValue(Structure_E_TypeState variable, Structure_E value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_E();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_E_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_E Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_E_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_Structure;
                instance.OnReadValue = OnRead_Scalar_Structure;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Structure;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_E)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_Structure Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Structure(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Structure;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Structure(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Structure = (ExtensionObject)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_E m_value;
        private Structure_E_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_F_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_F_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_F_TypeState : BaseDataVariableState<Structure_F>
    {
        #region Constructors
        /// <remarks />
        public Structure_F_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_F_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_F, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0ZfVHlwZUluc3RhbmNlAQHWBwEB1gfWBwAAAQHtA/////8BAf////8BAAAA" +
           "FWDJCgIAAAAdAAAAU2NhbGFyX0FwcGxpY2F0aW9uRGVzY3JpcHRpb24BAB0AAABTY2FsYXIgQXBwbGlj" +
           "YXRpb25EZXNjcmlwdGlvbgEBfBcALwA/fBcAAAEANAH/////AwP/////BwAAABVgiQoCAAAAAQAOAAAA" +
           "QXBwbGljYXRpb25VcmkBAX0XAC8AP30XAAAADP////8DA/////8AAAAAFWCJCgIAAAABAAoAAABQcm9k" +
           "dWN0VXJpAQF+FwAvAD9+FwAAAAz/////AwP/////AAAAABVgiQoCAAAAAQAPAAAAQXBwbGljYXRpb25O" +
           "YW1lAQF/FwAvAD9/FwAAABX/////AwP/////AAAAABVgiQoCAAAAAQAPAAAAQXBwbGljYXRpb25UeXBl" +
           "AQGAFwAvAD+AFwAAAQAzAf////8DA/////8AAAAAFWCJCgIAAAABABAAAABHYXRld2F5U2VydmVyVXJp" +
           "AQGBFwAvAD+BFwAAAAz/////AwP/////AAAAABVgiQoCAAAAAQATAAAARGlzY292ZXJ5UHJvZmlsZVVy" +
           "aQEBghcALwA/ghcAAAAM/////wMD/////wAAAAAXYIkKAgAAAAEADQAAAERpc2NvdmVyeVVybHMBAYMX" +
           "AC8AP4MXAAAADAEAAAABAAAAAAAAAAMD/////wEAAAAVYMkKAgAAABEAAABEaXNjb3ZlcnlVcmxzX25v" +
           "XwEAEQAAAERpc2NvdmVyeVVybHM8bm8+AQGEFwAvAD+EFwAAAAz/////AwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<ApplicationDescription> Scalar_ApplicationDescription
        {
            get
            {
                return m_scalar_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_ApplicationDescription = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_ApplicationDescription != null)
            {
                children.Add(m_scalar_ApplicationDescription);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_ApplicationDescription = new BaseDataVariableState<ApplicationDescription>(this);
                            }
                            else
                            {
                                Scalar_ApplicationDescription = (BaseDataVariableState<ApplicationDescription>)replacement;
                            }
                        }
                    }

                    instance = Scalar_ApplicationDescription;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<ApplicationDescription> m_scalar_ApplicationDescription;
        #endregion
    }

    #region Structure_F_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_F_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_F_TypeValue(Structure_F_TypeState variable, Structure_F value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_F();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_F_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_F Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_F_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_ApplicationDescription;
                instance.OnReadValue = OnRead_Scalar_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_Scalar_ApplicationDescription;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_F)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_F m_value;
        private Structure_F_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_G_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_G_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_G_TypeState : BaseDataVariableState<Structure_G>
    {
        #region Constructors
        /// <remarks />
        public Structure_G_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_G_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_G, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0dfVHlwZUluc3RhbmNlAQHXBwEB1wfXBwAAAQHuA/////8BAf////8BAAAA" +
           "FWDJCgIAAAASAAAAU2NhbGFyX1N0cnVjdHVyZV9BAQASAAAAU2NhbGFyIFN0cnVjdHVyZSBBAQGFFwAv" +
           "AQHRB4UXAAABAegD/////wMD/////wIAAAAVYMkKAgAAAA4AAABEdXJhdGlvbl9GaWVsZAEADgAAAER1" +
           "cmF0aW9uIEZpZWxkAQFwFwAvAD9wFwAAAQAiAf////8DA/////8AAAAAFWDJCgIAAAAMAAAARG91Ymxl" +
           "X0ZpZWxkAQAMAAAARG91YmxlIEZpZWxkAQFxFwAvAD9xFwAAAAv/////AwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public Structure_A_TypeState Scalar_Structure_A
        {
            get
            {
                return m_scalar_Structure_A;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Structure_A, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Structure_A = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_Structure_A != null)
            {
                children.Add(m_scalar_Structure_A);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_Structure_A:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Structure_A == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Structure_A = new Structure_A_TypeState(this);
                            }
                            else
                            {
                                Scalar_Structure_A = (Structure_A_TypeState)replacement;
                            }
                        }
                    }

                    instance = Scalar_Structure_A;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private Structure_A_TypeState m_scalar_Structure_A;
        #endregion
    }

    #region Structure_G_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_G_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_G_TypeValue(Structure_G_TypeState variable, Structure_G value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_G();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_G_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_G Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_G_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_Structure_A;
                instance.OnReadValue = OnRead_Scalar_Structure_A;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Structure_A;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_G)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_Structure_A Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Structure_A(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Structure_A;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Structure_A(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Structure_A = (Structure_A)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_G m_value;
        private Structure_G_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_H_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_H_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_H_TypeState : BaseDataVariableState<Structure_H>
    {
        #region Constructors
        /// <remarks />
        public Structure_H_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_H_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_H, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0hfVHlwZUluc3RhbmNlAQHYBwEB2AfYBwAAAQHvA/////8BAf////8BAAAA" +
           "FWDJCgIAAAAPAAAAU2NhbGFyX0R1cmF0aW9uAQAPAAAAU2NhbGFyIER1cmF0aW9uAQGGFwAvAD+GFwAA" +
           "AQAiAf////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<double> Scalar_Duration
        {
            get
            {
                return m_scalar_Duration;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Duration, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Duration = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_Duration != null)
            {
                children.Add(m_scalar_Duration);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_Duration:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Duration == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Duration = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Scalar_Duration = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Scalar_Duration;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<double> m_scalar_Duration;
        #endregion
    }

    #region Structure_H_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_H_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_H_TypeValue(Structure_H_TypeState variable, Structure_H value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_H();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_H_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_H Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_H_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_Duration;
                instance.OnReadValue = OnRead_Scalar_Duration;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Duration;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_H)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_Duration Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Duration(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Duration;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Duration(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Duration = (double)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_H m_value;
        private Structure_H_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_J_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_J_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_J_TypeState : BaseDataVariableState<Structure_J>
    {
        #region Constructors
        /// <remarks />
        public Structure_J_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_J_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_J, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0pfVHlwZUluc3RhbmNlAQHZBwEB2QfZBwAAAQHwA/////8BAf////8BAAAA" +
           "FWDJCgIAAAAaAAAAU2NhbGFyX01lc3NhZ2VTZWN1cml0eU1vZGUBABoAAABTY2FsYXIgTWVzc2FnZVNl" +
           "Y3VyaXR5TW9kZQEBhxcALwA/hxcAAAEALgH/////AwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<MessageSecurityMode> Scalar_MessageSecurityMode
        {
            get
            {
                return m_scalar_MessageSecurityMode;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_MessageSecurityMode, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_MessageSecurityMode = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_MessageSecurityMode != null)
            {
                children.Add(m_scalar_MessageSecurityMode);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_MessageSecurityMode:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_MessageSecurityMode == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_MessageSecurityMode = new BaseDataVariableState<MessageSecurityMode>(this);
                            }
                            else
                            {
                                Scalar_MessageSecurityMode = (BaseDataVariableState<MessageSecurityMode>)replacement;
                            }
                        }
                    }

                    instance = Scalar_MessageSecurityMode;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<MessageSecurityMode> m_scalar_MessageSecurityMode;
        #endregion
    }

    #region Structure_J_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_J_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_J_TypeValue(Structure_J_TypeState variable, Structure_J value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_J();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_J_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_J Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_J_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_MessageSecurityMode;
                instance.OnReadValue = OnRead_Scalar_MessageSecurityMode;
                instance.OnSimpleWriteValue = OnWrite_Scalar_MessageSecurityMode;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_J)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_MessageSecurityMode Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_MessageSecurityMode(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_MessageSecurityMode;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_MessageSecurityMode(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_MessageSecurityMode = (MessageSecurityMode)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_J m_value;
        private Structure_J_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_K_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_K_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_K_TypeState : BaseDataVariableState<Structure_K>
    {
        #region Constructors
        /// <remarks />
        public Structure_K_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_K_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_K, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0tfVHlwZUluc3RhbmNlAQHaBwEB2gfaBwAAAQHxA/////8BAf////8BAAAA" +
           "F2DJCgIAAAAiAAAAUzFEX0FycmF5X1N0cmluZ193aXRoaW5fbWF4X2xlbmd0aAEAIQAAADFEIEFycmF5" +
           "IFN0cmluZyB3aXRoaW4gbWF4IGxlbmd0aAEBiBcALwA/iBcAAAAMAQAAAAEAAAAAAAAAAwP/////AQAA" +
           "ABVgyQoCAAAAJgAAAFMxRF9BcnJheV9TdHJpbmdfd2l0aGluX21heF9sZW5ndGhfbm9fAQAlAAAAMUQg" +
           "QXJyYXkgU3RyaW5nIHdpdGhpbiBtYXggbGVuZ3RoPG5vPgEBiRcALwA/iRcAAAAM/////wMD/////wAA" +
           "AAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<string[]> S1D_Array_String_within_max_length
        {
            get
            {
                return m_s1D_Array_String_within_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_String_within_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_String_within_max_length = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s1D_Array_String_within_max_length != null)
            {
                children.Add(m_s1D_Array_String_within_max_length);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S1D_Array_String_within_max_length:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_String_within_max_length == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_String_within_max_length = new BaseDataVariableState<string[]>(this);
                            }
                            else
                            {
                                S1D_Array_String_within_max_length = (BaseDataVariableState<string[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_String_within_max_length;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<string[]> m_s1D_Array_String_within_max_length;
        #endregion
    }

    #region Structure_K_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_K_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_K_TypeValue(Structure_K_TypeState variable, Structure_K value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_K();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_K_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_K Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_K_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S1D_Array_String_within_max_length;
                instance.OnReadValue = OnRead_S1D_Array_String_within_max_length;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_String_within_max_length;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_K)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S1D_Array_String_within_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_String_within_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_String_within_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_String_within_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_String_within_max_length = (StringCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_K m_value;
        private Structure_K_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_L_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_L_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_L_TypeState : BaseDataVariableState<Structure_L>
    {
        #region Constructors
        /// <remarks />
        public Structure_L_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_L_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_L, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX0xfVHlwZUluc3RhbmNlAQHbBwEB2wfbBwAAAQHyA/////8BAf////8BAAAA" +
           "F2DJCgIAAAAZAAAAUzFEX0FycmF5X29mX0Jhc2VEYXRhVHlwZQEAGAAAADFEIEFycmF5IG9mIEJhc2VE" +
           "YXRhVHlwZQEBihcALwA/ihcAAAAYAQAAAAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAHQAAAFMxRF9B" +
           "cnJheV9vZl9CYXNlRGF0YVR5cGVfbm9fAQAcAAAAMUQgQXJyYXkgb2YgQmFzZURhdGFUeXBlPG5vPgEB" +
           "ixcALwA/ixcAAAAY/////wMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<object[]> S1D_Array_of_BaseDataType
        {
            get
            {
                return m_s1D_Array_of_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_BaseDataType = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s1D_Array_of_BaseDataType != null)
            {
                children.Add(m_s1D_Array_of_BaseDataType);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S1D_Array_of_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_BaseDataType = new BaseDataVariableState<object[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_BaseDataType = (BaseDataVariableState<object[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_BaseDataType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<object[]> m_s1D_Array_of_BaseDataType;
        #endregion
    }

    #region Structure_L_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_L_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_L_TypeValue(Structure_L_TypeState variable, Structure_L value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_L();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_L_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_L Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_L_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S1D_Array_of_BaseDataType;
                instance.OnReadValue = OnRead_S1D_Array_of_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_BaseDataType;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_L)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S1D_Array_of_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_BaseDataType = (VariantCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_L m_value;
        private Structure_L_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_M_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_M_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_M_TypeState : BaseDataVariableState<Structure_M>
    {
        #region Constructors
        /// <remarks />
        public Structure_M_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_M_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_M, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX01fVHlwZUluc3RhbmNlAQHcBwEB3AfcBwAAAQHzA/////8BAf////8BAAAA" +
           "F2DJCgIAAAAgAAAAUzFEX0FycmF5X29mX1N0cnVjdHVyZV9fdW5rbm93bl8BAB8AAAAxRCBBcnJheSBv" +
           "ZiBTdHJ1Y3R1cmUgKHVua25vd24pAQGMFwAvAD+MFwAAABYBAAAAAQAAAAAAAAADA/////8BAAAAFWDJ" +
           "CgIAAAAkAAAAUzFEX0FycmF5X29mX1N0cnVjdHVyZV9fdW5rbm93bl9fbm9fAQAjAAAAMUQgQXJyYXkg" +
           "b2YgU3RydWN0dXJlICh1bmtub3duKTxubz4BAY0XAC8AP40XAAAAFv////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<ExtensionObject[]> S1D_Array_of_Structure__unknown_
        {
            get
            {
                return m_s1D_Array_of_Structure__unknown_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure__unknown_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure__unknown_ = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s1D_Array_of_Structure__unknown_ != null)
            {
                children.Add(m_s1D_Array_of_Structure__unknown_);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure__unknown_:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure__unknown_ == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure__unknown_ = new BaseDataVariableState<ExtensionObject[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure__unknown_ = (BaseDataVariableState<ExtensionObject[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure__unknown_;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<ExtensionObject[]> m_s1D_Array_of_Structure__unknown_;
        #endregion
    }

    #region Structure_M_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_M_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_M_TypeValue(Structure_M_TypeState variable, Structure_M value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_M();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_M_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_M Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_M_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S1D_Array_of_Structure__unknown_;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure__unknown_;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure__unknown_;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_M)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S1D_Array_of_Structure__unknown_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure__unknown_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure__unknown_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure__unknown_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure__unknown_ = (ExtensionObjectCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_M m_value;
        private Structure_M_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_N_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_N_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_N_TypeState : BaseDataVariableState<Structure_N>
    {
        #region Constructors
        /// <remarks />
        public Structure_N_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_N_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_N, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX05fVHlwZUluc3RhbmNlAQHdBwEB3QfdBwAAAQH0A/////8BAf////8BAAAA" +
           "F2DJCgIAAAAeAAAAUzFEX0FycmF5X29mX1N0cnVjdHVyZV9fa25vd25fAQAdAAAAMUQgQXJyYXkgb2Yg" +
           "U3RydWN0dXJlIChrbm93bikBAY4XAC8AP44XAAAAFgEAAAABAAAAAAAAAAMD/////wEAAAAVYMkKAgAA" +
           "ACIAAABTMURfQXJyYXlfb2ZfU3RydWN0dXJlX19rbm93bl9fbm9fAQAhAAAAMUQgQXJyYXkgb2YgU3Ry" +
           "dWN0dXJlIChrbm93bik8bm8+AQGPFwAvAD+PFwAAABb/////AwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<ExtensionObject[]> S1D_Array_of_Structure__known_
        {
            get
            {
                return m_s1D_Array_of_Structure__known_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure__known_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure__known_ = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s1D_Array_of_Structure__known_ != null)
            {
                children.Add(m_s1D_Array_of_Structure__known_);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure__known_:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure__known_ == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure__known_ = new BaseDataVariableState<ExtensionObject[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure__known_ = (BaseDataVariableState<ExtensionObject[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure__known_;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<ExtensionObject[]> m_s1D_Array_of_Structure__known_;
        #endregion
    }

    #region Structure_N_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_N_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_N_TypeValue(Structure_N_TypeState variable, Structure_N value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_N();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_N_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_N Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_N_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S1D_Array_of_Structure__known_;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure__known_;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure__known_;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_N)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S1D_Array_of_Structure__known_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure__known_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure__known_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure__known_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure__known_ = (ExtensionObjectCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_N m_value;
        private Structure_N_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_O_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_O_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_O_TypeState : BaseDataVariableState<Structure_O>
    {
        #region Constructors
        /// <remarks />
        public Structure_O_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_O_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_O, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX09fVHlwZUluc3RhbmNlAQHeBwEB3gfeBwAAAQH1A/////8BAf////8BAAAA" +
           "F2DJCgIAAAAjAAAAUzFEX0FycmF5X29mX0FwcGxpY2F0aW9uRGVzY3JpcHRpb24BACIAAAAxRCBBcnJh" +
           "eSBvZiBBcHBsaWNhdGlvbkRlc2NyaXB0aW9uAQGQFwAvAD+QFwAAAQA0AQEAAAABAAAAAAAAAAMD////" +
           "/wEAAAAVYMkKAgAAACcAAABTMURfQXJyYXlfb2ZfQXBwbGljYXRpb25EZXNjcmlwdGlvbl9ub18BACYA" +
           "AAAxRCBBcnJheSBvZiBBcHBsaWNhdGlvbkRlc2NyaXB0aW9uPG5vPgEBkRcALwA/kRcAAAEANAH/////" +
           "AwP/////BwAAABVgiQoCAAAAAQAOAAAAQXBwbGljYXRpb25VcmkBAZIXAC8AP5IXAAAADP////8DA///" +
           "//8AAAAAFWCJCgIAAAABAAoAAABQcm9kdWN0VXJpAQGTFwAvAD+TFwAAAAz/////AwP/////AAAAABVg" +
           "iQoCAAAAAQAPAAAAQXBwbGljYXRpb25OYW1lAQGUFwAvAD+UFwAAABX/////AwP/////AAAAABVgiQoC" +
           "AAAAAQAPAAAAQXBwbGljYXRpb25UeXBlAQGVFwAvAD+VFwAAAQAzAf////8DA/////8AAAAAFWCJCgIA" +
           "AAABABAAAABHYXRld2F5U2VydmVyVXJpAQGWFwAvAD+WFwAAAAz/////AwP/////AAAAABVgiQoCAAAA" +
           "AQATAAAARGlzY292ZXJ5UHJvZmlsZVVyaQEBlxcALwA/lxcAAAAM/////wMD/////wAAAAAXYIkKAgAA" +
           "AAEADQAAAERpc2NvdmVyeVVybHMBAZgXAC8AP5gXAAAADAEAAAABAAAAAAAAAAMD/////wEAAAAVYMkK" +
           "AgAAABEAAABEaXNjb3ZlcnlVcmxzX25vXwEAEQAAAERpc2NvdmVyeVVybHM8bm8+AQGZFwAvAD+ZFwAA" +
           "AAz/////AwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState<ApplicationDescription[]> S1D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s1D_Array_of_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_ApplicationDescription = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s1D_Array_of_ApplicationDescription != null)
            {
                children.Add(m_s1D_Array_of_ApplicationDescription);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S1D_Array_of_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_ApplicationDescription = new BaseDataVariableState<ApplicationDescription[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_ApplicationDescription = (BaseDataVariableState<ApplicationDescription[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_ApplicationDescription;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<ApplicationDescription[]> m_s1D_Array_of_ApplicationDescription;
        #endregion
    }

    #region Structure_O_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_O_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_O_TypeValue(Structure_O_TypeState variable, Structure_O value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_O();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_O_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_O Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_O_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S1D_Array_of_ApplicationDescription;
                instance.OnReadValue = OnRead_S1D_Array_of_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_ApplicationDescription;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_O)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S1D_Array_of_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_ApplicationDescription = (ApplicationDescriptionCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_O m_value;
        private Structure_O_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_P_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_P_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_P_TypeState : BaseDataVariableState<Structure_P>
    {
        #region Constructors
        /// <remarks />
        public Structure_P_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_P_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_P, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX1BfVHlwZUluc3RhbmNlAQHfBwEB3wffBwAAAQH2A/////8BAf////8BAAAA" +
           "FWDJCgIAAAAiAAAAUzJEX0FycmF5X1N0cmluZ193aXRoaW5fbWF4X2xlbmd0aAEAIQAAADJEIEFycmF5" +
           "IFN0cmluZyB3aXRoaW4gbWF4IGxlbmd0aAEBmhcALwA/mhcAAAAMAAAAAAMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState S2D_Array_String_within_max_length
        {
            get
            {
                return m_s2D_Array_String_within_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_String_within_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_String_within_max_length = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s2D_Array_String_within_max_length != null)
            {
                children.Add(m_s2D_Array_String_within_max_length);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S2D_Array_String_within_max_length:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_String_within_max_length == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_String_within_max_length = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_String_within_max_length = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_String_within_max_length;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_s2D_Array_String_within_max_length;
        #endregion
    }

    #region Structure_P_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_P_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_P_TypeValue(Structure_P_TypeState variable, Structure_P value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_P();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_P_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_P Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_P_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S2D_Array_String_within_max_length;
                instance.OnReadValue = OnRead_S2D_Array_String_within_max_length;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_String_within_max_length;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_P)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S2D_Array_String_within_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_String_within_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_String_within_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_String_within_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_String_within_max_length = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_P m_value;
        private Structure_P_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_Q_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_Q_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_Q_TypeState : BaseDataVariableState<Structure_Q>
    {
        #region Constructors
        /// <remarks />
        public Structure_Q_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_Q_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_Q, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX1FfVHlwZUluc3RhbmNlAQHgBwEB4AfgBwAAAQH3A/////8BAf////8BAAAA" +
           "FWDJCgIAAAAZAAAAUzJEX0FycmF5X29mX0Jhc2VEYXRhVHlwZQEAGAAAADJEIEFycmF5IG9mIEJhc2VE" +
           "YXRhVHlwZQEBmxcALwA/mxcAAAAYAAAAAAMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState S2D_Array_of_BaseDataType
        {
            get
            {
                return m_s2D_Array_of_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_BaseDataType = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s2D_Array_of_BaseDataType != null)
            {
                children.Add(m_s2D_Array_of_BaseDataType);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S2D_Array_of_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_BaseDataType = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_BaseDataType = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_BaseDataType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_s2D_Array_of_BaseDataType;
        #endregion
    }

    #region Structure_Q_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_Q_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_Q_TypeValue(Structure_Q_TypeState variable, Structure_Q value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_Q();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_Q_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_Q Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_Q_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S2D_Array_of_BaseDataType;
                instance.OnReadValue = OnRead_S2D_Array_of_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_BaseDataType;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_Q)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S2D_Array_of_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_BaseDataType = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_Q m_value;
        private Structure_Q_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_R__TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_R__TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_R__TypeState : BaseDataVariableState<Structure_R>
    {
        #region Constructors
        /// <remarks />
        public Structure_R__TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_R__Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_R, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAZAAAAU3RydWN0dXJlX1JfX1R5cGVJbnN0YW5jZQEB4QcBAeEH4QcAAAEB+AP/////AQH/////AQAA" +
           "ABVgyQoCAAAAIAAAAFMyRF9BcnJheV9vZl9TdHJ1Y3R1cmVfX3Vua25vd25fAQAfAAAAMkQgQXJyYXkg" +
           "b2YgU3RydWN0dXJlICh1bmtub3duKQEBnBcALwA/nBcAAAAWAAAAAAMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState S2D_Array_of_Structure__unknown_
        {
            get
            {
                return m_s2D_Array_of_Structure__unknown_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Structure__unknown_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Structure__unknown_ = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s2D_Array_of_Structure__unknown_ != null)
            {
                children.Add(m_s2D_Array_of_Structure__unknown_);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S2D_Array_of_Structure__unknown_:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Structure__unknown_ == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Structure__unknown_ = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_Structure__unknown_ = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Structure__unknown_;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_s2D_Array_of_Structure__unknown_;
        #endregion
    }

    #region Structure_R__TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_R__TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_R__TypeValue(Structure_R__TypeState variable, Structure_R value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_R();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_R__TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_R Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_R__TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S2D_Array_of_Structure__unknown_;
                instance.OnReadValue = OnRead_S2D_Array_of_Structure__unknown_;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Structure__unknown_;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_R)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S2D_Array_of_Structure__unknown_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Structure__unknown_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Structure__unknown_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Structure__unknown_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Structure__unknown_ = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_R m_value;
        private Structure_R__TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_S_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_S_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_S_TypeState : BaseDataVariableState<Structure_S>
    {
        #region Constructors
        /// <remarks />
        public Structure_S_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_S_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_S, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX1NfVHlwZUluc3RhbmNlAQHiBwEB4gfiBwAAAQH5A/////8BAf////8BAAAA" +
           "FWDJCgIAAAAeAAAAUzJEX0FycmF5X29mX1N0cnVjdHVyZV9fa25vd25fAQAdAAAAMkQgQXJyYXkgb2Yg" +
           "U3RydWN0dXJlIChrbm93bikBAZ0XAC8AP50XAAAAFgAAAAADA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState S2D_Array_of_Structure__known_
        {
            get
            {
                return m_s2D_Array_of_Structure__known_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Structure__known_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Structure__known_ = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s2D_Array_of_Structure__known_ != null)
            {
                children.Add(m_s2D_Array_of_Structure__known_);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S2D_Array_of_Structure__known_:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Structure__known_ == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Structure__known_ = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_Structure__known_ = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Structure__known_;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_s2D_Array_of_Structure__known_;
        #endregion
    }

    #region Structure_S_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_S_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_S_TypeValue(Structure_S_TypeState variable, Structure_S value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_S();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_S_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_S Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_S_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S2D_Array_of_Structure__known_;
                instance.OnReadValue = OnRead_S2D_Array_of_Structure__known_;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Structure__known_;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_S)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S2D_Array_of_Structure__known_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Structure__known_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Structure__known_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Structure__known_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Structure__known_ = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_S m_value;
        private Structure_S_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region Structure_T_TypeState Class
    #if (!OPCUA_EXCLUDE_Structure_T_TypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class Structure_T_TypeState : BaseDataVariableState<Structure_T>
    {
        #region Constructors
        /// <remarks />
        public Structure_T_TypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.Structure_T_Type, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.Structure_T, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAYAAAAU3RydWN0dXJlX1RfVHlwZUluc3RhbmNlAQHjBwEB4wfjBwAAAQH6A/////8BAf////8BAAAA" +
           "FWDJCgIAAAAjAAAAUzJEX0FycmF5X29mX0FwcGxpY2F0aW9uRGVzY3JpcHRpb24BACIAAAAyRCBBcnJh" +
           "eSBvZiBBcHBsaWNhdGlvbkRlc2NyaXB0aW9uAQGeFwAvAD+eFwAAAQA0AQAAAAADA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState S2D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s2D_Array_of_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_ApplicationDescription = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_s2D_Array_of_ApplicationDescription != null)
            {
                children.Add(m_s2D_Array_of_ApplicationDescription);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.S2D_Array_of_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_ApplicationDescription = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_ApplicationDescription = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_ApplicationDescription;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_s2D_Array_of_ApplicationDescription;
        #endregion
    }

    #region Structure_T_TypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class Structure_T_TypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public Structure_T_TypeValue(Structure_T_TypeState variable, Structure_T value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new Structure_T();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public Structure_T_TypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public Structure_T Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(Structure_T_TypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.S2D_Array_of_ApplicationDescription;
                instance.OnReadValue = OnRead_S2D_Array_of_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_ApplicationDescription;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (Structure_T)Write(value);
            }

            return ServiceResult.Good;
        }

        #region S2D_Array_of_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_ApplicationDescription = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private Structure_T m_value;
        private Structure_T_TypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region LargeComplexStructureTypeState Class
    #if (!OPCUA_EXCLUDE_LargeComplexStructureTypeState)
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class LargeComplexStructureTypeState : BaseDataVariableState<LargeComplexStructure>
    {
        #region Constructors
        /// <remarks />
        public LargeComplexStructureTypeState(NodeState parent) : base(parent)
        {
        }

        /// <remarks />
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.VariableTypes.LargeComplexStructureType, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(StructuresWithArrays.DataTypes.LargeComplexStructure, StructuresWithArrays.Namespaces.StructuresWithArrays, namespaceUris);
        }

        /// <remarks />
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <remarks />
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <remarks />
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <remarks />
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAACgAAABodHRwOi8vdGVzdC5vcmcvVUEvU3RydWN0dXJlc1dpdGhBcnJheXMv/////xVgiQICAAAA" +
           "AQAhAAAATGFyZ2VDb21wbGV4U3RydWN0dXJlVHlwZUluc3RhbmNlAQHkBwEB5AfkBwAAAQHQB/////8B" +
           "Af////8dAAAAFWDJCgIAAAATAAAAU2NhbGFyX0Jhc2VEYXRhVHlwZQEAEwAAAFNjYWxhciBCYXNlRGF0" +
           "YVR5cGUBAZ8XAC8AP58XAAAAGP////8DA/////8AAAAAFWDJCgIAAAAQAAAAU2NhbGFyX1N0cnVjdHVy" +
           "ZQEAEAAAAFNjYWxhciBTdHJ1Y3R1cmUBAaAXAC8AP6AXAAAAFv////8DA/////8AAAAAFWDJCgIAAAAd" +
           "AAAAU2NhbGFyX0FwcGxpY2F0aW9uRGVzY3JpcHRpb24BAB0AAABTY2FsYXIgQXBwbGljYXRpb25EZXNj" +
           "cmlwdGlvbgEBoRcALwA/oRcAAAEANAH/////AwP/////BwAAABVgiQoCAAAAAQAOAAAAQXBwbGljYXRp" +
           "b25VcmkBAbwXAC8AP7wXAAAADP////8DA/////8AAAAAFWCJCgIAAAABAAoAAABQcm9kdWN0VXJpAQG9" +
           "FwAvAD+9FwAAAAz/////AwP/////AAAAABVgiQoCAAAAAQAPAAAAQXBwbGljYXRpb25OYW1lAQG+FwAv" +
           "AD++FwAAABX/////AwP/////AAAAABVgiQoCAAAAAQAPAAAAQXBwbGljYXRpb25UeXBlAQG/FwAvAD+/" +
           "FwAAAQAzAf////8DA/////8AAAAAFWCJCgIAAAABABAAAABHYXRld2F5U2VydmVyVXJpAQHAFwAvAD/A" +
           "FwAAAAz/////AwP/////AAAAABVgiQoCAAAAAQATAAAARGlzY292ZXJ5UHJvZmlsZVVyaQEBwRcALwA/" +
           "wRcAAAAM/////wMD/////wAAAAAXYIkKAgAAAAEADQAAAERpc2NvdmVyeVVybHMBAcIXAC8AP8IXAAAA" +
           "DAEAAAABAAAAAAAAAAMD/////wEAAAAVYMkKAgAAABEAAABEaXNjb3ZlcnlVcmxzX25vXwEAEQAAAERp" +
           "c2NvdmVyeVVybHM8bm8+AQHQFwAvAD/QFwAAAAz/////AwP/////AAAAABVgyQoCAAAAEgAAAFNjYWxh" +
           "cl9TdHJ1Y3R1cmVfQQEAEgAAAFNjYWxhciBTdHJ1Y3R1cmUgQQEBohcALwEB0QeiFwAAAQHoA/////8D" +
           "A/////8CAAAAFWDJCgIAAAAOAAAARHVyYXRpb25fRmllbGQBAA4AAABEdXJhdGlvbiBGaWVsZAEBcBcA" +
           "LwA/cBcAAAEAIgH/////AwP/////AAAAABVgyQoCAAAADAAAAERvdWJsZV9GaWVsZAEADAAAAERvdWJs" +
           "ZSBGaWVsZAEBcRcALwA/cRcAAAAL/////wMD/////wAAAAAVYMkKAgAAABIAAABTY2FsYXJfU3RydWN0" +
           "dXJlX0IBABIAAABTY2FsYXIgU3RydWN0dXJlIEIBAaMXAC8BAdIHoxcAAAEB6QP/////AwP/////AQAA" +
           "ABVgyQoCAAAACwAAAEludDMyX0ZpZWxkAQALAAAASW50MzIgRmllbGQBAXMXAC8AP3MXAAAABv////8D" +
           "A/////8AAAAAFWDJCgIAAAAOAAAAU2NhbGFyX0Jvb2xlYW4BAA4AAABTY2FsYXIgQm9vbGVhbgEBpBcA" +
           "LwA/pBcAAAAB/////wMD/////wAAAAAVYMkKAgAAAA8AAABTY2FsYXJfRHVyYXRpb24BAA8AAABTY2Fs" +
           "YXIgRHVyYXRpb24BAaUXAC8AP6UXAAABACIB/////wMD/////wAAAAAVYMkKAgAAABkAAABTY2FsYXJf" +
           "Um9sZVBlcm1pc3Npb25UeXBlAQAZAAAAU2NhbGFyIFJvbGVQZXJtaXNzaW9uVHlwZQEBphcALwA/phcA" +
           "AABg/////wMD/////wIAAAAVYIkKAgAAAAEABgAAAFJvbGVJZAEBwxcALwA/wxcAAAAR/////wMD////" +
           "/wAAAAAVYIkKAgAAAAEACwAAAFBlcm1pc3Npb25zAQHEFwAvAD/EFwAAAF7/////AwP/////AAAAABVg" +
           "yQoCAAAAGwAAAFNjYWxhcl9TdHJpbmdfbm9fbWF4X2xlbmd0aAEAGwAAAFNjYWxhciBTdHJpbmcgbm8g" +
           "bWF4IGxlbmd0aAEBpxcALwA/pxcAAAAY/////wMD/////wAAAAAVYMkKAgAAAB8AAABTY2FsYXJfU3Ry" +
           "aW5nX3dpdGhpbl9tYXhfbGVuZ3RoAQAfAAAAU2NhbGFyIFN0cmluZyB3aXRoaW4gbWF4IGxlbmd0aAEB" +
           "qBcALwA/qBcAAAAY/////wMD/////wAAAAAXYMkKAgAAAB4AAABTMURfQXJyYXlfU3RyaW5nX25vX21h" +
           "eF9sZW5ndGgBAB0AAAAxRCBBcnJheSBTdHJpbmcgbm8gbWF4IGxlbmd0aAEBqRcALwA/qRcAAAAMAQAA" +
           "AAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAIgAAAFMxRF9BcnJheV9TdHJpbmdfbm9fbWF4X2xlbmd0" +
           "aF9ub18BACEAAAAxRCBBcnJheSBTdHJpbmcgbm8gbWF4IGxlbmd0aDxubz4BAcUXAC8AP8UXAAAADP//" +
           "//8DA/////8AAAAAF2DJCgIAAAAiAAAAUzFEX0FycmF5X1N0cmluZ193aXRoaW5fbWF4X2xlbmd0aAEA" +
           "IQAAADFEIEFycmF5IFN0cmluZyB3aXRoaW4gbWF4IGxlbmd0aAEBqhcALwA/qhcAAAAMAQAAAAEAAAAA" +
           "AAAAAwP/////AQAAABVgyQoCAAAAJgAAAFMxRF9BcnJheV9TdHJpbmdfd2l0aGluX21heF9sZW5ndGhf" +
           "bm9fAQAlAAAAMUQgQXJyYXkgU3RyaW5nIHdpdGhpbiBtYXggbGVuZ3RoPG5vPgEBxhcALwA/xhcAAAAM" +
           "/////wMD/////wAAAAAXYMkKAgAAABkAAABTMURfQXJyYXlfb2ZfQmFzZURhdGFUeXBlAQAYAAAAMUQg" +
           "QXJyYXkgb2YgQmFzZURhdGFUeXBlAQGrFwAvAD+rFwAAABgBAAAAAQAAAAAAAAADA/////8BAAAAFWDJ" +
           "CgIAAAAdAAAAUzFEX0FycmF5X29mX0Jhc2VEYXRhVHlwZV9ub18BABwAAAAxRCBBcnJheSBvZiBCYXNl" +
           "RGF0YVR5cGU8bm8+AQHHFwAvAD/HFwAAABj/////AwP/////AAAAABdgyQoCAAAAIAAAAFMxRF9BcnJh" +
           "eV9vZl9TdHJ1Y3R1cmVfX3Vua25vd25fAQAfAAAAMUQgQXJyYXkgb2YgU3RydWN0dXJlICh1bmtub3du" +
           "KQEBrBcALwA/rBcAAAAWAQAAAAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAJAAAAFMxRF9BcnJheV9v" +
           "Zl9TdHJ1Y3R1cmVfX3Vua25vd25fX25vXwEAIwAAADFEIEFycmF5IG9mIFN0cnVjdHVyZSAodW5rbm93" +
           "bik8bm8+AQHIFwAvAD/IFwAAABb/////AwP/////AAAAABdgyQoCAAAAHgAAAFMxRF9BcnJheV9vZl9T" +
           "dHJ1Y3R1cmVfX2tub3duXwEAHQAAADFEIEFycmF5IG9mIFN0cnVjdHVyZSAoa25vd24pAQGtFwAvAD+t" +
           "FwAAABYBAAAAAQAAAAAAAAADA/////8BAAAAFWDJCgIAAAAiAAAAUzFEX0FycmF5X29mX1N0cnVjdHVy" +
           "ZV9fa25vd25fX25vXwEAIQAAADFEIEFycmF5IG9mIFN0cnVjdHVyZSAoa25vd24pPG5vPgEByRcALwA/" +
           "yRcAAAAW/////wMD/////wAAAAAXYMkKAgAAACMAAABTMURfQXJyYXlfb2ZfQXBwbGljYXRpb25EZXNj" +
           "cmlwdGlvbgEAIgAAADFEIEFycmF5IG9mIEFwcGxpY2F0aW9uRGVzY3JpcHRpb24BAa4XAC8AP64XAAAB" +
           "ADQBAQAAAAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAJwAAAFMxRF9BcnJheV9vZl9BcHBsaWNhdGlv" +
           "bkRlc2NyaXB0aW9uX25vXwEAJgAAADFEIEFycmF5IG9mIEFwcGxpY2F0aW9uRGVzY3JpcHRpb248bm8+" +
           "AQHKFwAvAD/KFwAAAQA0Af////8DA/////8HAAAAFWCJCgIAAAABAA4AAABBcHBsaWNhdGlvblVyaQEB" +
           "0RcALwA/0RcAAAAM/////wMD/////wAAAAAVYIkKAgAAAAEACgAAAFByb2R1Y3RVcmkBAdIXAC8AP9IX" +
           "AAAADP////8DA/////8AAAAAFWCJCgIAAAABAA8AAABBcHBsaWNhdGlvbk5hbWUBAdMXAC8AP9MXAAAA" +
           "Ff////8DA/////8AAAAAFWCJCgIAAAABAA8AAABBcHBsaWNhdGlvblR5cGUBAdQXAC8AP9QXAAABADMB" +
           "/////wMD/////wAAAAAVYIkKAgAAAAEAEAAAAEdhdGV3YXlTZXJ2ZXJVcmkBAdUXAC8AP9UXAAAADP//" +
           "//8DA/////8AAAAAFWCJCgIAAAABABMAAABEaXNjb3ZlcnlQcm9maWxlVXJpAQHWFwAvAD/WFwAAAAz/" +
           "////AwP/////AAAAABdgiQoCAAAAAQANAAAARGlzY292ZXJ5VXJscwEB1xcALwA/1xcAAAAMAQAAAAEA" +
           "AAAAAAAAAwP/////AQAAABVgyQoCAAAAEQAAAERpc2NvdmVyeVVybHNfbm9fAQARAAAARGlzY292ZXJ5" +
           "VXJsczxubz4BAd4XAC8AP94XAAAADP////8DA/////8AAAAAF2DJCgIAAAAYAAAAUzFEX0FycmF5X29m" +
           "X1N0cnVjdHVyZV9BAQAXAAAAMUQgQXJyYXkgb2YgU3RydWN0dXJlIEEBAa8XAC8AP68XAAABAegDAQAA" +
           "AAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAHAAAAFMxRF9BcnJheV9vZl9TdHJ1Y3R1cmVfQV9ub18B" +
           "ABsAAAAxRCBBcnJheSBvZiBTdHJ1Y3R1cmUgQTxubz4BAcsXAC8BAdEHyxcAAAEB6AP/////AwP/////" +
           "AgAAABVgyQoCAAAADgAAAER1cmF0aW9uX0ZpZWxkAQAOAAAARHVyYXRpb24gRmllbGQBAdgXAC8AP9gX" +
           "AAABACIB/////wMD/////wAAAAAVYMkKAgAAAAwAAABEb3VibGVfRmllbGQBAAwAAABEb3VibGUgRmll" +
           "bGQBAdkXAC8AP9kXAAAAC/////8DA/////8AAAAAF2DJCgIAAAAYAAAAUzFEX0FycmF5X29mX1N0cnVj" +
           "dHVyZV9CAQAXAAAAMUQgQXJyYXkgb2YgU3RydWN0dXJlIEIBAbAXAC8AP7AXAAABAekDAQAAAAEAAAAA" +
           "AAAAAwP/////AQAAABVgyQoCAAAAHAAAAFMxRF9BcnJheV9vZl9TdHJ1Y3R1cmVfQl9ub18BABsAAAAx" +
           "RCBBcnJheSBvZiBTdHJ1Y3R1cmUgQjxubz4BAcwXAC8BAdIHzBcAAAEB6QP/////AwP/////AgAAABVg" +
           "yQoCAAAADgAAAER1cmF0aW9uX0ZpZWxkAQAOAAAARHVyYXRpb24gRmllbGQBAdoXAC8AP9oXAAABACIB" +
           "/////wMD/////wAAAAAVYMkKAgAAAAsAAABJbnQzMl9GaWVsZAEACwAAAEludDMyIEZpZWxkAQHbFwAv" +
           "AD/bFwAAAAb/////AwP/////AAAAABdgyQoCAAAAFAAAAFMxRF9BcnJheV9vZl9Cb29sZWFuAQATAAAA" +
           "MUQgQXJyYXkgb2YgQm9vbGVhbgEBsRcALwA/sRcAAAABAQAAAAEAAAAAAAAAAwP/////AQAAABVgyQoC" +
           "AAAAGAAAAFMxRF9BcnJheV9vZl9Cb29sZWFuX25vXwEAFwAAADFEIEFycmF5IG9mIEJvb2xlYW48bm8+" +
           "AQHNFwAvAD/NFwAAAAH/////AwP/////AAAAABdgyQoCAAAAFQAAAFMxRF9BcnJheV9vZl9EdXJhdGlv" +
           "bgEAFAAAADFEIEFycmF5IG9mIER1cmF0aW9uAQGyFwAvAD+yFwAAAQAiAQEAAAABAAAAAAAAAAMD////" +
           "/wEAAAAVYMkKAgAAABkAAABTMURfQXJyYXlfb2ZfRHVyYXRpb25fbm9fAQAYAAAAMUQgQXJyYXkgb2Yg" +
           "RHVyYXRpb248bm8+AQHOFwAvAD/OFwAAAQAiAf////8DA/////8AAAAAF2DJCgIAAAAfAAAAUzFEX0Fy" +
           "cmF5X29mX1JvbGVQZXJtaXNzaW9uVHlwZQEAHgAAADFEIEFycmF5IG9mIFJvbGVQZXJtaXNzaW9uVHlw" +
           "ZQEBsxcALwA/sxcAAABgAQAAAAEAAAAAAAAAAwP/////AQAAABVgyQoCAAAAIwAAAFMxRF9BcnJheV9v" +
           "Zl9Sb2xlUGVybWlzc2lvblR5cGVfbm9fAQAiAAAAMUQgQXJyYXkgb2YgUm9sZVBlcm1pc3Npb25UeXBl" +
           "PG5vPgEBzxcALwA/zxcAAABg/////wMD/////wIAAAAVYIkKAgAAAAEABgAAAFJvbGVJZAEB3BcALwA/" +
           "3BcAAAAR/////wMD/////wAAAAAVYIkKAgAAAAEACwAAAFBlcm1pc3Npb25zAQHdFwAvAD/dFwAAAF7/" +
           "////AwP/////AAAAABVgyQoCAAAAGQAAAFMyRF9BcnJheV9vZl9CYXNlRGF0YVR5cGUBABgAAAAyRCBB" +
           "cnJheSBvZiBCYXNlRGF0YVR5cGUBAbQXAC8AP7QXAAAAGAAAAAADA/////8AAAAAFWDJCgIAAAAWAAAA" +
           "UzJEX0FycmF5X29mX1N0cnVjdHVyZQEAFQAAADJEIEFycmF5IG9mIFN0cnVjdHVyZQEBtRcALwA/tRcA" +
           "AAAWAAAAAAMD/////wAAAAAVYMkKAgAAACMAAABTMkRfQXJyYXlfb2ZfQXBwbGljYXRpb25EZXNjcmlw" +
           "dGlvbgEAIgAAADJEIEFycmF5IG9mIEFwcGxpY2F0aW9uRGVzY3JpcHRpb24BAbYXAC8AP7YXAAABADQB" +
           "AAAAAAMD/////wAAAAAVYMkKAgAAABgAAABTMkRfQXJyYXlfb2ZfU3RydWN0dXJlX0EBABcAAAAyRCBB" +
           "cnJheSBvZiBTdHJ1Y3R1cmUgQQEBtxcALwEB0Qe3FwAAAQHoAwAAAAADA/////8CAAAAFWDJCgIAAAAO" +
           "AAAARHVyYXRpb25fRmllbGQBAA4AAABEdXJhdGlvbiBGaWVsZAEBcBcALwA/cBcAAAEAIgH/////AwP/" +
           "////AAAAABVgyQoCAAAADAAAAERvdWJsZV9GaWVsZAEADAAAAERvdWJsZSBGaWVsZAEBcRcALwA/cRcA" +
           "AAAL/////wMD/////wAAAAAVYMkKAgAAABgAAABTMkRfQXJyYXlfb2ZfU3RydWN0dXJlX0IBABcAAAAy" +
           "RCBBcnJheSBvZiBTdHJ1Y3R1cmUgQgEBuBcALwEB0ge4FwAAAQHpAwAAAAADA/////8BAAAAFWDJCgIA" +
           "AAALAAAASW50MzJfRmllbGQBAAsAAABJbnQzMiBGaWVsZAEBcxcALwA/cxcAAAAG/////wMD/////wAA" +
           "AAAVYMkKAgAAABQAAABTMkRfQXJyYXlfb2ZfQm9vbGVhbgEAEwAAADJEIEFycmF5IG9mIEJvb2xlYW4B" +
           "AbkXAC8AP7kXAAAAAQAAAAADA/////8AAAAAFWDJCgIAAAAVAAAAUzJEX0FycmF5X29mX0R1cmF0aW9u" +
           "AQAUAAAAMkQgQXJyYXkgb2YgRHVyYXRpb24BAboXAC8AP7oXAAABACIBAAAAAAMD/////wAAAAAVYMkK" +
           "AgAAAB8AAABTMkRfQXJyYXlfb2ZfUm9sZVBlcm1pc3Npb25UeXBlAQAeAAAAMkQgQXJyYXkgb2YgUm9s" +
           "ZVBlcm1pc3Npb25UeXBlAQG7FwAvAD+7FwAAAGAAAAAAAwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public BaseDataVariableState Scalar_BaseDataType
        {
            get
            {
                return m_scalar_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_BaseDataType = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<ExtensionObject> Scalar_Structure
        {
            get
            {
                return m_scalar_Structure;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Structure, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Structure = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<ApplicationDescription> Scalar_ApplicationDescription
        {
            get
            {
                return m_scalar_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_ApplicationDescription = value;
            }
        }

        /// <remarks />
        public Structure_A_TypeState Scalar_Structure_A
        {
            get
            {
                return m_scalar_Structure_A;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Structure_A, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Structure_A = value;
            }
        }

        /// <remarks />
        public Structure_B_TypeState Scalar_Structure_B
        {
            get
            {
                return m_scalar_Structure_B;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Structure_B, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Structure_B = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<bool> Scalar_Boolean
        {
            get
            {
                return m_scalar_Boolean;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Boolean, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Boolean = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<double> Scalar_Duration
        {
            get
            {
                return m_scalar_Duration;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_Duration, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_Duration = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<RolePermissionType> Scalar_RolePermissionType
        {
            get
            {
                return m_scalar_RolePermissionType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_RolePermissionType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_RolePermissionType = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState Scalar_String_no_max_length
        {
            get
            {
                return m_scalar_String_no_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_String_no_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_String_no_max_length = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState Scalar_String_within_max_length
        {
            get
            {
                return m_scalar_String_within_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_scalar_String_within_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_scalar_String_within_max_length = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<string[]> S1D_Array_String_no_max_length
        {
            get
            {
                return m_s1D_Array_String_no_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_String_no_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_String_no_max_length = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<string[]> S1D_Array_String_within_max_length
        {
            get
            {
                return m_s1D_Array_String_within_max_length;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_String_within_max_length, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_String_within_max_length = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<object[]> S1D_Array_of_BaseDataType
        {
            get
            {
                return m_s1D_Array_of_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_BaseDataType = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<ExtensionObject[]> S1D_Array_of_Structure__unknown_
        {
            get
            {
                return m_s1D_Array_of_Structure__unknown_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure__unknown_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure__unknown_ = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<ExtensionObject[]> S1D_Array_of_Structure__known_
        {
            get
            {
                return m_s1D_Array_of_Structure__known_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure__known_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure__known_ = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<ApplicationDescription[]> S1D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s1D_Array_of_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_ApplicationDescription = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<Structure_A[]> S1D_Array_of_Structure_A
        {
            get
            {
                return m_s1D_Array_of_Structure_A;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure_A, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure_A = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<Structure_B[]> S1D_Array_of_Structure_B
        {
            get
            {
                return m_s1D_Array_of_Structure_B;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Structure_B, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Structure_B = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<bool[]> S1D_Array_of_Boolean
        {
            get
            {
                return m_s1D_Array_of_Boolean;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Boolean, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Boolean = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<double[]> S1D_Array_of_Duration
        {
            get
            {
                return m_s1D_Array_of_Duration;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_Duration, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_Duration = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState<RolePermissionType[]> S1D_Array_of_RolePermissionType
        {
            get
            {
                return m_s1D_Array_of_RolePermissionType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s1D_Array_of_RolePermissionType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s1D_Array_of_RolePermissionType = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_BaseDataType
        {
            get
            {
                return m_s2D_Array_of_BaseDataType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_BaseDataType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_BaseDataType = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_Structure
        {
            get
            {
                return m_s2D_Array_of_Structure;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Structure, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Structure = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_ApplicationDescription
        {
            get
            {
                return m_s2D_Array_of_ApplicationDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_ApplicationDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_ApplicationDescription = value;
            }
        }

        /// <remarks />
        public Structure_A_TypeState S2D_Array_of_Structure_A
        {
            get
            {
                return m_s2D_Array_of_Structure_A;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Structure_A, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Structure_A = value;
            }
        }

        /// <remarks />
        public Structure_B_TypeState S2D_Array_of_Structure_B
        {
            get
            {
                return m_s2D_Array_of_Structure_B;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Structure_B, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Structure_B = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_Boolean
        {
            get
            {
                return m_s2D_Array_of_Boolean;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Boolean, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Boolean = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_Duration
        {
            get
            {
                return m_s2D_Array_of_Duration;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_Duration, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_Duration = value;
            }
        }

        /// <remarks />
        public BaseDataVariableState S2D_Array_of_RolePermissionType
        {
            get
            {
                return m_s2D_Array_of_RolePermissionType;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s2D_Array_of_RolePermissionType, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s2D_Array_of_RolePermissionType = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <remarks />
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_scalar_BaseDataType != null)
            {
                children.Add(m_scalar_BaseDataType);
            }

            if (m_scalar_Structure != null)
            {
                children.Add(m_scalar_Structure);
            }

            if (m_scalar_ApplicationDescription != null)
            {
                children.Add(m_scalar_ApplicationDescription);
            }

            if (m_scalar_Structure_A != null)
            {
                children.Add(m_scalar_Structure_A);
            }

            if (m_scalar_Structure_B != null)
            {
                children.Add(m_scalar_Structure_B);
            }

            if (m_scalar_Boolean != null)
            {
                children.Add(m_scalar_Boolean);
            }

            if (m_scalar_Duration != null)
            {
                children.Add(m_scalar_Duration);
            }

            if (m_scalar_RolePermissionType != null)
            {
                children.Add(m_scalar_RolePermissionType);
            }

            if (m_scalar_String_no_max_length != null)
            {
                children.Add(m_scalar_String_no_max_length);
            }

            if (m_scalar_String_within_max_length != null)
            {
                children.Add(m_scalar_String_within_max_length);
            }

            if (m_s1D_Array_String_no_max_length != null)
            {
                children.Add(m_s1D_Array_String_no_max_length);
            }

            if (m_s1D_Array_String_within_max_length != null)
            {
                children.Add(m_s1D_Array_String_within_max_length);
            }

            if (m_s1D_Array_of_BaseDataType != null)
            {
                children.Add(m_s1D_Array_of_BaseDataType);
            }

            if (m_s1D_Array_of_Structure__unknown_ != null)
            {
                children.Add(m_s1D_Array_of_Structure__unknown_);
            }

            if (m_s1D_Array_of_Structure__known_ != null)
            {
                children.Add(m_s1D_Array_of_Structure__known_);
            }

            if (m_s1D_Array_of_ApplicationDescription != null)
            {
                children.Add(m_s1D_Array_of_ApplicationDescription);
            }

            if (m_s1D_Array_of_Structure_A != null)
            {
                children.Add(m_s1D_Array_of_Structure_A);
            }

            if (m_s1D_Array_of_Structure_B != null)
            {
                children.Add(m_s1D_Array_of_Structure_B);
            }

            if (m_s1D_Array_of_Boolean != null)
            {
                children.Add(m_s1D_Array_of_Boolean);
            }

            if (m_s1D_Array_of_Duration != null)
            {
                children.Add(m_s1D_Array_of_Duration);
            }

            if (m_s1D_Array_of_RolePermissionType != null)
            {
                children.Add(m_s1D_Array_of_RolePermissionType);
            }

            if (m_s2D_Array_of_BaseDataType != null)
            {
                children.Add(m_s2D_Array_of_BaseDataType);
            }

            if (m_s2D_Array_of_Structure != null)
            {
                children.Add(m_s2D_Array_of_Structure);
            }

            if (m_s2D_Array_of_ApplicationDescription != null)
            {
                children.Add(m_s2D_Array_of_ApplicationDescription);
            }

            if (m_s2D_Array_of_Structure_A != null)
            {
                children.Add(m_s2D_Array_of_Structure_A);
            }

            if (m_s2D_Array_of_Structure_B != null)
            {
                children.Add(m_s2D_Array_of_Structure_B);
            }

            if (m_s2D_Array_of_Boolean != null)
            {
                children.Add(m_s2D_Array_of_Boolean);
            }

            if (m_s2D_Array_of_Duration != null)
            {
                children.Add(m_s2D_Array_of_Duration);
            }

            if (m_s2D_Array_of_RolePermissionType != null)
            {
                children.Add(m_s2D_Array_of_RolePermissionType);
            }

            base.GetChildren(context, children);
        }
            
        /// <remarks />
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case StructuresWithArrays.BrowseNames.Scalar_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_BaseDataType = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Scalar_BaseDataType = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Scalar_BaseDataType;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_Structure:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Structure == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Structure = new BaseDataVariableState<ExtensionObject>(this);
                            }
                            else
                            {
                                Scalar_Structure = (BaseDataVariableState<ExtensionObject>)replacement;
                            }
                        }
                    }

                    instance = Scalar_Structure;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_ApplicationDescription = new BaseDataVariableState<ApplicationDescription>(this);
                            }
                            else
                            {
                                Scalar_ApplicationDescription = (BaseDataVariableState<ApplicationDescription>)replacement;
                            }
                        }
                    }

                    instance = Scalar_ApplicationDescription;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_Structure_A:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Structure_A == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Structure_A = new Structure_A_TypeState(this);
                            }
                            else
                            {
                                Scalar_Structure_A = (Structure_A_TypeState)replacement;
                            }
                        }
                    }

                    instance = Scalar_Structure_A;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_Structure_B:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Structure_B == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Structure_B = new Structure_B_TypeState(this);
                            }
                            else
                            {
                                Scalar_Structure_B = (Structure_B_TypeState)replacement;
                            }
                        }
                    }

                    instance = Scalar_Structure_B;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_Boolean:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Boolean == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Boolean = new BaseDataVariableState<bool>(this);
                            }
                            else
                            {
                                Scalar_Boolean = (BaseDataVariableState<bool>)replacement;
                            }
                        }
                    }

                    instance = Scalar_Boolean;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_Duration:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_Duration == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_Duration = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Scalar_Duration = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Scalar_Duration;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_RolePermissionType:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_RolePermissionType == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_RolePermissionType = new BaseDataVariableState<RolePermissionType>(this);
                            }
                            else
                            {
                                Scalar_RolePermissionType = (BaseDataVariableState<RolePermissionType>)replacement;
                            }
                        }
                    }

                    instance = Scalar_RolePermissionType;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_String_no_max_length:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_String_no_max_length == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_String_no_max_length = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Scalar_String_no_max_length = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Scalar_String_no_max_length;
                    break;
                }

                case StructuresWithArrays.BrowseNames.Scalar_String_within_max_length:
                {
                    if (createOrReplace)
                    {
                        if (Scalar_String_within_max_length == null)
                        {
                            if (replacement == null)
                            {
                                Scalar_String_within_max_length = new BaseDataVariableState(this);
                            }
                            else
                            {
                                Scalar_String_within_max_length = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = Scalar_String_within_max_length;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_String_no_max_length:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_String_no_max_length == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_String_no_max_length = new BaseDataVariableState<string[]>(this);
                            }
                            else
                            {
                                S1D_Array_String_no_max_length = (BaseDataVariableState<string[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_String_no_max_length;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_String_within_max_length:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_String_within_max_length == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_String_within_max_length = new BaseDataVariableState<string[]>(this);
                            }
                            else
                            {
                                S1D_Array_String_within_max_length = (BaseDataVariableState<string[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_String_within_max_length;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_BaseDataType = new BaseDataVariableState<object[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_BaseDataType = (BaseDataVariableState<object[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_BaseDataType;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure__unknown_:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure__unknown_ == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure__unknown_ = new BaseDataVariableState<ExtensionObject[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure__unknown_ = (BaseDataVariableState<ExtensionObject[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure__unknown_;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure__known_:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure__known_ == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure__known_ = new BaseDataVariableState<ExtensionObject[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure__known_ = (BaseDataVariableState<ExtensionObject[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure__known_;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_ApplicationDescription = new BaseDataVariableState<ApplicationDescription[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_ApplicationDescription = (BaseDataVariableState<ApplicationDescription[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_ApplicationDescription;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure_A:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure_A == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure_A = new BaseDataVariableState<Structure_A[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure_A = (BaseDataVariableState<Structure_A[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure_A;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Structure_B:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Structure_B == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Structure_B = new BaseDataVariableState<Structure_B[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Structure_B = (BaseDataVariableState<Structure_B[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Structure_B;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Boolean:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Boolean == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Boolean = new BaseDataVariableState<bool[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Boolean = (BaseDataVariableState<bool[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Boolean;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_Duration:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_Duration == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_Duration = new BaseDataVariableState<double[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_Duration = (BaseDataVariableState<double[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_Duration;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S1D_Array_of_RolePermissionType:
                {
                    if (createOrReplace)
                    {
                        if (S1D_Array_of_RolePermissionType == null)
                        {
                            if (replacement == null)
                            {
                                S1D_Array_of_RolePermissionType = new BaseDataVariableState<RolePermissionType[]>(this);
                            }
                            else
                            {
                                S1D_Array_of_RolePermissionType = (BaseDataVariableState<RolePermissionType[]>)replacement;
                            }
                        }
                    }

                    instance = S1D_Array_of_RolePermissionType;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_BaseDataType:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_BaseDataType == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_BaseDataType = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_BaseDataType = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_BaseDataType;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_Structure:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Structure == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Structure = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_Structure = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Structure;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_ApplicationDescription:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_ApplicationDescription == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_ApplicationDescription = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_ApplicationDescription = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_ApplicationDescription;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_Structure_A:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Structure_A == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Structure_A = new Structure_A_TypeState(this);
                            }
                            else
                            {
                                S2D_Array_of_Structure_A = (Structure_A_TypeState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Structure_A;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_Structure_B:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Structure_B == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Structure_B = new Structure_B_TypeState(this);
                            }
                            else
                            {
                                S2D_Array_of_Structure_B = (Structure_B_TypeState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Structure_B;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_Boolean:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Boolean == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Boolean = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_Boolean = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Boolean;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_Duration:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_Duration == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_Duration = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_Duration = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_Duration;
                    break;
                }

                case StructuresWithArrays.BrowseNames.S2D_Array_of_RolePermissionType:
                {
                    if (createOrReplace)
                    {
                        if (S2D_Array_of_RolePermissionType == null)
                        {
                            if (replacement == null)
                            {
                                S2D_Array_of_RolePermissionType = new BaseDataVariableState(this);
                            }
                            else
                            {
                                S2D_Array_of_RolePermissionType = (BaseDataVariableState)replacement;
                            }
                        }
                    }

                    instance = S2D_Array_of_RolePermissionType;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState m_scalar_BaseDataType;
        private BaseDataVariableState<ExtensionObject> m_scalar_Structure;
        private BaseDataVariableState<ApplicationDescription> m_scalar_ApplicationDescription;
        private Structure_A_TypeState m_scalar_Structure_A;
        private Structure_B_TypeState m_scalar_Structure_B;
        private BaseDataVariableState<bool> m_scalar_Boolean;
        private BaseDataVariableState<double> m_scalar_Duration;
        private BaseDataVariableState<RolePermissionType> m_scalar_RolePermissionType;
        private BaseDataVariableState m_scalar_String_no_max_length;
        private BaseDataVariableState m_scalar_String_within_max_length;
        private BaseDataVariableState<string[]> m_s1D_Array_String_no_max_length;
        private BaseDataVariableState<string[]> m_s1D_Array_String_within_max_length;
        private BaseDataVariableState<object[]> m_s1D_Array_of_BaseDataType;
        private BaseDataVariableState<ExtensionObject[]> m_s1D_Array_of_Structure__unknown_;
        private BaseDataVariableState<ExtensionObject[]> m_s1D_Array_of_Structure__known_;
        private BaseDataVariableState<ApplicationDescription[]> m_s1D_Array_of_ApplicationDescription;
        private BaseDataVariableState<Structure_A[]> m_s1D_Array_of_Structure_A;
        private BaseDataVariableState<Structure_B[]> m_s1D_Array_of_Structure_B;
        private BaseDataVariableState<bool[]> m_s1D_Array_of_Boolean;
        private BaseDataVariableState<double[]> m_s1D_Array_of_Duration;
        private BaseDataVariableState<RolePermissionType[]> m_s1D_Array_of_RolePermissionType;
        private BaseDataVariableState m_s2D_Array_of_BaseDataType;
        private BaseDataVariableState m_s2D_Array_of_Structure;
        private BaseDataVariableState m_s2D_Array_of_ApplicationDescription;
        private Structure_A_TypeState m_s2D_Array_of_Structure_A;
        private Structure_B_TypeState m_s2D_Array_of_Structure_B;
        private BaseDataVariableState m_s2D_Array_of_Boolean;
        private BaseDataVariableState m_s2D_Array_of_Duration;
        private BaseDataVariableState m_s2D_Array_of_RolePermissionType;
        #endregion
    }

    #region LargeComplexStructureTypeValue Class
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class LargeComplexStructureTypeValue : BaseVariableValue
    {
        #region Constructors
        /// <remarks />
        public LargeComplexStructureTypeValue(LargeComplexStructureTypeState variable, LargeComplexStructure value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new LargeComplexStructure();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <remarks />
        public LargeComplexStructureTypeState Variable
        {
            get { return m_variable; }
        }

        /// <remarks />
        public LargeComplexStructure Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        private void Initialize(LargeComplexStructureTypeState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.Scalar_BaseDataType;
                instance.OnReadValue = OnRead_Scalar_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_Scalar_BaseDataType;
                updateList.Add(instance);
                instance = m_variable.Scalar_Structure;
                instance.OnReadValue = OnRead_Scalar_Structure;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Structure;
                updateList.Add(instance);
                instance = m_variable.Scalar_ApplicationDescription;
                instance.OnReadValue = OnRead_Scalar_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_Scalar_ApplicationDescription;
                updateList.Add(instance);
                instance = m_variable.Scalar_Structure_A;
                instance.OnReadValue = OnRead_Scalar_Structure_A;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Structure_A;
                updateList.Add(instance);
                instance = m_variable.Scalar_Structure_B;
                instance.OnReadValue = OnRead_Scalar_Structure_B;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Structure_B;
                updateList.Add(instance);
                instance = m_variable.Scalar_Boolean;
                instance.OnReadValue = OnRead_Scalar_Boolean;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Boolean;
                updateList.Add(instance);
                instance = m_variable.Scalar_Duration;
                instance.OnReadValue = OnRead_Scalar_Duration;
                instance.OnSimpleWriteValue = OnWrite_Scalar_Duration;
                updateList.Add(instance);
                instance = m_variable.Scalar_RolePermissionType;
                instance.OnReadValue = OnRead_Scalar_RolePermissionType;
                instance.OnSimpleWriteValue = OnWrite_Scalar_RolePermissionType;
                updateList.Add(instance);
                instance = m_variable.Scalar_String_no_max_length;
                instance.OnReadValue = OnRead_Scalar_String_no_max_length;
                instance.OnSimpleWriteValue = OnWrite_Scalar_String_no_max_length;
                updateList.Add(instance);
                instance = m_variable.Scalar_String_within_max_length;
                instance.OnReadValue = OnRead_Scalar_String_within_max_length;
                instance.OnSimpleWriteValue = OnWrite_Scalar_String_within_max_length;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_String_no_max_length;
                instance.OnReadValue = OnRead_S1D_Array_String_no_max_length;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_String_no_max_length;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_String_within_max_length;
                instance.OnReadValue = OnRead_S1D_Array_String_within_max_length;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_String_within_max_length;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_BaseDataType;
                instance.OnReadValue = OnRead_S1D_Array_of_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_BaseDataType;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Structure__unknown_;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure__unknown_;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure__unknown_;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Structure__known_;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure__known_;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure__known_;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_ApplicationDescription;
                instance.OnReadValue = OnRead_S1D_Array_of_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_ApplicationDescription;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Structure_A;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure_A;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure_A;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Structure_B;
                instance.OnReadValue = OnRead_S1D_Array_of_Structure_B;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Structure_B;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Boolean;
                instance.OnReadValue = OnRead_S1D_Array_of_Boolean;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Boolean;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_Duration;
                instance.OnReadValue = OnRead_S1D_Array_of_Duration;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_Duration;
                updateList.Add(instance);
                instance = m_variable.S1D_Array_of_RolePermissionType;
                instance.OnReadValue = OnRead_S1D_Array_of_RolePermissionType;
                instance.OnSimpleWriteValue = OnWrite_S1D_Array_of_RolePermissionType;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_BaseDataType;
                instance.OnReadValue = OnRead_S2D_Array_of_BaseDataType;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_BaseDataType;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_Structure;
                instance.OnReadValue = OnRead_S2D_Array_of_Structure;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Structure;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_ApplicationDescription;
                instance.OnReadValue = OnRead_S2D_Array_of_ApplicationDescription;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_ApplicationDescription;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_Structure_A;
                instance.OnReadValue = OnRead_S2D_Array_of_Structure_A;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Structure_A;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_Structure_B;
                instance.OnReadValue = OnRead_S2D_Array_of_Structure_B;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Structure_B;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_Boolean;
                instance.OnReadValue = OnRead_S2D_Array_of_Boolean;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Boolean;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_Duration;
                instance.OnReadValue = OnRead_S2D_Array_of_Duration;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_Duration;
                updateList.Add(instance);
                instance = m_variable.S2D_Array_of_RolePermissionType;
                instance.OnReadValue = OnRead_S2D_Array_of_RolePermissionType;
                instance.OnSimpleWriteValue = OnWrite_S2D_Array_of_RolePermissionType;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <remarks />
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (LargeComplexStructure)Write(value);
            }

            return ServiceResult.Good;
        }

        #region Scalar_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_BaseDataType = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_Structure Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Structure(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Structure;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Structure(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Structure = (ExtensionObject)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_ApplicationDescription = (Opc.Ua.ApplicationDescription)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_Structure_A Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Structure_A(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Structure_A;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Structure_A(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Structure_A = (Structure_A)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_Structure_B Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Structure_B(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Structure_B;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Structure_B(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Structure_B = (Structure_B)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_Boolean Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Boolean(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Boolean;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Boolean(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Boolean = (bool)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_Duration Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_Duration(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_Duration;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_Duration(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_Duration = (double)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_RolePermissionType Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_RolePermissionType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_RolePermissionType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_RolePermissionType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_RolePermissionType = (Opc.Ua.RolePermissionType)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_String_no_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_String_no_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_String_no_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_String_no_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_String_no_max_length = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region Scalar_String_within_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_Scalar_String_within_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.Scalar_String_within_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_Scalar_String_within_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.Scalar_String_within_max_length = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_String_no_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_String_no_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_String_no_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_String_no_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_String_no_max_length = (StringCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_String_within_max_length Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_String_within_max_length(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_String_within_max_length;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_String_within_max_length(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_String_within_max_length = (StringCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_BaseDataType = (VariantCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Structure__unknown_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure__unknown_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure__unknown_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure__unknown_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure__unknown_ = (ExtensionObjectCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Structure__known_ Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure__known_(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure__known_;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure__known_(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure__known_ = (ExtensionObjectCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_ApplicationDescription = (ApplicationDescriptionCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Structure_A Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure_A(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure_A;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure_A(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure_A = (Structure_ACollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Structure_B Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Structure_B(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Structure_B;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Structure_B(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Structure_B = (Structure_BCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Boolean Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Boolean(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Boolean;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Boolean(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Boolean = (BooleanCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_Duration Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_Duration(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_Duration;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_Duration(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_Duration = (DoubleCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S1D_Array_of_RolePermissionType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S1D_Array_of_RolePermissionType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S1D_Array_of_RolePermissionType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S1D_Array_of_RolePermissionType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S1D_Array_of_RolePermissionType = (RolePermissionTypeCollection)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_BaseDataType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_BaseDataType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_BaseDataType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_BaseDataType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_BaseDataType = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_Structure Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Structure(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Structure;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Structure(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Structure = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_ApplicationDescription Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_ApplicationDescription(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_ApplicationDescription;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_ApplicationDescription(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_ApplicationDescription = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_Structure_A Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Structure_A(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Structure_A;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Structure_A(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Structure_A = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_Structure_B Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Structure_B(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Structure_B;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Structure_B(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Structure_B = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_Boolean Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Boolean(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Boolean;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Boolean(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Boolean = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_Duration Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_Duration(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_Duration;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_Duration(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_Duration = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region S2D_Array_of_RolePermissionType Access Methods
        /// <remarks />
        private ServiceResult OnRead_S2D_Array_of_RolePermissionType(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.S2D_Array_of_RolePermissionType;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <remarks />
        private ServiceResult OnWrite_S2D_Array_of_RolePermissionType(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.S2D_Array_of_RolePermissionType = (Variant)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private LargeComplexStructure m_value;
        private LargeComplexStructureTypeState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion
}