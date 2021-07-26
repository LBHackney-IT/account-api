using AccountApi.V1.Domain;
using System;

namespace AccountsApi.V1.Domain
{
    //TODO: rename this class to be the domain object which this API will getting. e.g. Residents or Claimants
    public class Account
    {
        public Guid Id { get; set; }
        public TargetType TargetType { get; set; }
        public Guid TargetId { get; set; }
        public AccountType AccountType { get; set; }
        public RentGroupType RentGroupType { get; set; }
        public string AgreementType { get; set; }
        public decimal AccountBalance { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public ConsolidatedCharges ConsolidatedCharges { get; set; }
        public Tenure Tenure { get; set; }
    }
}
