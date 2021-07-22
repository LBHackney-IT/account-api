using AccountApi.V1.Domain;
using AccountsApi.V1.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AccountApi.V1.Infrastructure;
using ChargeApi.V1.Infrastructure;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountRequestObject
    { 
        /// <summary>
        /// <example>
        ///     Estate
        /// </example>
        /// </summary>
        [AllowedValues(TargetType.Block,TargetType.Core,TargetType.Estate,TargetType.Flat)]
        public TargetType TargetType { get; set; }
        /// <summary>
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        /// </summary>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }
        /// <summary>
        /// <example>
        ///     Master
        /// </example>
        /// </summary>
        [AllowedValues(AccountType.Master,AccountType.Recharge,AccountType.Sundry)]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// <example>
        ///     MajorWorks
        /// </example>
        /// </summary>
        [Required]
        [NotNull]
        public RentGroupType RentGroupType { get; set; }
        /// <summary>
        /// <example>
        ///     test
        /// </example>
        /// </summary>
        [Required]
        [NotNull]
        public string AgreementType { get; set; }
        /// <summary>
        /// <example>
        ///     123.01
        /// </example>
        /// </summary>
        public decimal AccountBalance => 0;

        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        [RequiredDateTimeAttribute]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        [RequiredDateTimeAttribute]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        [RequiredDateTimeAttribute]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// <example>
        ///     Active
        /// </example>
        /// </summary>
        [Required]
        [NotNull]
        [AllowedValues(Domain.AccountStatus.Active,AccountStatus.Ended,AccountStatus.Suspended)]
        public AccountStatus AccountStatus { get; set; } 
    }
}
