using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using AccountsApi.V1.Factories;
using Xunit;

namespace AccountsApi.Tests.V1.UseCase
{
    public class AddUseCaseTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly Mock<ISnsGateway> _snsGateway;
        private readonly ISnsFactory _snsFactory;
        private readonly AddUseCase _addUseCase;

        public AddUseCaseTests()
        {
            _gateway = new Mock<IAccountApiGateway>();
            _snsGateway = new Mock<ISnsGateway>();
            _snsFactory = new AccountSnsFactory();
            _addUseCase = new AddUseCase(_gateway.Object, _snsGateway.Object, _snsFactory);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
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
            Func<Task<AccountResponse>> func = async () => await _addUseCase.ExecuteAsync(null).ConfigureAwait(false);

            // Assert
            _gateway.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Never);
            func.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public async Task AddValidModelCallsSnsGateway()
        {
            // Arrange
            AccountRequest accountRequest = _fixture.Create<AccountRequest>();

            _gateway.Setup(z => z.AddAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);

            // Act
            var result = await _addUseCase.ExecuteAsync(accountRequest).ConfigureAwait(false);

            // Assert
            _snsGateway.Verify(x => x.Publish(It.IsAny<AccountSns>()), Times.Exactly(1));
        }
    }
}
