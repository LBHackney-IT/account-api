using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Request;
using FluentAssertions;
using Xunit;

namespace AccountsApi.Tests.V1.Boundary.Request
{
    public class AccountRequestTests
    {
        [Fact]
        public void AccountRequestHasPropertiesSet()
        {
            AccountRequest request = Constants.ConstructorAccountRequestFromConstants();

            request.AccountStatus.Should().Be(Constants.ACCOUNTSTATUS);
            request.AccountBalance.Should().Be(Constants.ACCOUNTBALANCE);
            request.AccountType.Should().Be(Constants.ACCOUNTTYPE);
            request.AgreementType.Should().Be(Constants.AGREEMENTTYPE);
            request.CreatedBy.Should().Be(Constants.CREATEDBY);
            request.CreatedDate.Should().Be(Constants.CREATEDDATE);
            request.EndDate.Should().Be(Constants.ENDDATE);
            request.LastUpdatedBy.Should().Be(Constants.LASTUPDATEDBY);
            request.LastUpdatedDate.Should().Be(Constants.LASTUPDATEDDATE);
            request.RentGroupType.Should().Be(Constants.RENTGROUPTYPE);
            request.StartDate.Should().Be(Constants.STARTDATE);
            request.TargetId.Should().Be(Constants.TARGETID);
            request.TargetType.Should().Be(Constants.TARGETTYPE);
        }
    }
}
