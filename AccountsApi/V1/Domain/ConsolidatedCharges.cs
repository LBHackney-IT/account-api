using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class ConsolidatedCharges
    {
        /// <summary>
        /// <example>
        ///     Rent
        /// </example>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// <example>
        ///     Weekly
        /// </example>
        /// </summary>
        public string Frequency { get; set; }
        /// <summary>
        /// <example>
        ///     101.20
        /// </example>
        /// </summary>
        public decimal Amount { get; set; }
    }
}
