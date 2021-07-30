using System;
using System.ComponentModel.DataAnnotations;
using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Infrastructure
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value is Guid guid) && Guid.Empty == guid ?
                new ValidationResult("Guid cannot be empty or default.") : null;
        }
    }
}
