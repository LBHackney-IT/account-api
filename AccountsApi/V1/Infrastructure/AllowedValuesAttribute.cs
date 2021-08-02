using System;
using System.ComponentModel.DataAnnotations;

namespace AccountApi.V1.Infrastructure
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly Type _type;

        public AllowedValuesAttribute(Type enumType)
        {
            _type = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueType = value.GetType();

            if (!valueType.IsEnum || !Enum.IsDefined(_type, value))
            {
                return new ValidationResult($"{validationContext.MemberName} should be a type of {_type} enum.");
            }

            return ValidationResult.Success;
        }
    }
}
