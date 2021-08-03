using AccountsApi.V1.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountRequest
    {
        /// <example>
        ///     Estate
        /// </example>
        [AllowedValues(typeof(TargetType))]
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
        public AccountType AccountType { get; set; }

        /// <example>
        ///     MajorWorks
        /// </example>
        [AllowedValues(typeof(RentGroupType))]
        public RentGroupType RentGroupType { get; set; }

        /// <example>
        ///     test
        /// </example>
        [Required]
        public string AgreementType { get; set; }

        /// <example>
        ///     123.01
        /// </example>
        public decimal AccountBalance { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        [Required]
        public string CreatedBy { get; set; }

        /// <example>
        ///     Staff002
        /// </example>
        [Required]
        public string LastUpdatedBy { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        [RequiredDateTime]
        public DateTime CreatedDate { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        [RequiredDateTime]
        public DateTime LastUpdatedDate { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        [RequiredDateTime]
        public DateTime StartDate { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        [RequiredDateTime]
        public DateTime EndDate { get; set; }

        /// <example>
        ///     Active
        /// </example>
        [Required]
        [AllowedValues(typeof(AccountStatus))]
        public AccountStatus AccountStatus { get; set; }
    }
}
