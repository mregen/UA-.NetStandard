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
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// Stores information about engineering units.
    /// </summary>
    public partial class VariableAttributes
    {
        /// <summary>
        /// Initializes the object with the unitName and namespaceUri.
        /// </summary>
        public VariableAttributes(object value, byte accessLevel)
        {
            Initialize();

            Value = new Variant(value);
            AccessLevel = accessLevel;
            UserAccessLevel = accessLevel;
            MinimumSamplingInterval = MinimumSamplingIntervals.Indeterminate;
            Historizing = false;

            if (value == null)
            {
                DataType = DataTypeIds.BaseDataType;
                ValueRank = ValueRanks.Any;
            }
            else
            {
                DataType = TypeInfo.GetDataTypeId(value);
                ValueRank = TypeInfo.GetValueRank(value);
            }
        }
    }
}
