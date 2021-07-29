using System.Linq;
using AutoFixture;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.Infrastructure;
using AccountsApi.V1.UseCase;
using FluentAssertions;
using Moq;

namespace AccountsApi.Tests.V1.UseCase
{
    // todo
    public class GetAllUseCaseTests
    {
        private Mock<IAccountApiGateway> _mockGateway;
        private GetAllUseCase _classUnderTest;
        private Fixture _fixture;

        public GetAllUseCaseTests()
        {
            _mockGateway = new Mock<IAccountApiGateway>();
            _classUnderTest = new GetAllUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        // [Fact]
        // public void GetsAllFromTheGateway()
        // {
        //     var stubbedEntities = _fixture.CreateMany<AccountDbEntity>().ToList();
        //     _mockGateway.Setup(x => x.GetAll()).Returns(stubbedEntities);
        //
        //     var expectedResponse = new ResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };
        //
        //     _classUnderTest.Execute().Should().BeEquivalentTo(expectedResponse);
        // }

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
