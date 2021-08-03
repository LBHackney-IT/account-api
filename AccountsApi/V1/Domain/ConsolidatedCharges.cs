using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class ConsolidatedCharge
    {
        /// <example>
        ///     Rent
        /// </example>
        [NotNull]
        public string Type { get; set; }

        /// <example>
        ///     Weekly
        /// </example>
        [NotNull]
        public string Frequency { get; set; }

        /// <example>
        ///     101.20
        /// </example>
        [NotNull]
        public decimal Amount { get; set; }
    }
}
