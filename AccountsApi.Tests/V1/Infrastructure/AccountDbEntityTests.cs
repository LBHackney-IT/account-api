using AccountsApi.V1.Domain;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Infrastructure;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Infrastructure
{

    public class AccountDbEntityTests
    {
        private readonly Fixture _fixture;
        public AccountDbEntityTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void AccountDbEntityHasPropertiesSet()
        {
            var entityType = typeof(AccountDbEntity);
            entityType.GetProperties().Length.Should().Be(19);

            AccountDbEntity entity = _fixture.Create<AccountDbEntity>();

            #region Account

            entity.AccountBalance.Should().BeOfType(typeof(decimal));
            Assert.IsType<Guid>(entity.Id);
            Assert.IsType<string>(entity.PaymentReference);
            Assert.IsType<TargetType>(entity.TargetType);
            Assert.IsType<Guid>(entity.TargetId);
            Assert.IsType<AccountType>(entity.AccountType);
            Assert.IsType<RentGroupType>(entity.RentGroupType);
            Assert.IsType<string>(entity.AgreementType);
            Assert.IsType<decimal>(entity.AccountBalance);
            Assert.IsType<string>(entity.CreatedBy);
            Assert.IsType<string>(entity.LastUpdatedBy);
            Assert.IsType<DateTime>(entity.CreatedAt);
            Assert.IsType<DateTime>(entity.LastUpdatedAt);
            Assert.IsType<DateTime>(entity.StartDate);
            Assert.IsType<DateTime>(entity.EndDate);
            Assert.IsType<AccountStatus>(entity.AccountStatus);
            Assert.IsAssignableFrom<IEnumerable<ConsolidatedCharge>>(entity.ConsolidatedCharges);
            Assert.IsType<Tenure>(entity.Tenure);
            Assert.IsType<decimal>(entity.ConsolidatedBalance);
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
