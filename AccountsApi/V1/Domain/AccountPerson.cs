using System;
using System.Collections.Generic;

namespace AccountApi.V1.Domain
{
    public class AccountPerson
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public decimal WeeklyTotalCharges { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal RentCharges { get; set; } 
        public decimal TotalCharged { get; set; }
        public decimal HousingBenefit { get; set; }
        public decimal TotalPaid { get; set; }
        public ConsolidatedCharges ConsolidatedCharges { get; set; }
        public Tenure Tenure { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public Person Person { get; set; }
        public Asset Asset { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
    }
}
