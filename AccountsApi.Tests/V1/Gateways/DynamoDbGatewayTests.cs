using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.OpenApi.Writers;
using Moq.Protected;
using Xunit;

namespace AccountsApi.Tests.V1.Gateways
{
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly DynamoDbGateway _gateway;
        private readonly Mock<IAmazonDynamoDB> _amazonDynamoDb;

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _amazonDynamoDb = new Mock<IAmazonDynamoDB>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _amazonDynamoDb.Object);
        }

        #region GetById
        [Fact]
        public async Task GetById_DbReturnsNull_ReturnsNull()
        {
            _dynamoDb.Setup(_ => _.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AccountDbEntity) null);

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

            dbEntity.Should().NotBeNull();
            dbEntity.Id.Should().Be(id);
        }
        #endregion

        #region Add

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
        public async Task Add_WithInvalidModel_ThrowException()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _gateway.AddAsync((Account) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Never);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);
        }

        #endregion

        #region GetAll
        [Fact]
        public async Task GetAll_DbReturnsZeroItems_ReturnsEmptyList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var result = await _gateway.GetAllAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAll_DbReturnsItems_ReturnsList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _gateway.GetAllAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);
            _amazonDynamoDb.Verify(x => x.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response.ToAccounts());
        }

        [Fact]
        public async Task GetAll_DbReturnsItemsWithoutConsolidatedCharges_ReturnsList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponseWithoutConsolidatedCharges<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _gateway.GetAllAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);
            _amazonDynamoDb.Verify(x => x.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response.ToAccounts());
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_WithValidModel_WorksOnce()
        {
            var domain = _fixture.Create<Account>();

            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _dynamoDb.Setup(_ => _.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(domain.ToDatabase());

            await _gateway.UpdateAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
            var updatedDomain = await _gateway.GetByIdAsync(domain.Id).ConfigureAwait(false);
            updatedDomain.Should().NotBeNull();
            updatedDomain.Should().BeEquivalentTo(domain,
                opt => opt.Excluding(f => f.LastUpdatedAt));
            updatedDomain.LastUpdatedAt.Should().BeAfter(DateTime.Now.AddMinutes(-5));
        }

        [Fact]
        public async Task Update_WithInvalidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _gateway.UpdateAsync((Account) null).ConfigureAwait(false);

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Never);
        }
        #endregion

        #region GetAllArrears
        [Fact]
        public async Task GetAllArrears_DbReturnsZeroItems_ReturnsEmptyList()
        {
            _amazonDynamoDb.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var result = await _gateway.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task GetAllArrears_DbReturnsItems_ReturnsSortedList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _gateway.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response.ToAccounts());
            Assert.All(result, item => item.Should().NotBeNull());
        }
        #endregion

        #region Remove
        [Fact]
        public async Task RemoveAsync_WithValidEntry_WorksOnce()
        {
            _dynamoDb.Setup(x => x.DeleteAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            Account account = new Account();

            await _gateway.RemoveAsync(account).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.DeleteAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task RemoveAsync_WithInValidEntry_WorksOnce()
        {
            _dynamoDb.Setup(x => x.DeleteAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _gateway.RemoveAsync((Account) null).ConfigureAwait(false);

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);
            _dynamoDb.Verify(x => x.DeleteAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()), Times.Never());
        }
        #endregion
    }
}
