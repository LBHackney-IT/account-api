using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Tenure
    {
        /// <example>
        ///     31245
        /// </example>
        public string TenancyId { get; set; }

        /// <example>
        ///     Introductory
        /// </example>
        public string TenancyType { get; set; }

        /// <example>
        ///     285 Avenue, 315 Amsterdam
        /// </example>
        public string FullAddress { get; set; }

        /// <example>
        ///     3fa85f64-5717-4562-b3fc-2c963f66a7af
        /// </example>
        public Guid AssetId { get; set; }
    }
}
