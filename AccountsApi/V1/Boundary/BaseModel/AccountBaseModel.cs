using AccountsApi.V1.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AccountsApi.V1.Infrastructure;

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
        public Guid ParentAccountId { get; set; }

        /// <example>
        ///     123234345
        /// </example>
        [Required]
        [NotNull]
        public string PaymentReference { get; set; }

        /// <example>
        ///     End reason code
        /// </example>
        public string EndReasonCode { get; set; }

        /// <example>
        ///     Estate
        /// </example>
        [AllowedValues(typeof(TargetType))]
        [Required]
        public TargetType TargetType { get; set; }

        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }

        /// <example>
        ///     Master
        /// </example>
        [AllowedValues(typeof(AccountType))]
        [Required]
        public AccountType AccountType { get; set; }

        /// <example>
        ///     MajorWorks
        /// </example>
        [AllowedValues(typeof(RentGroupType))]
        [Required]
        public RentGroupType RentGroupType { get; set; }

        /// <example>
        ///     Master Account
        /// </example>
        [Required]
        [NotNull]
        public string AgreementType { get; set; }

        /// <example>
        ///     Active
        /// </example>
        [Required]
        [AllowedValues(typeof(AccountStatus))]
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
