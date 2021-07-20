using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class InnerAccount
    {
        public Guid Id { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
