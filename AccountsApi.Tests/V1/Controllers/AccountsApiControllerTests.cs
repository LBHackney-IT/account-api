using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
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

        public AccountsApiControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();
            _getByIdUseCase = new Mock<IGetByIdUseCase>();
            _addUseCase = new Mock<IAddUseCase>();
            _updateUseCase = new Mock<IUpdateUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _accountApiController = new AccountApiController(_getAllUseCase.Object, _getByIdUseCase.Object,
                _addUseCase.Object, _updateUseCase.Object)
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
                            LastUpdated= new DateTime(2021,07,30),
                            StartDate= new DateTime(2021,07,30),
                            EndDate= new DateTime(2021,07,30),
                            AccountStatus = AccountStatus.Active,
                            ConsolidatedCharges = new List<ConsolidatedCharge>
                            {
                                new ConsolidatedCharge
                                {
                                    Amount = 125,Frequency = "Weekly",Type = "Water"
                                },
                                new ConsolidatedCharge
                                {
                                    Amount = 123,Frequency = "Weekly",Type = "Elevator"
                                }
                            },
                            Tenure =new Tenure
                            {
                                TenancyType = "INT",
                                AssetId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66a7af"),
                                FullAddress = "Hamilton Street 123 Alley 4.12",TenancyId = "123"
                            }
                        }
                    }
                });

            var result =await _accountApiController.GetAllAsync(Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

        }
    }
}
