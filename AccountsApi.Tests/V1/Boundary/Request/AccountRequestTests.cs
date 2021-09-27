using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain;
using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

namespace AccountsApi.Tests.V1.Boundary.Request
{
    public class AccountRequestTests
    {

        private readonly Fixture _fixture;
        public AccountRequestTests()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void AccountRequestHasPropertiesSet()
        {
            var request = typeof(AccountRequest);
            request.GetProperties().Length.Should().Be(11);

            AccountRequest account = _fixture.Create<AccountRequest>();

            Assert.IsType<AccountStatus>(account.AccountStatus);
            Assert.IsType<AccountType>(account.AccountType);
            Assert.IsType<string>(account.AgreementType);
            Assert.IsType<string>(account.CreatedBy);
            Assert.IsType<DateTime>(account.EndDate);
            Assert.IsType<Guid>(account.ParentAccountId);
            Assert.IsType<string>(account.PaymentReference);
            Assert.IsType<RentGroupType>(account.RentGroupType);
            Assert.IsType<DateTime>(account.StartDate);
            Assert.IsType<Guid>(account.TargetId);
            Assert.IsType<TargetType>(account.TargetType);
        }
    }
}
