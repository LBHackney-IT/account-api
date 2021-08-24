using System.Diagnostics.CodeAnalysis;

namespace AccountsApi.V1.Domain
{
    public class ConsolidatedCharges
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
