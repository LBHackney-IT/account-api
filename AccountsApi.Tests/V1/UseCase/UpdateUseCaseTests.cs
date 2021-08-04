using AccountsApi.V1.Boundary.Response;
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
    public class UpdateUseCaseTests
    {
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly UpdateUseCase _updateUseCase;

        public UpdateUseCaseTests()
        {
            _gateway = new Mock<IAccountApiGateway>();
            _updateUseCase = new UpdateUseCase(_gateway.Object);
        }

        [Fact]
        public async Task Update_ValidModel_ReturnsAccount()
        {
            var assetModel = new AccountModel()
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
            };

            _gateway.Setup(_ => _.UpdateAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);

            var result = await _updateUseCase.ExecuteAsync(assetModel).ConfigureAwait(false);

            _gateway.Verify(_ => _.UpdateAsync(It.IsAny<Account>()), Times.Once);

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(assetModel);
        }
    }
}
