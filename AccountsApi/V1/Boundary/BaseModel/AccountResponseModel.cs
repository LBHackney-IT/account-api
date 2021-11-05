using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccountsApi.V1.Boundary.BaseModel
{
    public abstract class AccountResponseModel : AccountBaseModel
    {
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        [Required]
        [JsonProperty(Order = 14)]
        public string LastUpdatedBy { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        [JsonProperty(Order = 10)]
        public DateTime StartDate { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        [JsonProperty(Order = 11)]
        public DateTime? EndDate { get; set; }

        /// <example>
        ///     123.01
        /// </example>
        [JsonProperty(Order = 2)]
        public decimal AccountBalance { get; set; } = 0;

        /// <example>
        ///     278.05
        /// </example>
        [JsonProperty(Order = 13)]
        public decimal ConsolidatedBalance { get; set; } = 0;

        [NotNull]
        public IEnumerable<ConsolidatedCharge> ConsolidatedCharges { get; set; }

    }
}
