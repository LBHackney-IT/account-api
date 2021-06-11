using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class ConsolidatedCharges
    {
        public string Type { get; set; }
        public string Frequency { get; set; }
        public decimal Amount { get; set; }
    }
}
