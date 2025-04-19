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
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace StructuresWithArrays
{
    #region DataType Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypes
    {
        /// <remarks />
        public const uint Structure_A = 1000;

        /// <remarks />
        public const uint Structure_B = 1001;

        /// <remarks />
        public const uint Structure_C = 1002;

        /// <remarks />
        public const uint Structure_D = 1003;

        /// <remarks />
        public const uint Structure_E = 1004;

        /// <remarks />
        public const uint Structure_F = 1005;

        /// <remarks />
        public const uint Structure_G = 1006;

        /// <remarks />
        public const uint Structure_H = 1007;

        /// <remarks />
        public const uint Structure_J = 1008;

        /// <remarks />
        public const uint Structure_K = 1009;

        /// <remarks />
        public const uint Structure_L = 1010;

        /// <remarks />
        public const uint Structure_M = 1011;

        /// <remarks />
        public const uint Structure_N = 1012;

        /// <remarks />
        public const uint Structure_O = 1013;

        /// <remarks />
        public const uint Structure_P = 1014;

        /// <remarks />
        public const uint Structure_Q = 1015;

        /// <remarks />
        public const uint Structure_R = 1016;

        /// <remarks />
        public const uint Structure_S = 1017;

        /// <remarks />
        public const uint Structure_T = 1018;

        /// <remarks />
        public const uint LargeComplexStructure = 2000;
    }
    #endregion

    #region Object Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <remarks />
        public const uint Structure_A_Encoding_DefaultXml = 5000;

        /// <remarks />
        public const uint Structure_A_Encoding_DefaultBinary = 5001;

        /// <remarks />
        public const uint Structure_B_Encoding_DefaultBinary = 5002;

        /// <remarks />
        public const uint Structure_B_Encoding_DefaultXml = 5003;

        /// <remarks />
        public const uint Structure_C_Encoding_DefaultBinary = 5004;

        /// <remarks />
        public const uint Structure_C_Encoding_DefaultXml = 5005;

        /// <remarks />
        public const uint Structure_D_Encoding_DefaultBinary = 5006;

        /// <remarks />
        public const uint Structure_D_Encoding_DefaultXml = 5007;

        /// <remarks />
        public const uint Structure_E_Encoding_DefaultBinary = 5008;

        /// <remarks />
        public const uint Structure_E_Encoding_DefaultXml = 5009;

        /// <remarks />
        public const uint Structure_F_Encoding_DefaultBinary = 5010;

        /// <remarks />
        public const uint Structure_F_Encoding_DefaultXml = 5011;

        /// <remarks />
        public const uint Structure_G_Encoding_DefaultBinary = 5012;

        /// <remarks />
        public const uint Structure_G_Encoding_DefaultXml = 5013;

        /// <remarks />
        public const uint Structure_H_Encoding_DefaultBinary = 5014;

        /// <remarks />
        public const uint Structure_H_Encoding_DefaultXml = 5015;

        /// <remarks />
        public const uint Structure_J_Encoding_DefaultBinary = 5016;

        /// <remarks />
        public const uint Structure_J_Encoding_DefaultXml = 5017;

        /// <remarks />
        public const uint Structure_K_Encoding_DefaultBinary = 5018;

        /// <remarks />
        public const uint Structure_K_Encoding_DefaultXml = 5019;

        /// <remarks />
        public const uint Structure_L_Encoding_DefaultBinary = 5020;

        /// <remarks />
        public const uint Structure_L_Encoding_DefaultXml = 5021;

        /// <remarks />
        public const uint Structure_M_Encoding_DefaultBinary = 5022;

        /// <remarks />
        public const uint Structure_M_Encoding_DefaultXml = 5023;

        /// <remarks />
        public const uint Structure_N_Encoding_DefaultBinary = 5024;

        /// <remarks />
        public const uint Structure_N_Encoding_DefaultXml = 5025;

        /// <remarks />
        public const uint Structure_O_Encoding_DefaultBinary = 5026;

        /// <remarks />
        public const uint Structure_O_Encoding_DefaultXml = 5027;

        /// <remarks />
        public const uint Structure_P_Encoding_DefaultBinary = 5028;

        /// <remarks />
        public const uint Structure_P_Encoding_DefaultXml = 5029;

        /// <remarks />
        public const uint Structure_Q_Encoding_DefaultBinary = 5030;

        /// <remarks />
        public const uint Structure_Q_Encoding_DefaultXml = 5031;

        /// <remarks />
        public const uint Structure_R_Encoding_DefaultBinary = 5032;

        /// <remarks />
        public const uint Structure_R_Encoding_DefaultXml = 5033;

        /// <remarks />
        public const uint Structure_S_Encoding_DefaultBinary = 5034;

        /// <remarks />
        public const uint Structure_S_Encoding_DefaultXml = 5035;

        /// <remarks />
        public const uint Structure_T_Encoding_DefaultBinary = 5036;

        /// <remarks />
        public const uint Structure_T_Encoding_DefaultXml = 5037;

        /// <remarks />
        public const uint LargeComplexStructure_Encoding_DefaultBinary = 5038;

        /// <remarks />
        public const uint LargeComplexStructure_Encoding_DefaultXml = 5039;

        /// <remarks />
        public const uint Structures = 5043;
    }
    #endregion

    #region Variable Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <remarks />
        public const uint Structures_OneDimArray_Structure_A_Array_Duration_Field = 6203;

        /// <remarks />
        public const uint Structures_OneDimArray_Structure_A_Array_Double_Field = 6202;

        /// <remarks />
        public const uint Structures_Scalar_Structure_A_Duration_Field = 6206;

        /// <remarks />
        public const uint Structures_Scalar_Structure_A_Double_Field = 6205;

        /// <remarks />
        public const uint Structures_Scalar_Structure_B_Int32_Field = 6009;

        /// <remarks />
        public const uint Structures_Scalar_Structure_C_Int32_Field = 6011;

        /// <remarks />
        public const uint Structures_Scalar_Structure_C_NodeId_Field = 6111;

        /// <remarks />
        public const uint Structures_Scalar_Structure_D_Scalar_BaseDataType = 6113;

        /// <remarks />
        public const uint Structures_Scalar_Structure_E_Scalar_Structure = 6115;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription = 6118;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationUri = 6121;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ProductUri = 6125;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationName = 6119;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationType = 6120;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_GatewayServerUri = 6124;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryProfileUri = 6122;

        /// <remarks />
        public const uint Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryUrls = 6123;

        /// <remarks />
        public const uint Structures_Scalar_Structure_G_Scalar_Structure_A = 6127;

        /// <remarks />
        public const uint Structures_Scalar_Structure_G_Scalar_Structure_A_Duration_Field = 6129;

        /// <remarks />
        public const uint Structures_Scalar_Structure_G_Scalar_Structure_A_Double_Field = 6128;

        /// <remarks />
        public const uint Structures_Scalar_Structure_H_Scalar_Duration = 6131;

        /// <remarks />
        public const uint Structures_Scalar_Structure_J_Scalar_MessageSecurityMode = 6133;

        /// <remarks />
        public const uint Structures_Scalar_Structure_K_S1D_Array_String_within_max_length = 6135;

        /// <remarks />
        public const uint Structures_Scalar_Structure_L_S1D_Array_of_BaseDataType = 6137;

        /// <remarks />
        public const uint Structures_Scalar_Structure_M_S1D_Array_of_Structure__unknown_ = 6139;

        /// <remarks />
        public const uint Structures_Scalar_Structure_N_S1D_Array_of_Structure__known_ = 6141;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription = 6143;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = 6034;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = 6035;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = 6036;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = 6037;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = 6038;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = 6039;

        /// <remarks />
        public const uint Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = 6040;

        /// <remarks />
        public const uint Structures_Scalar_Structure_P_S2D_Array_String_within_max_length = 6145;

        /// <remarks />
        public const uint Structures_Scalar_Structure_Q_S2D_Array_of_BaseDataType = 6148;

        /// <remarks />
        public const uint Structures_Scalar_Structure_R_S2D_Array_of_Structure__unknown_ = 6150;

        /// <remarks />
        public const uint Structures_Scalar_Structure_S_S2D_Array_of_Structure__known_ = 6152;

        /// <remarks />
        public const uint Structures_Scalar_Structure_T_S2D_Array_of_ApplicationDescription = 6154;

        /// <remarks />
        public const uint Structures_TwoDimArray_Structure_A_Duration_Field = 6209;

        /// <remarks />
        public const uint Structures_TwoDimArray_Structure_A_Double_Field = 6208;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_BaseDataType = 6177;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure = 6183;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription = 6176;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_ApplicationUri = 6191;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_ProductUri = 6195;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_ApplicationName = 6189;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_ApplicationType = 6190;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_GatewayServerUri = 6194;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryProfileUri = 6192;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryUrls = 6193;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure_A = 6184;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure_A_Duration_Field = 6199;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure_A_Double_Field = 6198;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure_B = 6185;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Structure_B_Int32_Field = 6200;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Boolean = 6178;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_Duration = 6179;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_RolePermissionType = 6180;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_RolePermissionType_RoleId = 6197;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_RolePermissionType_Permissions = 6196;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_String_no_max_length = 6181;

        /// <remarks />
        public const uint Structures_LargeArray_Scalar_String_within_max_length = 6182;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_String_no_max_length = 6166;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_String_within_max_length = 6167;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_BaseDataType = 6158;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure__unknown_ = 6163;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure__known_ = 6162;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription = 6157;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = 6097;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = 6098;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = 6099;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = 6100;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = 6101;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = 6102;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = 6103;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure_A = 6164;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field = 6104;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field = 6105;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure_B = 6165;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field = 6107;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Boolean = 6159;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_Duration = 6160;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_RolePermissionType = 6161;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId = 6108;

        /// <remarks />
        public const uint Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions = 6109;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_BaseDataType = 6169;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure = 6173;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_ApplicationDescription = 6168;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure_A = 6174;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure_A_Duration_Field = 6187;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure_A_Double_Field = 6186;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure_B = 6175;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Structure_B_Int32_Field = 6188;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Boolean = 6170;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_Duration = 6171;

        /// <remarks />
        public const uint Structures_LargeArray_S2D_Array_of_RolePermissionType = 6172;

        /// <remarks />
        public const uint Structure_A_Type_Duration_Field = 6000;

        /// <remarks />
        public const uint Structure_A_Type_Double_Field = 6001;

        /// <remarks />
        public const uint Structure_B_Type_Duration_Field = 6002;

        /// <remarks />
        public const uint Structure_B_Type_Int32_Field = 6003;

        /// <remarks />
        public const uint Structure_C_Type_Int32_Field = 6004;

        /// <remarks />
        public const uint Structure_C_Type_NodeId_Field = 6005;

        /// <remarks />
        public const uint Structure_D_Type_Scalar_BaseDataType = 6006;

        /// <remarks />
        public const uint Structure_E_Type_Scalar_Structure = 6007;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription = 6012;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_ApplicationUri = 6013;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_ProductUri = 6014;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_ApplicationName = 6015;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_ApplicationType = 6016;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_GatewayServerUri = 6017;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_DiscoveryProfileUri = 6018;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls = 6019;

        /// <remarks />
        public const uint Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_ = 6020;

        /// <remarks />
        public const uint Structure_G_Type_Scalar_Structure_A = 6021;

        /// <remarks />
        public const uint Structure_G_Type_Scalar_Structure_A_Duration_Field = 6000;

        /// <remarks />
        public const uint Structure_G_Type_Scalar_Structure_A_Double_Field = 6001;

        /// <remarks />
        public const uint Structure_H_Type_Scalar_Duration = 6022;

        /// <remarks />
        public const uint Structure_J_Type_Scalar_MessageSecurityMode = 6023;

        /// <remarks />
        public const uint Structure_K_Type_S1D_Array_String_within_max_length = 6024;

        /// <remarks />
        public const uint Structure_K_Type_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_ = 6025;

        /// <remarks />
        public const uint Structure_L_Type_S1D_Array_of_BaseDataType = 6026;

        /// <remarks />
        public const uint Structure_L_Type_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_ = 6027;

        /// <remarks />
        public const uint Structure_M_Type_S1D_Array_of_Structure__unknown_ = 6028;

        /// <remarks />
        public const uint Structure_M_Type_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_ = 6029;

        /// <remarks />
        public const uint Structure_N_Type_S1D_Array_of_Structure__known_ = 6030;

        /// <remarks />
        public const uint Structure_N_Type_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_ = 6031;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription = 6032;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_ = 6033;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = 6034;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = 6035;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = 6036;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = 6037;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = 6038;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = 6039;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = 6040;

        /// <remarks />
        public const uint Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_ = 6041;

        /// <remarks />
        public const uint Structure_P_Type_S2D_Array_String_within_max_length = 6042;

        /// <remarks />
        public const uint Structure_Q_Type_S2D_Array_of_BaseDataType = 6043;

        /// <remarks />
        public const uint Structure_R__Type_S2D_Array_of_Structure__unknown_ = 6044;

        /// <remarks />
        public const uint Structure_S_Type_S2D_Array_of_Structure__known_ = 6045;

        /// <remarks />
        public const uint Structure_T_Type_S2D_Array_of_ApplicationDescription = 6046;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_BaseDataType = 6047;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure = 6048;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription = 6049;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationUri = 6076;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_ProductUri = 6077;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationName = 6078;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationType = 6079;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_GatewayServerUri = 6080;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryProfileUri = 6081;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls = 6082;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_ = 6096;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure_A = 6050;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure_A_Duration_Field = 6000;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure_A_Double_Field = 6001;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure_B = 6051;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Structure_B_Int32_Field = 6003;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Boolean = 6052;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_Duration = 6053;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_RolePermissionType = 6054;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_RolePermissionType_RoleId = 6083;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_RolePermissionType_Permissions = 6084;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_String_no_max_length = 6055;

        /// <remarks />
        public const uint LargeComplexStructureType_Scalar_String_within_max_length = 6056;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_String_no_max_length = 6057;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_String_no_max_length_S1D_Array_String_no_max_length_no_ = 6085;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_String_within_max_length = 6058;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_ = 6086;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_BaseDataType = 6059;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_ = 6087;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure__unknown_ = 6060;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_ = 6088;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure__known_ = 6061;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_ = 6089;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription = 6062;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_ = 6090;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = 6097;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = 6098;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = 6099;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = 6100;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = 6101;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = 6102;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = 6103;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_ = 6110;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_A = 6063;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no_ = 6091;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field = 6104;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field = 6105;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_B = 6064;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no_ = 6092;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Duration_Field = 6106;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field = 6107;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Boolean = 6065;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Boolean_S1D_Array_of_Boolean_no_ = 6093;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Duration = 6066;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_Duration_S1D_Array_of_Duration_no_ = 6094;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_RolePermissionType = 6067;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no_ = 6095;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId = 6108;

        /// <remarks />
        public const uint LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions = 6109;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_BaseDataType = 6068;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure = 6069;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_ApplicationDescription = 6070;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure_A = 6071;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure_A_Duration_Field = 6000;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure_A_Double_Field = 6001;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure_B = 6072;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Structure_B_Int32_Field = 6003;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Boolean = 6073;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_Duration = 6074;

        /// <remarks />
        public const uint LargeComplexStructureType_S2D_Array_of_RolePermissionType = 6075;
    }
    #endregion

    #region VariableType Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableTypes
    {
        /// <remarks />
        public const uint Structure_A_Type = 2001;

        /// <remarks />
        public const uint Structure_B_Type = 2002;

        /// <remarks />
        public const uint Structure_C_Type = 2003;

        /// <remarks />
        public const uint Structure_D_Type = 2004;

        /// <remarks />
        public const uint Structure_E_Type = 2005;

        /// <remarks />
        public const uint Structure_F_Type = 2006;

        /// <remarks />
        public const uint Structure_G_Type = 2007;

        /// <remarks />
        public const uint Structure_H_Type = 2008;

        /// <remarks />
        public const uint Structure_J_Type = 2009;

        /// <remarks />
        public const uint Structure_K_Type = 2010;

        /// <remarks />
        public const uint Structure_L_Type = 2011;

        /// <remarks />
        public const uint Structure_M_Type = 2012;

        /// <remarks />
        public const uint Structure_N_Type = 2013;

        /// <remarks />
        public const uint Structure_O_Type = 2014;

        /// <remarks />
        public const uint Structure_P_Type = 2015;

        /// <remarks />
        public const uint Structure_Q_Type = 2016;

        /// <remarks />
        public const uint Structure_R__Type = 2017;

        /// <remarks />
        public const uint Structure_S_Type = 2018;

        /// <remarks />
        public const uint Structure_T_Type = 2019;

        /// <remarks />
        public const uint LargeComplexStructureType = 2020;
    }
    #endregion

    #region DataType Node Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypeIds
    {
        /// <remarks />
        public static readonly ExpandedNodeId Structure_A = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_C, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_D = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_D, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_E = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_E, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_F, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_G, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_H = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_H, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_J = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_J, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_K, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_L, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_M, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_N, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_O, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_P = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_P, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_Q = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_Q, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_R = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_R, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_S = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_S, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_T = new ExpandedNodeId(StructuresWithArrays.DataTypes.Structure_T, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructure = new ExpandedNodeId(StructuresWithArrays.DataTypes.LargeComplexStructure, StructuresWithArrays.Namespaces.StructuresWithArrays);
    }
    #endregion

    #region Object Node Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <remarks />
        public static readonly ExpandedNodeId Structure_A_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_A_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_A_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_A_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_B_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_B_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_C_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_C_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_D_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_D_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_D_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_D_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_E_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_E_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_E_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_E_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_F_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_F_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_G_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_G_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_H_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_H_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_H_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_H_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_J_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_J_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_J_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_J_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_K_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_K_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_L_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_L_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_M_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_M_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_N_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_N_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_O_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_O_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_P_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_P_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_P_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_P_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_Q_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_Q_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_Q_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_Q_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_R_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_R_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_R_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_R_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_S_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_S_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_S_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_S_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_T_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_T_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_T_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.Structure_T_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructure_Encoding_DefaultBinary = new ExpandedNodeId(StructuresWithArrays.Objects.LargeComplexStructure_Encoding_DefaultBinary, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructure_Encoding_DefaultXml = new ExpandedNodeId(StructuresWithArrays.Objects.LargeComplexStructure_Encoding_DefaultXml, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures = new ExpandedNodeId(StructuresWithArrays.Objects.Structures, StructuresWithArrays.Namespaces.StructuresWithArrays);
    }
    #endregion

    #region Variable Node Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <remarks />
        public static readonly ExpandedNodeId Structures_OneDimArray_Structure_A_Array_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_OneDimArray_Structure_A_Array_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_OneDimArray_Structure_A_Array_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_OneDimArray_Structure_A_Array_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_B_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_B_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_C_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_C_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_C_NodeId_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_C_NodeId_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_D_Scalar_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_D_Scalar_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_E_Scalar_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_E_Scalar_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_F_Scalar_ApplicationDescription_DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_G_Scalar_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_G_Scalar_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_G_Scalar_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_G_Scalar_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_G_Scalar_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_G_Scalar_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_H_Scalar_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_H_Scalar_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_J_Scalar_MessageSecurityMode = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_J_Scalar_MessageSecurityMode, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_K_S1D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_K_S1D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_L_S1D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_L_S1D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_M_S1D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_M_S1D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_N_S1D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_N_S1D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_O_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_P_S2D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_P_S2D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_Q_S2D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_Q_S2D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_R_S2D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_R_S2D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_S_S2D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_S_S2D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_Scalar_Structure_T_S2D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_Scalar_Structure_T_S2D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_TwoDimArray_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_TwoDimArray_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_TwoDimArray_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_TwoDimArray_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_ApplicationDescription_DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Structure_B_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Structure_B_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_RolePermissionType_RoleId = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_RolePermissionType_RoleId, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_RolePermissionType_Permissions = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_RolePermissionType_Permissions, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_String_no_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_String_no_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_Scalar_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_Scalar_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_String_no_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_String_no_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Structure_B_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Structure_B_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structures_LargeArray_S2D_Array_of_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.Structures_LargeArray_S2D_Array_of_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_A_Type_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_A_Type_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_A_Type_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_A_Type_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B_Type_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_B_Type_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B_Type_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_B_Type_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C_Type_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_C_Type_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C_Type_NodeId_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_C_Type_NodeId_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_D_Type_Scalar_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_D_Type_Scalar_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_E_Type_Scalar_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_E_Type_Scalar_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_F_Type_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Type_Scalar_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_G_Type_Scalar_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Type_Scalar_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_G_Type_Scalar_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Type_Scalar_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_G_Type_Scalar_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_H_Type_Scalar_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_H_Type_Scalar_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_J_Type_Scalar_MessageSecurityMode = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_J_Type_Scalar_MessageSecurityMode, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K_Type_S1D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_K_Type_S1D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K_Type_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_K_Type_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L_Type_S1D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_L_Type_S1D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L_Type_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_L_Type_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M_Type_S1D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_M_Type_S1D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M_Type_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_M_Type_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N_Type_S1D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_N_Type_S1D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N_Type_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_N_Type_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_O_Type_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_P_Type_S2D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_P_Type_S2D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_Q_Type_S2D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_Q_Type_S2D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_R__Type_S2D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_R__Type_S2D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_S_Type_S2D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_S_Type_S2D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_T_Type_S2D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.Structure_T_Type_S2D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_ApplicationDescription_DiscoveryUrls_DiscoveryUrls_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Structure_B_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Structure_B_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_RolePermissionType_RoleId = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_RolePermissionType_RoleId, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_RolePermissionType_Permissions = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_RolePermissionType_Permissions, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_String_no_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_String_no_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_Scalar_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_Scalar_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_String_no_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_String_no_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_String_no_max_length_S1D_Array_String_no_max_length_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_String_no_max_length_S1D_Array_String_no_max_length_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_String_within_max_length = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_String_within_max_length, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_String_within_max_length_S1D_Array_String_within_max_length_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_BaseDataType_S1D_Array_of_BaseDataType_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure__unknown_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure__unknown_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure__unknown__S1D_Array_of_Structure__unknown__no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure__known_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure__known_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure__known__S1D_Array_of_Structure__known__no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ProductUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationName, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__ApplicationType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__GatewayServerUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryProfileUri, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_ApplicationDescription_S1D_Array_of_ApplicationDescription_no__DiscoveryUrls_DiscoveryUrls_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_A_S1D_Array_of_Structure_A_no__Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Structure_B_S1D_Array_of_Structure_B_no__Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Boolean_S1D_Array_of_Boolean_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Boolean_S1D_Array_of_Boolean_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_Duration_S1D_Array_of_Duration_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_Duration_S1D_Array_of_Duration_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no_ = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no_, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__RoleId, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S1D_Array_of_RolePermissionType_S1D_Array_of_RolePermissionType_no__Permissions, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_BaseDataType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_BaseDataType, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_ApplicationDescription = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_ApplicationDescription, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure_A = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure_A, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure_A_Duration_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure_A_Duration_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure_A_Double_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure_A_Double_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure_B = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure_B, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Structure_B_Int32_Field = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Structure_B_Int32_Field, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Boolean = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Boolean, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_Duration = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_Duration, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType_S2D_Array_of_RolePermissionType = new ExpandedNodeId(StructuresWithArrays.Variables.LargeComplexStructureType_S2D_Array_of_RolePermissionType, StructuresWithArrays.Namespaces.StructuresWithArrays);
    }
    #endregion

    #region VariableType Node Identifiers
    /// <remarks />
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableTypeIds
    {
        /// <remarks />
        public static readonly ExpandedNodeId Structure_A_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_A_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_B_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_B_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_C_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_C_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_D_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_D_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_E_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_E_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_F_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_F_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_G_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_G_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_H_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_H_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_J_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_J_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_K_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_K_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_L_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_L_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_M_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_M_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_N_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_N_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_O_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_O_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_P_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_P_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_Q_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_Q_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_R__Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_R__Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_S_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_S_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId Structure_T_Type = new ExpandedNodeId(StructuresWithArrays.VariableTypes.Structure_T_Type, StructuresWithArrays.Namespaces.StructuresWithArrays);

        /// <remarks />
        public static readonly ExpandedNodeId LargeComplexStructureType = new ExpandedNodeId(StructuresWithArrays.VariableTypes.LargeComplexStructureType, StructuresWithArrays.Namespaces.StructuresWithArrays);
    }
    #endregion

    #region BrowseName Declarations
    /// <remarks />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class BrowseNames
    {
        /// <remarks />
        public const string Double_Field = "Double Field";

        /// <remarks />
        public const string Duration_Field = "Duration Field";

        /// <remarks />
        public const string Int32_Field = "Int32 Field";

        /// <remarks />
        public const string LargeArray = "LargeArray";

        /// <remarks />
        public const string LargeComplexStructure = "LargeComplexStructure";

        /// <remarks />
        public const string LargeComplexStructureType = "LargeComplexStructureType";

        /// <remarks />
        public const string NodeId_Field = "NodeId Field";

        /// <remarks />
        public const string OneDimArray = "OneDimArray";

        /// <remarks />
        public const string S1D_Array_of_ApplicationDescription = "1D Array of ApplicationDescription";

        /// <remarks />
        public const string S1D_Array_of_BaseDataType = "1D Array of BaseDataType";

        /// <remarks />
        public const string S1D_Array_of_Boolean = "1D Array of Boolean";

        /// <remarks />
        public const string S1D_Array_of_Duration = "1D Array of Duration";

        /// <remarks />
        public const string S1D_Array_of_RolePermissionType = "1D Array of RolePermissionType";

        /// <remarks />
        public const string S1D_Array_of_Structure__known_ = "1D Array of Structure (known)";

        /// <remarks />
        public const string S1D_Array_of_Structure__unknown_ = "1D Array of Structure (unknown)";

        /// <remarks />
        public const string S1D_Array_of_Structure_A = "1D Array of Structure A";

        /// <remarks />
        public const string S1D_Array_of_Structure_B = "1D Array of Structure B";

        /// <remarks />
        public const string S1D_Array_String_no_max_length = "1D Array String no max length";

        /// <remarks />
        public const string S1D_Array_String_within_max_length = "1D Array String within max length";

        /// <remarks />
        public const string S2D_Array_of_ApplicationDescription = "2D Array of ApplicationDescription";

        /// <remarks />
        public const string S2D_Array_of_BaseDataType = "2D Array of BaseDataType";

        /// <remarks />
        public const string S2D_Array_of_Boolean = "2D Array of Boolean";

        /// <remarks />
        public const string S2D_Array_of_Duration = "2D Array of Duration";

        /// <remarks />
        public const string S2D_Array_of_RolePermissionType = "2D Array of RolePermissionType";

        /// <remarks />
        public const string S2D_Array_of_Structure = "2D Array of Structure";

        /// <remarks />
        public const string S2D_Array_of_Structure__known_ = "2D Array of Structure (known)";

        /// <remarks />
        public const string S2D_Array_of_Structure__unknown_ = "2D Array of Structure (unknown)";

        /// <remarks />
        public const string S2D_Array_of_Structure_A = "2D Array of Structure A";

        /// <remarks />
        public const string S2D_Array_of_Structure_B = "2D Array of Structure B";

        /// <remarks />
        public const string S2D_Array_String_within_max_length = "2D Array String within max length";

        /// <remarks />
        public const string Scalar = "Scalar";

        /// <remarks />
        public const string Scalar_ApplicationDescription = "Scalar ApplicationDescription";

        /// <remarks />
        public const string Scalar_BaseDataType = "Scalar BaseDataType";

        /// <remarks />
        public const string Scalar_Boolean = "Scalar Boolean";

        /// <remarks />
        public const string Scalar_Duration = "Scalar Duration";

        /// <remarks />
        public const string Scalar_MessageSecurityMode = "Scalar MessageSecurityMode";

        /// <remarks />
        public const string Scalar_RolePermissionType = "Scalar RolePermissionType";

        /// <remarks />
        public const string Scalar_String_no_max_length = "Scalar String no max length";

        /// <remarks />
        public const string Scalar_String_within_max_length = "Scalar String within max length";

        /// <remarks />
        public const string Scalar_Structure = "Scalar Structure";

        /// <remarks />
        public const string Scalar_Structure_A = "Scalar Structure A";

        /// <remarks />
        public const string Scalar_Structure_B = "Scalar Structure B";

        /// <remarks />
        public const string Structure_A = "Structure A";

        /// <remarks />
        public const string Structure_A_Type = "Structure A Type";

        /// <remarks />
        public const string Structure_B = "Structure B";

        /// <remarks />
        public const string Structure_B_Type = "Structure B Type";

        /// <remarks />
        public const string Structure_C = "Structure C";

        /// <remarks />
        public const string Structure_C_Type = "Structure C Type";

        /// <remarks />
        public const string Structure_D = "Structure D";

        /// <remarks />
        public const string Structure_D_Type = "Structure D Type";

        /// <remarks />
        public const string Structure_E = "Structure E";

        /// <remarks />
        public const string Structure_E_Type = "Structure E Type";

        /// <remarks />
        public const string Structure_F = "Structure F";

        /// <remarks />
        public const string Structure_F_Type = "Structure F Type";

        /// <remarks />
        public const string Structure_G = "Structure G";

        /// <remarks />
        public const string Structure_G_Type = "Structure G Type";

        /// <remarks />
        public const string Structure_H = "Structure H";

        /// <remarks />
        public const string Structure_H_Type = "Structure H Type";

        /// <remarks />
        public const string Structure_J = "Structure J";

        /// <remarks />
        public const string Structure_J_Type = "Structure J Type";

        /// <remarks />
        public const string Structure_K = "Structure K";

        /// <remarks />
        public const string Structure_K_Type = "Structure K Type";

        /// <remarks />
        public const string Structure_L = "Structure L";

        /// <remarks />
        public const string Structure_L_Type = "Structure L Type";

        /// <remarks />
        public const string Structure_M = "Structure M";

        /// <remarks />
        public const string Structure_M_Type = "Structure M Type";

        /// <remarks />
        public const string Structure_N = "Structure N";

        /// <remarks />
        public const string Structure_N_Type = "Structure N Type";

        /// <remarks />
        public const string Structure_O = "Structure O";

        /// <remarks />
        public const string Structure_O_Type = "Structure O Type";

        /// <remarks />
        public const string Structure_P = "Structure P";

        /// <remarks />
        public const string Structure_P_Type = "Structure P Type";

        /// <remarks />
        public const string Structure_Q = "Structure Q";

        /// <remarks />
        public const string Structure_Q_Type = "Structure Q Type";

        /// <remarks />
        public const string Structure_R = "Structure R";

        /// <remarks />
        public const string Structure_R__Type = "Structure R- Type";

        /// <remarks />
        public const string Structure_S = "Structure S";

        /// <remarks />
        public const string Structure_S_Type = "Structure S Type";

        /// <remarks />
        public const string Structure_T = "Structure T";

        /// <remarks />
        public const string Structure_T_Type = "Structure T Type";

        /// <remarks />
        public const string Structures = "Structures";

        /// <remarks />
        public const string TwoDimArray = "TwoDimArray";
    }
    #endregion

    #region Namespace Declarations
    /// <remarks />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the StructuresWithArrays namespace (.NET code namespace is 'StructuresWithArrays').
        /// </summary>
        public const string StructuresWithArrays = "http://test.org/UA/StructuresWithArrays/";

        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";
    }
    #endregion
}