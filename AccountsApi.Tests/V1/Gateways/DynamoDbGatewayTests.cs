using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Xunit;

namespace AccountsApi.Tests.V1.Gateways
{
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly Mock<DynamoDbContextWrapper> _wrapper;
        private readonly DynamoDbGateway _gateway;

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _wrapper = new Mock<DynamoDbContextWrapper>();
            var amazonDynamoDb = new Mock<IAmazonDynamoDB>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _wrapper.Object,amazonDynamoDb.Object);
        }

        [Fact]
        public async Task GetById_DbReturnsNull_ReturnsNull()
        {
            var response = await _gateway.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

            response.Should().BeNull();
        }

        [Fact]
        public async Task GetById_DbReturnsEntity_ReturnsTheEntity()
        {
            var id = Guid.NewGuid();
            var dbEntity = _fixture.Create<AccountDbEntity>();

            dbEntity.Id = id;

            _dynamoDb.Setup(x => x.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), default))
                     .ReturnsAsync(dbEntity);

            var response = await _gateway.GetByIdAsync(id).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), default), Times.Once);

            dbEntity.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetAll_DbReturnsZeroItems_ReturnsEmptyList()
        {
            _wrapper.Setup(x => x.ScanAsync(
               It.IsAny<IDynamoDBContext>(),
               It.IsAny<IEnumerable<ScanCondition>>(),
               It.IsAny<DynamoDBOperationConfig>()))
               .ReturnsAsync(new List<AccountDbEntity>() { });

            var result = await _gateway.GetAllAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAll_DbReturnsItems_ReturnsList()
        {
            var dbResponse = new List<AccountDbEntity>()
            {
                new AccountDbEntity()
                {
                    Id = new Guid("b3b91924-1a3d-44b7-b38a-ae4ae5e57b69"),
                    ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fh5r3"),
                    PaymentReference = "123234345",
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = -127.1M,
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
                    ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fh5r3"),
                    PaymentReference = "123234345",
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = -5.6M,
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

            var result = await _gateway.GetAllAsync(new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"), AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().HaveCount(2);

            result[0].Should().BeEquivalentTo(dbResponse[0]);

            result[1].Should().BeEquivalentTo(dbResponse[1]);
        }

        [Fact]
        public async Task Add_WithValidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<Account>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task Add_WithIlvalidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = (Account) null;

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task Update_WithValidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<Account>();

            await _gateway.UpdateAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task Update_WithIlvalidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = (Account) null;

            await _gateway.UpdateAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
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
                    ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fh5r3"),
                    PaymentReference = "123234345",
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = -127.1M,
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
                    ParentAccount = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fh5r3"),
                    PaymentReference = "123234345",
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = -5.6M,
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
