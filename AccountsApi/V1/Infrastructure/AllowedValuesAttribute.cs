using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AccountsApi.V1.Domain;

namespace AccountApi.V1.Infrastructure
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly List<TargetType> _allowedTargetTypeEnumItems;
        private readonly List<AccountType> _allowedAccountTypeEnumItems;
        private readonly List<AccountStatus> _allowedAccountStatusEnumItems;
        private readonly List<Direction> _allowedDirectionEnumItems;

        public AllowedValuesAttribute(params TargetType[] allowedEnumItems)
        {
            _allowedTargetTypeEnumItems = allowedEnumItems.ToList();
        }

        public AllowedValuesAttribute(params AccountType[] allowedEnumItems)
        {
            _allowedAccountTypeEnumItems = allowedEnumItems.ToList();
        }
        public AllowedValuesAttribute(params AccountStatus[] allowedEnumItems)
        {
            _allowedAccountStatusEnumItems = allowedEnumItems.ToList();
        }

        public AllowedValuesAttribute(params Direction[] allowedEnumItems)
        {
            _allowedDirectionEnumItems = allowedEnumItems.ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.MemberName} is required.");
            }

            var valueType = value.GetType();

            if (value.GetType() == typeof(TargetType))
            {
                if (!valueType.IsEnum || !Enum.IsDefined(typeof(TargetType), value))
                {
                    return new ValidationResult($"{validationContext.MemberName} should be a type of TargetType enum.");
                }

                var isValid = _allowedTargetTypeEnumItems.Contains((TargetType) value);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName} should be in a range: [{string.Join(", ", _allowedTargetTypeEnumItems.Select(a => $"{(int) a}({a})"))}].");
                }
            }
            else if (value.GetType() == typeof(AccountType))
            {

                if (!valueType.IsEnum || !Enum.IsDefined(typeof(AccountType), value))
                {
                    return new ValidationResult($"{validationContext.MemberName} should be a type of AccountType enum.");
                }

                var isValid = _allowedAccountTypeEnumItems.Contains((AccountType) value);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName} should be in a range: [{string.Join(", ", _allowedAccountTypeEnumItems.Select(a => $"{(int) a}({a})"))}].");
                }
            }
            else if (value.GetType() == typeof(AccountStatus))
            {

                if (!valueType.IsEnum || !Enum.IsDefined(typeof(AccountStatus), value))
                {
                    return new ValidationResult($"{validationContext.MemberName} should be a type of AccountStatus enum.");
                }

                var isValid = _allowedAccountStatusEnumItems.Contains((AccountStatus) value);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName} should be in a range: [{string.Join(", ", _allowedAccountStatusEnumItems.Select(a => $"{(int) a}({a})"))}].");
                }
            }
            else if (value.GetType() == typeof(Direction))
            {

                if (!valueType.IsEnum || !Enum.IsDefined(typeof(Direction), value))
                {
                    return new ValidationResult($"{validationContext.MemberName} should be a type of Direction enum.");
                }

                var isValid = _allowedDirectionEnumItems.Contains((Direction) value);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName} should be in a range: [{string.Join(", ", _allowedDirectionEnumItems.Select(a => $"{(int) a}({a})"))}].");
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
