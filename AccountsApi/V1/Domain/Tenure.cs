using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Tenure
    {
        public string TenancyId { get; set; }
        public int RentAccountNumber { get; set; }
        public DateTime StartOfTenureDate { get; set; }
        public DateTime EndOfTenureDate { get; set; }
        public TenancyType TenancyType { get; set; }
        public IEnumerable<PrimaryTenant> PrimaryTenants { get; set; }
    }
}
