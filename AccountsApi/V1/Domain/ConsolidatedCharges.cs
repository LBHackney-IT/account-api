namespace AccountsApi.V1.Domain
{
    public class ConsolidatedCharge
    {
        /// <example>
        ///     Rent
        /// </example>
        public string Type { get; set; }

        /// <example>
        ///     Weekly
        /// </example>
        public string Frequency { get; set; }

        /// <example>
        ///     101.20
        /// </example>
        public decimal Amount { get; set; }
    }
}
