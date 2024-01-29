using System;
using System.Threading;
using System.Threading.Tasks;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AutoFixture;
using FluentAssertions;
using Hackney.Core.Testing.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AccountsApi.Tests.V1.Gateways
{
    [Collection("LogCall collection")]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly Mock<IAmazonDynamoDB> _amazonDynamoDb;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<ILogger<DynamoDbGateway>> _logger;
        private readonly DynamoDbGateway _classUnderTest;

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _amazonDynamoDb = new Mock<IAmazonDynamoDB>();
            _mockConfig = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<DynamoDbGateway>>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object, _amazonDynamoDb.Object, _mockConfig.Object, _logger.Object);
        }

        #region GetById
        [Fact]
        public async Task GetById_DbReturnsNull_ReturnsNull()
        {
            _dynamoDb.Setup(_ => _.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AccountDbEntity) null);
            var id = Guid.NewGuid();
            var response = await _classUnderTest.GetByIdAsync(id).ConfigureAwait(false);

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

            var response = await _classUnderTest.GetByIdAsync(id).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), default), Times.Once);

            dbEntity.Should().NotBeNull();
            dbEntity.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetById_DbCallIsLogged_WhenDbCallSucceeds()
        {
            var id = Guid.NewGuid();
            var dbEntity = _fixture.Create<AccountDbEntity>();
            _dynamoDb.Setup(x => x.LoadAsync<AccountDbEntity>(id, default))
                     .ReturnsAsync(dbEntity);

            await _classUnderTest.GetByIdAsync(id).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.LoadAsync for ID: {id}", Times.Once());
        }

        [Fact]
        public async Task GetById_DbCallIsLogged_WhenDbCallFails()
        {
            var id = Guid.NewGuid();
            _dynamoDb.Setup(x => x.LoadAsync<AccountDbEntity>(id, default))
                     .ThrowsAsync(new Exception("Test exception"));

            async Task Func() => await _classUnderTest.GetByIdAsync(id).ConfigureAwait(false);
            await Assert.ThrowsAsync<Exception>(Func).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.LoadAsync for ID: {id}", Times.Once());
        }
        #endregion

        #region Add

        [Fact]
        public async Task Add_WithValidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<Account>();

            await _classUnderTest.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task Add_WithInvalidModel_ThrowException()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _classUnderTest.AddAsync((Account) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Never);
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);
        }

        [Fact]
        public async Task Add_WithValidModel_LogsDbTransaction()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<Account>();
            await _classUnderTest.AddAsync(domain).ConfigureAwait(false);

            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.SaveAsync for account ID: {domain.Id}", Times.Once());
        }

        [Fact]
        public async Task Add_WithInvalidModel_DoesNotLogDbTranscation()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _classUnderTest.AddAsync((Account) null).ConfigureAwait(false);

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.SaveAsync for account ID: {It.IsAny<Guid>()}", Times.Never());
        }

        #endregion

        #region GetAll
        [Fact]
        public async Task GetAll_DbReturnsZeroItems_ReturnsEmptyList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var targetId = Guid.NewGuid();
            var accountType = AccountType.Master;

            var result = await _classUnderTest.GetAllAsync(targetId, accountType).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAll_DbReturnsItems_ReturnsList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var targetId = Guid.NewGuid();
            var accountType = AccountType.Master;

            var result = await _classUnderTest.GetAllAsync(targetId, accountType).ConfigureAwait(false);
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

            var targetId = Guid.NewGuid();
            var accountType = AccountType.Master;

            var result = await _classUnderTest.GetAllAsync(targetId, accountType).ConfigureAwait(false);
            _amazonDynamoDb.Verify(x => x.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response.ToAccounts());
        }

        [Fact]
        public async Task GetAll_DbCallIsLogged_WhenDbCallSucceeds()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();
            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var targetId = Guid.NewGuid();
            var accountType = AccountType.Master;

            await _classUnderTest.GetAllAsync(targetId, accountType).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _amazonDynamoDb.QueryAsync for targetId: {targetId} and accountType: {accountType}", Times.Once());
        }

        [Fact]
        public async Task GetAll_DbCallIsLogged_WhenDbCallFails()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var targetId = Guid.NewGuid();
            var accountType = AccountType.Master;

            async Task Func() => await _classUnderTest.GetAllAsync(targetId, accountType).ConfigureAwait(false);
            await Assert.ThrowsAsync<Exception>(Func).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _amazonDynamoDb.QueryAsync for targetId: {targetId} and accountType: {accountType}", Times.Once());
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

            await _classUnderTest.UpdateAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Once);
            var updatedDomain = await _classUnderTest.GetByIdAsync(domain.Id).ConfigureAwait(false);
            updatedDomain.Should().NotBeNull();
            updatedDomain.Should().BeEquivalentTo(domain,
                opt => opt.Excluding(f => f.LastUpdatedAt));
        }

        [Fact]
        public async Task Update_WithInvalidModel_WorksOnce()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _classUnderTest.UpdateAsync((Account) null).ConfigureAwait(false);

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), default), Times.Never);
        }

        [Fact]
        public async Task Update_WithValidModel_LogsDbTransaction()
        {
            var domain = _fixture.Create<Account>();

            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _dynamoDb.Setup(_ => _.LoadAsync<AccountDbEntity>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(domain.ToDatabase());

            await _classUnderTest.UpdateAsync(domain).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.SaveAsync for account ID: {domain.Id}", Times.Once());
        }

        [Fact]
        public async Task Update_WithInvalidModel_DoesNotLogDbTranscation()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AccountDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            async Task Func() => await _classUnderTest.UpdateAsync((Account) null).ConfigureAwait(false);

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(Func).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _dynamoDbContext.SaveAsync for account: {It.IsAny<Account>()}", Times.Never());
        }
        #endregion

        #region GetAllArrears
        [Fact]
        public async Task GetAllArrears_DbReturnsZeroItems_ReturnsEmptyList()
        {
            _amazonDynamoDb.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var result = await _classUnderTest.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task GetAllArrears_DbReturnsItems_ReturnsSortedList()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _classUnderTest.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response.ToAccounts());
            Assert.All(result, item => item.Should().NotBeNull());
        }

        [Fact]
        public async Task GetAllArrears_DbCallIsLogged_WhenDbCallSucceeds()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();
            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            await _classUnderTest.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _amazonDynamoDb.QueryAsync for accountType: {AccountType.Master}", Times.Once());
        }

        [Fact]
        public async Task GetAllArrears_DbCallIsLogged_WhenDbCallFails()
        {
            QueryResponse response = FakeDataHelper.MockQueryResponse<AccountResponse>();

            _amazonDynamoDb.Setup(p => p.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            async Task Func() => await _classUnderTest.GetAllArrearsAsync(AccountType.Master, "Field", Direction.Asc).ConfigureAwait(false);
            await Assert.ThrowsAsync<Exception>(Func).ConfigureAwait(false);
            _logger.VerifyExact(LogLevel.Debug, $"Calling _amazonDynamoDb.QueryAsync for accountType: {AccountType.Master}", Times.Once());
        }
        #endregion
    }
}
