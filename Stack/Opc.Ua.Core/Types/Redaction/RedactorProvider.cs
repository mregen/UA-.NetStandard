// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace Opc.Ua.Redaction
{
    /// <summary>
    /// Provides redactors per data classification set.
    /// </summary>
    public class RedactorProvider : IRedactorProvider
    {
        private readonly IReadOnlyDictionary<DataClassificationSet, Redactor> m_classRedactors;
        private readonly Redactor m_fallbackRedactor;

        /// <summary>
        /// Constructor of the OPC UA specific redactor provider to use in services.
        /// </summary>
        public RedactorProvider(IEnumerable<Redactor> redactors, Dictionary<DataClassificationSet, Type> map, Type fallbackRedactor = null)
        {
            m_classRedactors = GetClassRedactorMap(redactors, map);
            m_fallbackRedactor = GetFallbackRedactor(redactors, fallbackRedactor);
        }

        /// <inheritdoc/>
        public Redactor GetRedactor(DataClassificationSet classifications)
        {
            if (m_classRedactors.TryGetValue(classifications, out var result))
            {
                return result;
            }

            return m_fallbackRedactor;
        }

        private static IReadOnlyDictionary<DataClassificationSet, Redactor> GetClassRedactorMap(IEnumerable<Redactor> redactors, Dictionary<DataClassificationSet, Type> map)
        {
            if (!map.ContainsKey(DataClassification.None))
            {
                map.Add(DataClassification.None, typeof(NullRedactor));
                redactors = [.. redactors, NullRedactor.Instance];
            }

            var dict = new Dictionary<DataClassificationSet, Redactor>(map.Count);
            foreach (var m in map)
            {
                foreach (var r in redactors)
                {
                    if (r.GetType() == m.Value)
                    {
                        dict[m.Key] = r;
                        break;
                    }
                }
            }

#if NET8_0_OR_GREATER
            return dict.ToFrozenDictionary();
#else
            return new ReadOnlyDictionary<DataClassificationSet, Redactor>(dict);
#endif
        }

        private static Redactor GetFallbackRedactor(IEnumerable<Redactor> redactors, Type defaultRedactorType)
        {
            if (defaultRedactorType == null)
            {
                return NullRedactor.Instance;
            }

            foreach (var r in redactors)
            {
                if (r.GetType() == defaultRedactorType)
                {
                    return r;
                }
            }

            throw new InvalidOperationException($"Couldn't find redactor of type {defaultRedactorType} in the container.");
        }
    }
}
