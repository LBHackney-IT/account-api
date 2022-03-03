using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var accountRequest = typeof(AccountRequest);
            accountRequest.GetProperties().Length.Should().Be(11);

            var tenureRequest = typeof(Tenure);
            tenureRequest.GetProperties().Length.Should().Be(4);

            var primaryTenantsRequest = typeof(PrimaryTenants);
            primaryTenantsRequest.GetProperties().Length.Should().Be(2);

            AccountRequest account = _fixture.Create<AccountRequest>();

            Assert.IsType<AccountStatus>(account.AccountStatus);
            Assert.IsType<AccountType>(account.AccountType);
            Assert.IsType<string>(account.AgreementType);
            Assert.IsType<string>(account.CreatedBy);
            Assert.IsType<Guid>(account.ParentAccountId);
            Assert.IsType<string>(account.PaymentReference);
            Assert.IsType<RentGroupType>(account.RentGroupType);
            Assert.IsType<Guid>(account.TargetId);
            Assert.IsType<TargetType>(account.TargetType);
            Assert.IsType<Tenure>(account.Tenure);
            Assert.IsType<string>(account.EndReasonCode);

            Assert.IsType<string>(account.Tenure.FullAddress);
            Assert.All(account.Tenure.PrimaryTenants, item => Assert.IsType<PrimaryTenants>(item));
            Assert.All(account.Tenure.PrimaryTenants, item => Assert.IsType<Guid>(item.Id));
            Assert.All(account.Tenure.PrimaryTenants, item => Assert.IsType<string>(item.FullName));

            Assert.IsType<TenureType>(account.Tenure.TenureType);
            Assert.IsType<string>(account.Tenure.TenureId);
        }
    }
}
