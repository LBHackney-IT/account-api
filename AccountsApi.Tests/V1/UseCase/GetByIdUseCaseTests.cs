using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IAccountApiGateway> _gateway;
        private GetByIdUseCase _getByIdUseCase;

        public GetByIdUseCaseTests()
        {
            _gateway = new Mock<IAccountApiGateway>();
            _getByIdUseCase = new GetByIdUseCase(_gateway.Object);
        }

        [Fact]
        public async Task GetById_GatewayReturnsNull_ReturnsNull()
        {
            _gateway.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Account) null);

            var result = await _getByIdUseCase.ExecuteAsync(Guid.NewGuid()).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetById_GatewayReturnsAccount_ReturnsAccount()
        {
            var gatewayResponse = new Account()
            {
                Id = new Guid("b3b91924-1a3d-44b7-b38a-ae4ae5e57b69"),
                TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                StartDate = new DateTime(2021, 7, 1),
                CreatedAt = new DateTime(2021, 6, 1),
                EndDate = new DateTime(2021, 9, 1),
                LastUpdatedAt = new DateTime(2021, 7, 1),
                AccountBalance = 100.0M,
                AccountStatus = AccountStatus.Active,
                AccountType = AccountType.Master,
                AgreementType = "string",
                CreatedBy = "Admin",
                RentGroupType = RentGroupType.Garages,
                LastUpdatedBy = "Admin",
                TargetType = TargetType.Tenure
            };

            _gateway.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(gatewayResponse);

            var result = await _getByIdUseCase.ExecuteAsync(Guid.NewGuid()).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(gatewayResponse);
        }
    }
}
