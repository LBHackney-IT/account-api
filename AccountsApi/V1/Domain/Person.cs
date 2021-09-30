using System;
using System.ComponentModel.DataAnnotations;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Domain
{
    public class Person
    {
        [NonEmptyGuid]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
