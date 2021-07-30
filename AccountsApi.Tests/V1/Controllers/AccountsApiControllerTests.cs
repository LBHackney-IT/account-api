using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;

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

        public async Task GetAllByTargetIdAndAccountTypeReturn200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(),
                    It.IsAny<AccountType>()))
                .ReturnsAsync(new AccountResponses
                {
                    AccountResponses = new List<AccountModel>
                    {
                        new AccountModel
                        {
                            
                        }
                    }
                });
        }
    }
}
