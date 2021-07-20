using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Summary
    {
        public Guid Id { get; set; }
        public string TargetId { get; set; }
        public string TargetType { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal TotalDwellingRent { get; set; }
        public decimal TotalNonDwellingRent { get; set; }
        public decimal TotalServiceCharges { get; set; }
        public decimal TotalRentalServiceCharges { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
