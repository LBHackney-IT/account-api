using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AccountsApi.V1.Boundary.BaseModel
{
    public abstract class AccountResponseModel : AccountBaseModel
    {
        /// <example>
        ///     74c5fbc4-2fc8-40dc-896a-0cfa671fc832
        /// </example>
        public Guid Id { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        [Required]
        public string LastUpdatedBy { get; set; }

        /// <example>
        ///     123.01
        /// </example>
        public decimal AccountBalance { get; set; } = 0;

        /// <example>
        ///     278.05
        /// </example>
        public decimal ConsolidatedBalance { get; set; } = 0;

        [NotNull]
        public IEnumerable<ConsolidatedCharge> ConsolidatedCharges { get; set; }

    }
}
