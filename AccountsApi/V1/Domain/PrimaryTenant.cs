using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class PrimaryTenants
    {
        /// <example>
        ///     793dd4ca-d7c4-4110-a8ff-c58eac4b90fa
        ///     Smith Johnson
        /// </example>
        public List<Person> Persons { get; set; }
    }
}
