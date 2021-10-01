using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Domain
{
    public class PrimaryTenants
    {
        /// <example>
        ///     793dd4ca-d7c4-4110-a8ff-c58eac4b90fa
        /// </example>
        [NonEmptyGuid]
        public Guid Id { get; set; }
        /// <example>
        ///     Smith Johnson
        /// </example>
        [Required]
        public string FullName { get; set; }
    }
}
