// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

namespace Opc.Ua.Redaction
{
    /// <summary>
    /// OPC UA redactors.
    /// </summary>
    public static class Redactors
    {
        /// <summary>
        /// Redacts personal information, e.g. user name.
        /// </summary>
        public static Redactor PersonalRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Redacts Uri information.
        /// </summary>
        public static Redactor UriRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Redacts public certificate information. 
        /// </summary>
        public static Redactor CertificateRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Redacts OPC UA endpoint information.
        /// </summary>
        public static Redactor EndpointRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Redacts sensitive information, e.g. passwords.
        /// </summary>
        public static Redactor SensitiveRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Redacts program data, e.g. symbols or a call stack.
        /// </summary>
        public static Redactor ProgramRedactor { get; private set; } = NullRedactor.Instance;

        /// <summary>
        /// Provides a redactor per data classification set.
        /// </summary>
        public static IRedactorProvider RedactorProvider { get; private set; } = new RedactorProvider(
            new[] { NullRedactor.Instance },
            new Dictionary<DataClassificationSet, Type>()
            {
                { TaxonomyClassifications.PersonalData, typeof(NullRedactor) },
                { TaxonomyClassifications.SensitiveData, typeof(NullRedactor) },
                { TaxonomyClassifications.ProgramData, typeof(NullRedactor) }
            });
    }

}
