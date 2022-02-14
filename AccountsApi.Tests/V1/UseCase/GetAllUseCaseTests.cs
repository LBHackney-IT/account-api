using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;
using Xunit;

namespace AccountsApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        /*private readonly Fixture _fixture;
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly GetByTargetIdUseCase _getAllUseCase;

        public GetAllUseCaseTests()
        {
            _fixture = new Fixture();
            _gateway = new Mock<IAccountApiGateway>();
            _getAllUseCase = new GetByTargetIdUseCase(_gateway.Object);
        }

        [Fact]
        public async Task ExecuteAsyncNoneExistIDReturnsEmpryAccountList()
        {
            // Arrange
            _gateway.Setup(_ => _.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ReturnsAsync(new List<Account>());

            // Act
            var result = await _getAllUseCase.ExecuteAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.AccountResponseList.Should().NotBeNull();
            result.AccountResponseList.Should().HaveCount(0);
            _gateway.Verify(x => x.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsyncWithValidParametersReturnsRealAccountList()
        {
            // Arrange
            var gatewayResponse = Enumerable.Range(0, 20)
                .Select(x => _fixture.Build<Account>().Create())
                .ToList();

            _gateway.Setup(_ => _.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ReturnsAsync(gatewayResponse);

            // Act
            var result = await _getAllUseCase.ExecuteAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();

            result.AccountResponseList.Should().NotBeNull();
            _gateway.Verify(p => p.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()), Times.Once);
            result.AccountResponseList.Should().HaveCount(20);
            result.AccountResponseList[0].Should().BeEquivalentTo(gatewayResponse[0]);
            result.AccountResponseList[1].Should().BeEquivalentTo(gatewayResponse[1]);
        }*/
    }
}
