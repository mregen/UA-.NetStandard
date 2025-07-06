// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using Microsoft.Extensions.Compliance.Classification;

namespace Opc.Ua.Redaction
{
    /// <summary>
    /// OPC UA taxonomy classifactions.
    /// </summary>
    public static class TaxonomyClassifications
    {
        /// <summary>
        /// The name of this taxonomy.
        /// </summary>
        public static string Name => "OpcUaTaxonomy";

        /// <summary>
        /// Classifies personal user name information.
        /// </summary>
        public static DataClassification UserNameInformation => new DataClassification(Name, nameof(UserNameInformation));

        /// <summary>
        /// Classifies Uri information.
        /// </summary>
        public static DataClassification UriInformation => new DataClassification(Name, nameof(UriInformation));

        /// <summary>
        /// Classifies personal certificate data like a subject name. 
        /// </summary>
        public static DataClassification CertificateInformation => new DataClassification(Name, nameof(CertificateInformation));

        /// <summary>
        /// Classifies an endpoint with an url.
        /// </summary>
        public static DataClassification EndpointInformation => new DataClassification(Name, nameof(EndpointInformation));

        /// <summary>
        /// Classifies sensitive password information.
        /// </summary>
        public static DataClassification SensitiveInformation => new DataClassification(Name, nameof(SensitiveInformation));

        /// <summary>
        /// Classifies an exception with a call stack.
        /// </summary>
        public static DataClassification ExceptionInformation => new DataClassification(Name, nameof(ExceptionInformation));

        /// <summary>
        /// Set of personal classifactions.
        /// </summary>
        public static DataClassificationSet PersonalData => new DataClassificationSet(new DataClassification[] { UserNameInformation, UriInformation, CertificateInformation, EndpointInformation});

        /// <summary>
        /// Set of sensititve classifactions.
        /// </summary>
        public static DataClassificationSet SensitiveData => new DataClassificationSet(new DataClassification[] { SensitiveInformation });

        /// <summary>
        /// Set of personal classifactions.
        /// </summary>
        public static DataClassificationSet ProgramData => new DataClassificationSet(new DataClassification[] { ExceptionInformation });
    }

    /// <summary>
    /// Attribute to classify exception information.
    /// </summary>
    public sealed class ExceptionInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the exception information attribute.
        /// </summary>
        public ExceptionInformationAttribute()
            : base(TaxonomyClassifications.ExceptionInformation)
        {
        }
    }

    /// <summary>
    /// Attribute to classify certificate information.
    /// </summary>
    public sealed class CertificateInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the certificate information attribute.
        /// </summary>
        public CertificateInformationAttribute()
            : base(TaxonomyClassifications.CertificateInformation)
        {
        }
    }

    /// <summary>
    /// Attribute to classify endpoint information.
    /// </summary>
    public sealed class EndpointInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the endpoint information attribute.
        /// </summary>
        public EndpointInformationAttribute()
            : base(TaxonomyClassifications.EndpointInformation)
        {
        }
    }

    /// <summary>
    /// Attribute to classify user name information.
    /// </summary>
    public sealed class UserNameInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the user name information attribute.
        /// </summary>
        public UserNameInformationAttribute()
            : base(TaxonomyClassifications.UserNameInformation)
        {
        }
    }

    /// <summary>
    /// Attribute to classify secret information.
    /// </summary>
    public sealed class SecretInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the secret information attribute.
        /// </summary>
        public SecretInformationAttribute()
            : base(TaxonomyClassifications.SensitiveInformation)
        {
        }
    }

    /// <summary>
    /// Attribute to classify Uri information.
    /// </summary>
    public sealed class UriInformationAttribute : DataClassificationAttribute
    {
        /// <summary>
        /// The constructor of the Uri information attribute.
        /// </summary>
        public UriInformationAttribute()
            : base(TaxonomyClassifications.UriInformation)
        {
        }
    }
}
