using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        public Title PreferredTitle { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredSurname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
