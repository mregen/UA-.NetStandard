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
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Opc.Ua
{
    /// <summary>
    /// A class that defines constants used by UA applications.
    /// </summary>
    public static partial class StatusCodes
    {
        #region Static Helper Functions
        /// <summary>
        /// Creates a dictionary of browse names for the status codes.
        /// </summary>
        private static readonly Lazy<FrozenDictionary<uint, string>> BrowseNames = new Lazy<FrozenDictionary<uint, string>>(CreateBrowseNamesDictionary);

        /// <summary>
        /// Creates a dictionary of Utf8 browse names for the status codes.
        /// </summary>
        private static readonly Lazy<FrozenDictionary<uint, byte[]>> Utf8BrowseNames = new Lazy<FrozenDictionary<uint, byte[]>>(CreateUtf8BrowseNamesDictionary);

        /// <summary>
		/// Returns the browse utf8BrowseName for the attribute.
		/// </summary>
        public static string GetBrowseName(uint identifier)
        {
            if (BrowseNames.Value.TryGetValue(identifier, out var browseName))
            {
                return browseName;
            }

            return string.Empty;
        }

        /// <summary>
		/// Returns the browse utf8BrowseName for the attribute.
		/// </summary>
        public static byte[] GetUtf8BrowseName(uint identifier)
        {
            if (Utf8BrowseNames.Value.TryGetValue(identifier, out var utf8BrowseName))
            {
                return utf8BrowseName;
            }

            return null;
        }

        /// <summary>
        /// Returns the browse names for all attributes.
        /// </summary>
        public static IReadOnlyCollection<string> GetBrowseNames()
        {
            return BrowseNames.Value.Values;
        }

        /// <summary>
        /// Returns the id for the attribute with the specified browse utf8BrowseName.
        /// </summary>
        public static uint GetIdentifier(string browseName)
        {
            foreach (var field in BrowseNames.Value)
            {
                if (field.Value == browseName)
                {
                    return field.Key;
                }
            }

            return 0;
        }
        #endregion

        #region Private Methods
        private static FrozenDictionary<uint, string> CreateBrowseNamesDictionary()
        {
            FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);

            var keyValuePairs = new Dictionary<uint, string>();
            foreach (FieldInfo field in fields)
            {
                keyValuePairs.Add((uint)field.GetValue(typeof(StatusCodes)), field.Name);
            }

            return keyValuePairs.ToFrozenDictionary();
        }

        private static FrozenDictionary<uint, byte[]> CreateUtf8BrowseNamesDictionary()
        {
            FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);

            var keyValuePairs = new Dictionary<uint, byte[]>();
            foreach (FieldInfo field in fields)
            {
                keyValuePairs.Add((uint)field.GetValue(typeof(StatusCodes)), Encoding.UTF8.GetBytes(field.Name));
            }

            return keyValuePairs.ToFrozenDictionary();
        }
        #endregion
    }
}
