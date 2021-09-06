using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.UseCase
{
    public class AddUseCaseTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly AddUseCase _addUseCase;

        public AddUseCaseTests()
        {
            _fixture = new Fixture();
            _gateway = new Mock<IAccountApiGateway>();
            _addUseCase = new AddUseCase(_gateway.Object);
        }

        [Fact]
        public async Task AddValidModelReturnsAccount()
        {
            // Arrange
            AccountRequest accountRequest = _fixture.Create<AccountRequest>();

            _gateway.Setup(z => z.AddAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);

            // Act
            var result = await _addUseCase.ExecuteAsync(accountRequest).ConfigureAwait(false);

            // Assert
            _gateway.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Exactly(1));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(accountRequest);
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void AddNullReturnsBadRequest()
        {
            // Arrange
            _gateway.Setup(x => x.AddAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);

            // Act
            Func<Task<AccountModel>> func = async () => await _addUseCase.ExecuteAsync(null).ConfigureAwait(false);

            // Assert
            _gateway.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Never);
            func.Should().Throw<ArgumentNullException>();

        }
    }
}
