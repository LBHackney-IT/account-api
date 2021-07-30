using System.Linq;
using System.Reflection.Metadata;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Domain;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Domain
{
    public class AccountTests
    {
        [Fact]
        public void AccountHasPropertiesSet()
        {
            Account account = Constants.ConstructAccountFromConstants();
            account.Id.Should().Be(Constants.ID);
            account.ConsolidatedCharges.Should().ContainSingle();
            account.ConsolidatedCharges.First().Amount.Should().Be(Constants.AMOUNT);
            account.ConsolidatedCharges.First().Frequency.Should().Be(Constants.FREQUENCY);
            account.ConsolidatedCharges.First().Type.Should().Be(Constants.TYPE);
            account.LastUpdatedDate.Should().Be(Constants.LASTUPDATEDDATE);
            account.AccountBalance.Should().Be(Constants.ACCOUNTBALANCE);
            account.AccountStatus.Should().Be(Constants.ACCOUNTSTATUS);
            account.AccountType.Should().Be(Constants.ACCOUNTTYPE);
            account.TargetType.Should().Be(Constants.TARGETTYPE);
            account.TargetId.Should().Be(Constants.TARGETID);
            account.RentGroupType.Should().Be(Constants.RENTGROUPTYPE);
            account.AgreementType.Should().Be(Constants.AGREEMENTTYPE);
            account.CreatedBy.Should().Be(Constants.CREATEDBY);
            account.LastUpdatedBy.Should().Be(Constants.LASTUPDATEDBY);
            account.CreatedDate.Should().Be(Constants.CREATEDDATE);
            account.LastUpdatedDate.Should().Be(Constants.LASTUPDATEDDATE);
            account.StartDate.Should().Be(Constants.STARTDATE);
            account.EndDate.Should().Be(Constants.ENDDATE);
            account.Tenure.TenancyType.Should().Be(Constants.TENURE.TenancyType);
            account.Tenure.AssetId.Should().Be(Constants.TENURE.AssetId);
            account.Tenure.FullAddress.Should().Be(Constants.TENURE.FullAddress);
            account.Tenure.TenancyId.Should().Be(Constants.TENURE.TenancyId);
        }
    }
}
