using AccountApi.V1.Infrastructure;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.Controllers
{
    public class AccountApiControllerTests
    {
        private readonly AccountApiController _controller;
        private readonly Mock<IGetAllUseCase> _getAllUseCase;
        private readonly Mock<IUpdateUseCase> _updateUseCase;
        private readonly Mock<IAddUseCase> _addUseCase;
        private readonly Mock<IGetByIdUseCase> _getByIdUseCase;
        private readonly Mock<IGetAllArrearsUseCase> _getAllArrearsUseCase;

        public AccountApiControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();
            _updateUseCase = new Mock<IUpdateUseCase>();
            _addUseCase = new Mock<IAddUseCase>();
            _getByIdUseCase = new Mock<IGetByIdUseCase>();
            _getAllArrearsUseCase = new Mock<IGetAllArrearsUseCase>();
            _controller = new AccountApiController(_getAllUseCase.Object, _getByIdUseCase.Object, _addUseCase.Object, _updateUseCase.Object, _getAllArrearsUseCase.Object);
        }

        [Fact]
        public async Task GetAllArrears_UseCaseReturnsList_Returns200()
        {
            var useCaseResponse = new AccountResponses()
            {
                AccountResponseObjects = new List<AccountModel>()
                {
                    new AccountModel()
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
                    new AccountModel()
                    {
                        Id = new Guid("17b107e7-b7a1-4c14-a1c9-0630cebdf28e"),
                        TargetId = new Guid("2da59b6b-cdcb-46bd-ac61-1c10d1046285"),
                        StartDate = new DateTime(2021, 7, 1),
                        CreatedDate = new DateTime(2021, 6, 1),
                        EndDate = new DateTime(2021, 9, 1),
                        LastUpdatedDate = new DateTime(2021, 7, 1),
                        AccountBalance = 110.0M,
                        AccountStatus = AccountStatus.Suspended,
                        AccountType = AccountType.Master,
                        AgreementType = "string",
                        CreatedBy = "Admin",
                        RentGroupType = RentGroupType.GenFundRents,
                        LastUpdatedBy = "Admin",
                        TargetType = TargetType.Core
                    }
                }
            };

            var request = new ArrearRequest()
            {
                Type = AccountType.Master,
                SortBy = "AgreementType",
                Direction = Direction.Asc
            };

            _getAllArrearsUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<ArrearRequest>()))
                .ReturnsAsync(useCaseResponse);

            var result = await _controller.GetArrears(request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            okResult.Value.Should().NotBeNull();

            var response = okResult.Value as AccountResponses;

            response.Should().NotBeNull();

            response.AccountResponseObjects.Should().NotBeNull();

            response.AccountResponseObjects.Should().HaveCount(2);

            response.AccountResponseObjects[0].Should().BeEquivalentTo(useCaseResponse.AccountResponseObjects[0]);

            response.AccountResponseObjects[1].Should().BeEquivalentTo(useCaseResponse.AccountResponseObjects[1]);
            
        }

        [Fact]
        public async Task GetAllArrears_NullRequestModel_Returns400()
        {
            ArrearRequest request = null;

            var result = await _controller.GetArrears(request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequest = result as BadRequestObjectResult;

            badRequest.Should().NotBeNull();

            badRequest.Value.Should().NotBeNull();

            var baseErrorResponse = badRequest.Value as BaseErrorResponse;

            baseErrorResponse.Should().NotBeNull();

            baseErrorResponse.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            baseErrorResponse.Message.Should().BeEquivalentTo("ArrearRequest can't be null");

            baseErrorResponse.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task GetAllArrears_UseCaseThrowException_Returns500()
        {
            var request = new ArrearRequest();

            _getAllArrearsUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<ArrearRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _controller.GetArrears(new ArrearRequest()).ConfigureAwait(false);
                AssertExtensions.Fail();
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }
    }
}
