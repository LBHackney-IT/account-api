using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccountsApi.V1.Domain
{
    public class Account
    {
        public Guid Id { get; set; }

        public TargetType TargetType { get; set; }

        public Guid TargetId { get; set; }
 
        public AccountType AccountType { get; set; }
 
        public RentGroupType RentGroupType { get; set; }

        public string AgreementType { get; set; }

        public decimal AccountBalance { get; set; } = 0;

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
 
        public AccountStatus AccountStatus { get; set; }

        public IEnumerable<ConsolidatedCharges> ConsolidatedCharges { get; set; }

        public Tenure Tenure { get; set; }
    }
}
