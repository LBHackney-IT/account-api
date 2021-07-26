using AccountApi.V1.Domain;
using AccountsApi.V1.Domain;
using System;

namespace AccountsApi.V1.Boundary.Response
{
    public class AccountResponseObject
    {
        /// <summary>
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// <example>
        ///     Estate
        /// </example>
        /// </summary>
        public TargetType TargetType { get; set; }
        /// <summary>
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        /// </summary>
        public Guid TargetId { get; set; }
        /// <summary>
        /// <example>
        ///     Master
        /// </example>
        /// </summary>
        public AccountType AccountType { get; set; }
        /// <summary>
        /// <example>
        ///     MajorWorks
        /// </example>
        /// </summary>
        public RentGroupType RentGroupType { get; set; }
        /// <summary>
        /// <example>
        ///     GHFHDHJGSDFG-FJHSGDJF-7634856-GJFJHDGJH
        /// </example>
        /// </summary>
        public string AgreementType { get; set; }
        /// <summary>
        /// <example>
        ///     123.01
        /// </example>
        /// </summary>
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// <example>
        ///     Active
        /// </example>
        /// </summary>
        public AccountStatus AccountStatus { get; set; }
        public ConsolidatedCharges ConsolidatedCharges { get; set; }
        public Tenure Tenure { get; set; }
    }
}
