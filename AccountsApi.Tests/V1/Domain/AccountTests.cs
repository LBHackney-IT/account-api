using AccountsApi.V1.Domain;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AccountsApi.Tests.V1.Domain
{
    public class AccountTests
    {
        private readonly Fixture _fixture;
        public AccountTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void AccountHasPropertiesSet()
        {

            var entityType = typeof(Account);
            entityType.GetProperties().Length.Should().Be(20);
            Account account = _fixture.Create<Account>();

            #region Account
            account.AccountBalance.Should().BeOfType(typeof(decimal));
            Assert.IsType<Guid>(account.Id);
            Assert.IsType<string>(account.PaymentReference);
            Assert.IsType<TargetType>(account.TargetType);
            Assert.IsType<Guid>(account.TargetId);
            Assert.IsType<AccountType>(account.AccountType);
            Assert.IsType<RentGroupType>(account.RentGroupType);
            Assert.IsType<string>(account.AgreementType);
            Assert.IsType<decimal>(account.AccountBalance);
            Assert.IsType<string>(account.CreatedBy);
            Assert.IsType<string>(account.LastUpdatedBy);
            Assert.IsType<DateTime>(account.CreatedAt);
            Assert.IsType<DateTime>(account.LastUpdatedAt);
            Assert.IsType<DateTime>(account.StartDate);
            Assert.IsType<DateTime>(account.EndDate);
            Assert.IsType<AccountStatus>(account.AccountStatus);
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
