using System;
using System.Collections.Generic;
using System.Linq;
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
            model.GetProperties().Length.Should().Be(20);

            AccountResponse account = _fixture.Create<AccountResponse>();

            Assert.IsType<Guid>(account.Id);
            Assert.IsType<AccountStatus>(account.AccountStatus);
            Assert.IsType<AccountType>(account.AccountType);
            Assert.IsType<string>(account.AgreementType);
            Assert.IsType<string>(account.CreatedBy);
            Assert.IsType<DateTime>(account.CreatedAt);
            Assert.IsType<DateTime>(account.EndDate);
            Assert.IsType<string>(account.LastUpdatedBy);
            Assert.IsType<DateTime>(account.LastUpdatedAt);
            Assert.IsType<Guid>(account.ParentAccountId);
            Assert.IsType<string>(account.PaymentReference);
            Assert.IsType<RentGroupType>(account.RentGroupType);
            Assert.IsType<DateTime>(account.StartDate);
            Assert.IsType<Guid>(account.TargetId);
            Assert.IsType<TargetType>(account.TargetType);
            Assert.IsAssignableFrom<IEnumerable<ConsolidatedCharge>>(account.ConsolidatedCharges);
            Assert.IsType<Tenure>(account.Tenure);
            Assert.IsType<decimal>(account.ConsolidatedBalance);
            Assert.IsType<string>(account.EndReasonCode);
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
            Assert.IsType<string>(tenure.TenureId);
            Assert.IsType<TenureType>(tenure.TenureType);
            Assert.IsAssignableFrom<IEnumerable<PrimaryTenants>>(tenure.PrimaryTenants);
            #endregion

            #region PrimaryTenant
            var entityPrimaryTenant = typeof(PrimaryTenants);
            entityPrimaryTenant.GetProperties().Length.Should().Be(2);

            PrimaryTenants primaryTenant = _fixture.Create<PrimaryTenants>();
            Assert.IsType<string>(primaryTenant.FullName);
            Assert.IsType<Guid>(primaryTenant.Id);
            #endregion
        }
    }
}
