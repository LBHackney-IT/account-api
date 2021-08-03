using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using AccountsApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.Controllers
{
    public class AccountsApiControllerTests
    {
        private readonly AccountApiController _accountApiController;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _httpContext;

        private readonly Mock<IGetAllUseCase> _getAllUseCase;
        private readonly Mock<IGetByIdUseCase> _getByIdUseCase;
        private readonly Mock<IAddUseCase> _addUseCase;
        private readonly Mock<IUpdateUseCase> _updateUseCase;
        private readonly Mock<IGetAllArrearsUseCase> _getAllArrearsUseCase;

        public AccountsApiControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();
            _getByIdUseCase = new Mock<IGetByIdUseCase>();
            _addUseCase = new Mock<IAddUseCase>();
            _updateUseCase = new Mock<IUpdateUseCase>();
            _getAllArrearsUseCase = new Mock<IGetAllArrearsUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _accountApiController = new AccountApiController(_getAllUseCase.Object, _getByIdUseCase.Object,
                _addUseCase.Object, _updateUseCase.Object, _getAllArrearsUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Fact]
        public async Task GetAllByTargetIdAndAccountTypeReturn200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(),
                    It.IsAny<AccountType>()))
                .ReturnsAsync(new AccountResponses
                {
                    AccountResponseList = new List<AccountModel>
                    {
                        new AccountModel
                        {
                            Id = Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"),
                            TargetType = TargetType.Block,
                            TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                            AccountType = AccountType.Master,
                            RentGroupType= RentGroupType.Garages,
                            AgreementType = "Agreement type 001",
                            AccountBalance = 125.23M,
                            CreatedBy = "Admin",
                            LastUpdatedBy = "Staff-001",
                            CreatedDate = new DateTime(2021,07,30),
                            LastUpdatedDate = new DateTime(2021,07,30),
                            StartDate= new DateTime(2021,07,30),
                            EndDate= new DateTime(2021,07,30),
                            AccountStatus = AccountStatus.Active,
                            ConsolidatedCharges = new List<ConsolidatedCharge>
                            {
                                new ConsolidatedCharge
                                {
                                    Amount = 125, Frequency = "Weekly", Type = "Water"
                                },
                                new ConsolidatedCharge
                                {
                                    Amount = 123, Frequency = "Weekly", Type = "Elevator"
                                }
                            },
                            Tenure =new Tenure
                            {
                                TenancyType = "INT",
                                AssetId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66a7af"),
                                FullAddress = "Hamilton Street 123 Alley 4.12", TenancyId = "123"
                            }
                        },
                        new AccountModel
                        {
                            Id = Guid.Parse("72aa6932-e98d-41a1-a4d4-2b44135554f7"),
                            TargetType = TargetType.Core,
                            TargetId = Guid.Parse("64c5fbc4-2fc8-40dc-896a-0cfa671fc831"),
                            AccountType = AccountType.Recharge,
                            RentGroupType= RentGroupType.GenFundRents,
                            AgreementType = "Agreement type 002",
                            AccountBalance = 225.23M,
                            CreatedBy = "002",
                            LastUpdatedBy = "Staff-001",
                            CreatedDate = new DateTime(2021,07,30),
                            LastUpdatedDate = new DateTime(2021,07,30),
                            StartDate = new DateTime(2021,07,30),
                            EndDate = new DateTime(2021,07,30),
                            AccountStatus = AccountStatus.Active,
                            ConsolidatedCharges = new List<ConsolidatedCharge>
                            {
                                new ConsolidatedCharge
                                {
                                    Amount = 125, Frequency = "Weekly", Type = "Water"
                                },
                                new ConsolidatedCharge
                                {
                                    Amount = 123, Frequency = "Weekly", Type = "Elevator"
                                }
                            },
                            Tenure =new Tenure
                            {
                                TenancyType = "INT",
                                AssetId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66a7af"),
                                FullAddress = "Hamilton Street 123 Alley 4.12", TenancyId = "123"
                            }
                        }
                    }
                });

            var result = await _accountApiController.GetAllAsync(Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var accounts = okResult.Value as AccountResponses;

            accounts.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllArrears_UseCaseReturnsList_Returns200()
        {
            var useCaseResponse = new AccountResponses()
            {
                AccountResponseList = new List<AccountModel>()
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

            var result = await _accountApiController.GetArrears(request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            okResult.Value.Should().NotBeNull();

            var response = okResult.Value as AccountResponses;

            response.Should().NotBeNull();

            response.AccountResponseList.Should().NotBeNull();

            response.AccountResponseList.Should().HaveCount(2);

            response.AccountResponseList[0].Should().BeEquivalentTo(useCaseResponse.AccountResponseList[0]);

            response.AccountResponseList[1].Should().BeEquivalentTo(useCaseResponse.AccountResponseList[1]);

        }

        [Fact]
        public async Task GetAllArrears_NullRequestModel_Returns400()
        {
            ArrearRequest request = null;

            var result = await _accountApiController.GetArrears(request).ConfigureAwait(false);

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
                var result = await _accountApiController.GetArrears(new ArrearRequest()).ConfigureAwait(false);
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
