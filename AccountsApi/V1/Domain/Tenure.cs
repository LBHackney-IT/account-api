using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Tenure
    {
        /// <summary>
        /// <example>
        ///     31245
        /// </example>
        /// </summary>
        public string TenancyId { get; set; }
        /// <summary>
        /// <example>
        ///     Introductory
        /// </example>
        /// </summary>
        public string TenancyType { get; set; }
        public IEnumerable<PrimaryTenant> PrimaryTenants { get; set; }
    }
}
