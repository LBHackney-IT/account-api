using AccountApi.V1.Domain;
using System;

namespace AccountApi.V1.Boundary.Response
{
    public class AccountResponseObject
    {
        /// <example>
        ///     793dd4ca-d7c4-4110-a8ff-c58eac4b90a7
        /// </example>
        public Guid Id { get; set; }
        /// <example>
        ///     793dd4ca-d7c4-4110-a8ff-c58eac4b90f8
        /// </example>
        public Guid TargetId { get; set; }
        /// <example>
        ///     Housing
        /// </example>
        public TargetType TargetType { get; set; }
        /// <example>
        ///     132.45
        /// </example>
        public decimal AccountBalance { get; set; }
        /// <example>
        ///     UI1212
        /// </example>
        public string PaymentReference { get; set; }
        /// <example>
        ///     2021-05-16
        /// </example>
        public DateTime LastUpdated { get; set; }
        /// <example>
        ///     2021-05-16
        /// </example>
        public DateTime StartDate { get; set; }
        /// <example>
        ///     2021-05-16
        /// </example>
        public DateTime EndDate { get; set; }
        /// <example>
        ///     Active
        /// </example>
        public AccountStatus AccountStatus { get; set; }
        /// <example>
        ///     123.123
        /// </example>
        public decimal TotalCharged { get; set; }
        /// <example>
        ///     456.46
        /// </example>
        public decimal TotalPaid { get; set; }
    }
}
