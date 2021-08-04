using AccountsApi.V1.Domain;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private readonly Mock<IAccountApiGateway> _gateway;
        private readonly GetAllUseCase _getAllUseCase;

        public GetAllUseCaseTests()
        {
            _gateway = new Mock<IAccountApiGateway>();
            _getAllUseCase = new GetAllUseCase(_gateway.Object);
        }

        [Fact]
        public async Task GetAll_GatewayReturnsEmptyList_ReturnsEmptyList()
        {
            _gateway.Setup(_ => _.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ReturnsAsync(new List<Account>());

            var result = await _getAllUseCase.ExecuteAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.AccountResponseList.Should().NotBeNull();

            result.AccountResponseList.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAll_GatewayReturnsListWithAccounts_ReturnsListWithAccounts()
        {
            var gatewayResponse = new List<Account>()
            {
                new Account()
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
                },
                new Account()
                {
                    Id = new Guid("17b107e7-b7a1-4c14-a1c9-0630cebdf28e"),
                    TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                    StartDate = new DateTime(2021, 7, 1),
                    CreatedDate = new DateTime(2021, 6, 1),
                    EndDate = new DateTime(2021, 9, 1),
                    LastUpdatedDate = new DateTime(2021, 7, 1),
                    AccountBalance = 110.0M,
                    AccountStatus = AccountStatus.Suspended,
                    AccountType = AccountType.Sundry,
                    AgreementType = "string",
                    CreatedBy = "Admin",
                    RentGroupType = RentGroupType.GenFundRents,
                    LastUpdatedBy = "Admin",
                    TargetType = TargetType.Core
                }
            };

            _gateway.Setup(_ => _.GetAllAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ReturnsAsync(gatewayResponse);

            var result = await _getAllUseCase.ExecuteAsync(Guid.NewGuid(), AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.AccountResponseList.Should().NotBeNull();

            result.AccountResponseList.Should().HaveCount(2);
            result.AccountResponseList[0].Should().BeEquivalentTo(gatewayResponse[0]);
            result.AccountResponseList[1].Should().BeEquivalentTo(gatewayResponse[1]);
        }
    }
}
