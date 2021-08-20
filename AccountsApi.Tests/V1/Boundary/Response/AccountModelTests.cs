using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AccountsApi.Tests.V1.Boundary.Response
{
    public class AccountModelTests
    {
        [Fact]
        public void AccountModelHasPropertiesSet()
        {
            AccountModel response = Constants.ConstructorAccountModelFromConstants();
            response.Id = Constants.ID;
            response.AccountStatus.Should().Be(Constants.ACCOUNTSTATUS);
            response.AccountType.Should().Be(Constants.ACCOUNTTYPE);
            response.AgreementType.Should().Be(Constants.AGREEMENTTYPE);
            response.CreatedBy.Should().Be(Constants.CREATEDBY);
            response.CreatedDate.Should().Be(Constants.CREATEDDATE);
            response.EndDate.Should().Be(Constants.ENDDATE);
            response.LastUpdatedBy.Should().Be(Constants.LASTUPDATEDBY);
            response.LastUpdatedDate.Should().Be(Constants.LASTUPDATEDDATE);
            response.RentGroupType.Should().Be(Constants.RENTGROUPTYPE);
            response.StartDate.Should().Be(Constants.STARTDATE);
            response.TargetId.Should().Be(Constants.TARGETID);
            response.TargetType.Should().Be(Constants.TARGETTYPE);
            response.ParentAccount.Should().Be(Constants.PARENTACCOUNT);
            response.PaymentReference.Should().Be(Constants.PAYMENTREFERENCE);
            response.ConsolidatedCharges.First().Should().Be(Constants.CONSOLIDATEDCHARGES);
            response.ConsolidatedCharges.ToList()[1].Should().Be(Constants.CONSOLIDATEDCHARGES.ToList()[1]);
        }
    }
}
