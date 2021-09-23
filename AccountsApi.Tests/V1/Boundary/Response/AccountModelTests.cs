using System;
using System.Collections.Generic;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Boundary.Response
{
    public class AccountModelTests
    {
        private readonly Fixture _fixture;
        public AccountModelTests()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void AccountModelHasPropertiesSet()
        {
            #region AccountModel
            var model = typeof(AccountResponse);
            model.GetProperties().Length.Should().Be(19);

            AccountResponse account = _fixture.Create<AccountResponse>();

            Assert.IsType<Guid>(account.Id);
            Assert.IsType<AccountStatus>(account.AccountStatus);
            Assert.IsType<AccountType>(account.AccountType);
            Assert.IsType<string>(account.AgreementType);
            Assert.IsType<string>(account.CreatedBy);
            Assert.IsType<DateTime>(account.CreatedDate);
            Assert.IsType<DateTime>(account.EndDate);
            Assert.IsType<string>(account.LastUpdatedBy);
            Assert.IsType<DateTime>(account.LastUpdatedDate);
            Assert.IsType<Guid>(account.ParentAccount);
            Assert.IsType<string>(account.PaymentReference);
            Assert.IsType<RentGroupType>(account.RentGroupType);
            Assert.IsType<DateTime>(account.StartDate);
            Assert.IsType<Guid>(account.TargetId);
            Assert.IsType<TargetType>(account.TargetType);
            Assert.IsAssignableFrom<IEnumerable<ConsolidatedCharge>>(account.ConsolidatedCharges);
            Assert.IsType<Tenure>(account.Tenure);
            Assert.IsType<decimal>(account.TotalBalance);
            #endregion

            #region ConsolidatedCharge
            var consolidatedChargeEntity = typeof(ConsolidatedCharge);
            consolidatedChargeEntity.GetProperties().Length.Should().Be(3);

            ConsolidatedCharge consolidatedCharge = _fixture.Create<ConsolidatedCharge>();

            Assert.IsType<decimal>(consolidatedCharge.Amount);
            Assert.IsType<string>(consolidatedCharge.Frequency);
            Assert.IsType<string>(consolidatedCharge.Type);
            #endregion

            #region Tenure
            var entityTenure = typeof(Tenure);
            entityTenure.GetProperties().Length.Should().Be(4);

            Tenure tenure = _fixture.Create<Tenure>();
            Assert.IsType<string>(tenure.FullAddress);
            Assert.IsType<string>(tenure.TenancyId);
            Assert.IsType<string>(tenure.TenancyType);
            Assert.IsAssignableFrom<IEnumerable<PrimaryTenant>>(tenure.PrimaryTenants);
            #endregion

            #region PrimaryTenant
            var entityPrimaryTenant = typeof(PrimaryTenant);
            entityPrimaryTenant.GetProperties().Length.Should().Be(1);

            PrimaryTenant primaryTenant = _fixture.Create<PrimaryTenant>();
            Assert.IsType<string>(primaryTenant.FullName);
            #endregion
        }
    }
}
