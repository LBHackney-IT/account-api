using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;

namespace AccountsApi.Tests.V1.Infrastructure
{
    public class AllowedValuesAttributeTests
    {
        private AccountsApi.V1.Infrastructure.AllowedValuesAttribute _allowedValues;

        public AllowedValuesAttributeTests()
        {

        }

        [Theory]
        [InlineData(AccountType.Recharge)]
        [InlineData(TargetType.Tenure)]
        public void IsValidEnumTypeEntryReturnsSuccess<T>(T enmType)
        {
            _allowedValues = new AccountsApi.V1.Infrastructure.AllowedValuesAttribute(typeof(T));
            _allowedValues.GetValidationResult(enmType, new ValidationContext(this)).Should().BeEquivalentTo(ValidationResult.Success);
        }

        [Theory]
        [InlineData(123)]
        [InlineData(null)]
        [InlineData("123")]
        [InlineData(true)]
        public void IsValidEnumTypeEntryReturnsThrowValidationException<T>(T data)
        {
            /*_allowedValues = new AllowedValuesAttribute(typeof(T));
            var result = _allowedValues.IsValid(data);*/


            _allowedValues = new AccountsApi.V1.Infrastructure.AllowedValuesAttribute(typeof(T));
            void Func() => _allowedValues.Validate(data, new ValidationContext(this));

            Assert.Throws<ValidationException>(Func);

        }
    }
}
