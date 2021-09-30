using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Domain
{
    public class Person
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
