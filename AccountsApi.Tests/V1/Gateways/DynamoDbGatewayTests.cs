using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AccountsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-accounts-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IDynamoDBContext> _dynamoDb;
        private DynamoDbGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        }

        [Test]
        public async Task GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var response = await _classUnderTest.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(false);
            response.Should().BeNull();
        }

        [Test]
        public async Task GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<AccountDbEntity>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<AccountDbEntity>(entity.Id, default))
                     .ReturnsAsync(dbEntity);

            var response = await _classUnderTest.GetByIdAsync(entity.Id).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<AccountDbEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Id);
            entity.StartDate.Should().BeSameDateAs(response.StartDate);
        }
    }
}
