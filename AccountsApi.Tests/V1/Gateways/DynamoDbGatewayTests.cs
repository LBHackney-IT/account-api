using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-accounts-api/wiki/Running-the-test-suite.

    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly Mock<DynamoDbContextWrapper> _wrapper;
        private readonly DynamoDbGateway _gateway;
 
        public   DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _wrapper = new Mock<DynamoDbContextWrapper>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _wrapper.Object);
        }

        [Fact]
        public async Task GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var response = await _gateway.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(false);
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<AccountDbEntity>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<AccountDbEntity>(entity.Id, default))
                     .ReturnsAsync(dbEntity);

            var response = await _gateway.GetByIdAsync(entity.Id).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<AccountDbEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Id);
            entity.StartDate.Should().BeSameDateAs(response.StartDate);
        }

        [Fact]
        public async Task GetAllArrears_DbReturnsZeroItems_ReturnsEmptyList()
        {
            _wrapper.Setup(x => x.ScanAsync(
               It.IsAny<IDynamoDBContext>(),
               It.IsAny<IEnumerable<ScanCondition>>(),
               It.IsAny<DynamoDBOperationConfig>()))
               .ReturnsAsync(new List<AccountDbEntity>() { });

            var result = await _gateway.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAllArrears_DbReturnsItems_ReturnsSortedList()
        {
            var dbResponse = new List<AccountDbEntity>()
            {
                new AccountDbEntity()
                {
                    Id = new Guid("b3b91924-1a3d-44b7-b38a-ae4ae5e57b69"),
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = 100.0M,
                    AccountStatus = AccountStatus.Active,
                    AccountType = AccountType.Master,
                    AgreementType = "string",
                    CreatedBy = "Admin",
                    RentGroupType = RentGroupType.Garages,
                    LastUpdatedBy = "Admin",
                    TargetType = TargetType.Block
                },
                new AccountDbEntity()
                {
                    Id = new Guid("42668c56-3a7e-4897-867c-0c39e8a328ea"),
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = 100.0M,
                    AccountStatus = AccountStatus.Active,
                    AccountType = AccountType.Master,
                    AgreementType = "1string",
                    CreatedBy = "Admin",
                    RentGroupType = RentGroupType.Garages,
                    LastUpdatedBy = "Admin",
                    TargetType = TargetType.Block
                }
            };

            _wrapper.Setup(x => x.ScanAsync(
               It.IsAny<IDynamoDBContext>(),
               It.IsAny<IEnumerable<ScanCondition>>(),
               It.IsAny<DynamoDBOperationConfig>()))
               .ReturnsAsync(dbResponse);

            var result = await _gateway.GetAllArrearsAsync(AccountType.Master, "AgreementType", Direction.Asc).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().HaveCount(2);

            result[0].Should().BeEquivalentTo(dbResponse[1]);

            result[1].Should().BeEquivalentTo(dbResponse[0]);
        }
    }
}
