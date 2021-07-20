using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class PrimaryTenant
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
