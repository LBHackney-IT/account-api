using AccountsApi.V1.Domain;
using ChargeApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AccountApi.V1.Infrastructure;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountRequest
    {
        /// <example>
        ///     Estate
        /// </example>
        [AllowedValues(TargetType.Block, TargetType.Core, TargetType.Estate, TargetType.Flat)]
        public TargetType TargetType { get; set; }

        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }

        /// <example>
        ///     Master
        /// </example>
        ///[AllowedValues(AccountType.Master,AccountType.Recharge,AccountType.Sundry)]
        [Required]
        public AccountType AccountType { get; set; }

        /// <example>
        ///     MajorWorks
        /// </example>
        [Required]
        [NotNull]
        public RentGroupType RentGroupType { get; set; }

        /// <example>
        ///     test
        /// </example>
        [Required]
        public string AgreementType { get; set; }

        // TODO check this
        /// <example>
        ///     123.01
        /// </example>
        public decimal AccountBalance => 0;

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
        [AllowedValues(AccountStatus.Active, AccountStatus.Ended, AccountStatus.Suspended)]
        public AccountStatus AccountStatus { get; set; }
    }
}
