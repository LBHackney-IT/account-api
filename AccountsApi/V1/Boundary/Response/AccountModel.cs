using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;

namespace AccountsApi.V1.Boundary.Response
{
    public class AccountModel
    {
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        public Guid Id { get; set; }

        /// <example>
        ///     Estate
        /// </example>
        public TargetType TargetType { get; set; }

        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        public Guid TargetId { get; set; }

        /// <example>
        ///     Master
        /// </example>
        public AccountType AccountType { get; set; }

        /// <example>
        ///     MajorWorks
        /// </example>
        public RentGroupType RentGroupType { get; set; }

        /// <example>
        ///     GHFHDHJGSDFG-FJHSGDJF-7634856-GJFJHDGJH
        /// </example>
        public string AgreementType { get; set; }

        /// <example>
        ///     123.01
        /// </example>
        public decimal AccountBalance { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public string CreatedBy { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public string LastUpdatedBy { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public DateTime CreatedDate { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public DateTime LastUpdated { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public DateTime StartDate { get; set; }

        /// <example>
        ///     021-03-29T15:10:37.471Z
        /// </example>
        public DateTime EndDate { get; set; }

        /// <example>
        ///     Active
        /// </example>
        public AccountStatus AccountStatus { get; set; }

        public IEnumerable<ConsolidatedCharge> ConsolidatedCharges { get; set; }

        public Tenure Tenure { get; set; }
    }
}
