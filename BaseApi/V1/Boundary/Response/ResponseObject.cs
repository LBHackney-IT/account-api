using AccountApi.V1.Domain;
using System;

namespace AccountApi.V1.Boundary.Response
{
    public class AccountResponseObject
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public decimal AccountBalance { get; set; }
        public string PaymentReference { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public decimal TotalCharged { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
