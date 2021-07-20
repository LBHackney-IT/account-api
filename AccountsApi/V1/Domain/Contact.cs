using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Contact
    {
        public ContactType ContactType { get; set; }
        public string SubType { get; set; }
        public string Value { get; set; }
    }
}
