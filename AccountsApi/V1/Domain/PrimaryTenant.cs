using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class PrimaryTenant
    {
        /// <summary>
        /// <example>
        ///     John Smith
        /// </example>
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// <example>
        ///     1980-05-05
        /// </example>
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
