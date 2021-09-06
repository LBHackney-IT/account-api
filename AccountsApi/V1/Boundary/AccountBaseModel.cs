using AccountsApi.V1.Domain;
using System;

namespace AccountsApi.V1.Boundary
{
    public abstract class AccountBaseModel
    {
        /// <summary>
        ///     Foreign reference number to attache to the the parent account.
        /// </summary>
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc435
        /// </example>
        public Guid ParentAccount { get; set; }

        /// <example>
        ///     123234345
        /// </example>
        public string PaymentReference { get; set; }

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
        ///     Master Account
        /// </example>
        public string AgreementType { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        public string CreatedBy { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        public string LastUpdatedBy { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime CreatedDate { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime LastUpdatedDate { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime StartDate { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime EndDate { get; set; }

        /// <example>
        ///     Active
        /// </example>
        public AccountStatus AccountStatus { get; set; }
    }
}
