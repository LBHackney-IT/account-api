using AccountsApi.V1.Boundary.Request;
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
    public class AddUseCaseTests
    {
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly AddUseCase _addUseCase;

        public AddUseCaseTests()
        {
            _gateway = new Mock<IAccountApiGateway>();
            _addUseCase = new AddUseCase(_gateway.Object);
        }

        [Fact]
        public async Task Add_ValidModel_ReturnsAccount()
        {
            var assetModel = new AccountRequest()
            {
                TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                StartDate = new DateTime(2021, 7, 1),
                CreatedDate = new DateTime(2021, 6, 1),
                EndDate = new DateTime(2021, 9, 1),
                LastUpdatedDate = new DateTime(2021, 7, 1),
                AccountStatus = AccountStatus.Active,
                AccountType = AccountType.Master,
                AgreementType = "string",
                CreatedBy = "Admin",
                RentGroupType = RentGroupType.Garages,
                LastUpdatedBy = "Admin",
                TargetType = TargetType.Block
            };

            _gateway.Setup(_ => _.AddAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);

            var result = await _addUseCase.ExecuteAsync(assetModel).ConfigureAwait(false);

            _gateway.Verify(_ => _.AddAsync(It.IsAny<Account>()), Times.Once);

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(assetModel);
        }
    }
}
