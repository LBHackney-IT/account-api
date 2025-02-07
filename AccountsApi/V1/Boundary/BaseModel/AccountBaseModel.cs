using AccountsApi.V1.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AccountsApi.V1.Infrastructure;
using Newtonsoft.Json;
using AllowedValuesAttribute = AccountsApi.V1.Infrastructure.AllowedValuesAttribute;

namespace AccountsApi.V1.Boundary.BaseModel
{
    public abstract class AccountBaseModel
    {
        /// <summary>
        ///     Foreign reference number to attache to the the parent account.
        /// </summary>
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc435
        /// </example>
        [JsonProperty(Order = 5)]
        public Guid ParentAccountId { get; set; }

        /// <example>
        ///     123234345
        /// </example>
        [Required]
        [NotNull]
        [JsonProperty(Order = 4)]
        public string PaymentReference { get; set; }

        /// <example>
        ///     End reason code
        /// </example>
        public string EndReasonCode { get; set; }

        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        [NonEmptyGuid]
        [JsonProperty(Order = 2)]
        public Guid TargetId { get; set; }

        /// <example>
        ///     Estate
        /// </example>
        [AllowedValues(typeof(TargetType))]
        [Required]
        [JsonProperty(Order = 3)]
        public TargetType TargetType { get; set; }

        /// <example>
        ///     Master
        /// </example>
        [AllowedValues(typeof(AccountType))]
        [Required]
        [JsonProperty(Order = 6)]
        public AccountType AccountType { get; set; }

        /// <example>
        ///     MajorWorks
        /// </example>
        [AllowedValues(typeof(RentGroupType))]
        [Required]
        [JsonProperty(Order = 7)]
        public RentGroupType RentGroupType { get; set; }

        /// <example>
        ///     Master Account
        /// </example>
        [Required]
        [NotNull]
        [JsonProperty(Order = 8)]
        public string AgreementType { get; set; }

        /// <example>
        ///     Active
        /// </example>
        [Required]
        [AllowedValues(typeof(AccountStatus))]
        [JsonProperty(Order = 9)]
        public AccountStatus AccountStatus { get; set; }

        /// <example>
        ///     31245
        ///     Introductory
        ///     285 Avenue, 315 Amsterdam
        /// </example>
        [Required]
        [NotNull]
        public Tenure Tenure { get; set; }
    }
}
