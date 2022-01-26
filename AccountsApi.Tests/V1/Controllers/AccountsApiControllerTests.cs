using AccountsApi.Tests.V1.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using AccountsApi.V1.UseCase.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using Microsoft.AspNetCore.JsonPatch;

namespace AccountsApi.Tests.V1.Controllers
{
    public class AccountsApiControllerTests
    {

        private readonly AccountApiController _sut;

        private readonly Mock<IGetAllUseCase> _getAllUseCase;
        private readonly Mock<IGetByIdUseCase> _getByIdUseCase;
        private readonly Mock<IAddUseCase> _addUseCase;
        private readonly Mock<IUpdateUseCase> _updateUseCase;
        private readonly Mock<IGetAllArrearsUseCase> _getAllArrearsUseCase;
        private readonly Mock<IAddBatchUseCase> _addBatchUseCase;
        private readonly Mock<IGetAccountByPrnUseCase> _getAccountByPrnUseCase;

        private readonly Fixture _fixture = new Fixture();

        public AccountsApiControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();
            _getByIdUseCase = new Mock<IGetByIdUseCase>();
            _addUseCase = new Mock<IAddUseCase>();
            _updateUseCase = new Mock<IUpdateUseCase>();
            _getAllArrearsUseCase = new Mock<IGetAllArrearsUseCase>();
            _addBatchUseCase = new Mock<IAddBatchUseCase>();
            _getAccountByPrnUseCase = new Mock<IGetAccountByPrnUseCase>();

            HttpContext httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext(new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor()));
            _sut = new AccountApiController(_getAllUseCase.Object, _getByIdUseCase.Object,
                _addUseCase.Object, _updateUseCase.Object,
                _getAllArrearsUseCase.Object, _addBatchUseCase.Object,
                _getAccountByPrnUseCase.Object)
            {
                ControllerContext = controllerContext
            };
        }


        #region GetAllAsync
        [Fact]
        public async Task GetAllAsyncFoundReturnResponse()
        {
            // Arrange
            Guid targetId = Guid.NewGuid();
            AccountType accountType = AccountType.Recharge;

            var accountModel1 = _fixture.Create<AccountResponse>();
            accountModel1.TargetId = targetId;
            accountModel1.AccountType = accountType;

            var accountModel2 = _fixture.Create<AccountResponse>();
            accountModel2.TargetId = targetId;
            accountModel2.AccountType = accountType;

            List<AccountResponse> accountModels = new List<AccountResponse>() { accountModel1, accountModel2 };
            AccountResponses accountResponses = new AccountResponses { AccountResponseList = accountModels };

            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(),
                    It.IsAny<AccountType>()))
                .ReturnsAsync(accountResponses);

            // Action
            var result = await _sut.GetAllAsync(targetId, AccountType.Recharge).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var accounts = okResult?.Value as AccountResponses;

            accounts.Should().NotBeNull();
            accounts?.AccountResponseList.Should().HaveCount(2);

            accounts.Should().BeEquivalentTo(accountResponses);

        }

        [Theory]
        [MemberData(nameof(MockParametersForNotFound.GetTestData), MemberType = typeof(MockParametersForNotFound))]
        public async Task GetAllAsyncNotFoundReturnsNotFound(Guid id, AccountType accountType)
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ReturnsAsync((AccountResponses) null);

            var response = await _sut.GetAllAsync(id, accountType).ConfigureAwait(false);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(NotFoundResult));
        }

        [Theory]
        [MemberData(nameof(MockParametersForFormatException.GetTestData), MemberType = typeof(MockParametersForFormatException))]
        public void GetAllAsyncExceptionReturnsFormatException(string gid, string accountType)
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ThrowsAsync(new FormatException());

            Func<Task<IActionResult>> getAllFunc =
                async () => await _sut.GetAllAsync(Guid.Parse(gid), Enum.Parse<AccountType>(accountType))
                    .ConfigureAwait(false);

            getAllFunc.Should().Throw<FormatException>();

        }

        [Theory]
        [MemberData(nameof(MockParametersForArgumentNullException.GetTestData), MemberType = typeof(MockParametersForArgumentNullException))]
        public void GetAllAsyncExceptionReturnsArgumentNullException(string s, string accountType)
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<AccountType>()))
                .ThrowsAsync(new Exception());

            Func<Task<IActionResult>> getAllFunc =
                async () => await _sut.GetAllAsync(Guid.Parse(s), Enum.Parse<AccountType>(accountType)).ConfigureAwait(false);

            getAllFunc.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsyncFoundReturnsResponse()
        {
            Guid id = Guid.NewGuid();
            AccountResponse accountModel = _fixture.Create<AccountResponse>();
            accountModel.Id = id;
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(accountModel);

            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult) result).Value.Should().BeEquivalentTo(accountModel);
        }

        [Fact]
        public async Task GetByIdAsyncNotFoundReturnsNotResponse()
        {
            Guid id = Guid.NewGuid();

            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync((AccountResponse) null);

            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            notFoundResult?.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            notFoundResult?.Value.Should().NotBeNull();

            var baseError = notFoundResult?.Value as BaseErrorResponse;

            baseError?.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            baseError?.Message.Should().BeEquivalentTo("The Account by provided id not found!");

            baseError?.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public void GetByIdAsyncExceptionReturnsException()
        {
            Guid id = Guid.NewGuid();
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>())).Throws(new Exception());

            Func<Task<IActionResult>> func = async () => await _sut.GetByIdAsync(id).ConfigureAwait(false);

            func.Should().Throw<Exception>();
        }

        [Fact]
        public async Task GetByIdAsyncEmptyIdReturnsBadRequest()
        {
            // Arrange
            Guid id = Guid.Empty;
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => new AccountResponse());

            // Act
            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        #endregion

        #region Post

        [Fact]
        public async Task PostWithoutTenureReturnsException()
        {
            AccountRequest request = _fixture.Build<AccountRequest>().Without(s => s.Tenure).Create();
            _addUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<AccountRequest>()))
                .ReturnsAsync(It.IsAny<AccountResponse>());

            Func<Task<IActionResult>> func = async () => await _sut.Post(request).ConfigureAwait(false);

            await func.Should().ThrowAsync<Exception>().ConfigureAwait(false);
        }

        [Fact]
        public async Task PostSuccessfullReturnsAccountModel()
        {
            // Arrange
            AccountRequest request = _fixture.Create<AccountRequest>();
            Guid id = Guid.NewGuid();
            AccountResponse response = _fixture.Create<AccountResponse>();
            response.Id = id;

            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AccountRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _sut.Post(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();

            AccountResponse model = (AccountResponse) ((CreatedAtActionResult) result).Value;

            model.Should().NotBeNull();

            model.Should().BeEquivalentTo(response);

        }
        #endregion

        #region Update

        [Fact]
        public async Task Update_UseCaseReturnsResult_Return200()
        {
            var guid = Guid.NewGuid();

            AccountResponse responseModel = new AccountResponse
            {
                Id = guid,
                TargetType = TargetType.Tenure,
                TargetId = Guid.NewGuid(),
                AccountType = AccountType.Recharge,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement",
                CreatedBy = "Admin",
                LastUpdatedBy = "Admin",
                CreatedAt = new DateTime(2021, 8, 3),
                LastUpdatedAt = new DateTime(2021, 8, 3),
                StartDate = new DateTime(2021, 9, 1),
                EndDate = new DateTime(2022, 9, 1),
                AccountStatus = AccountStatus.Active,
                Tenure = null,
                ConsolidatedCharges = null,
                AccountBalance = 0
            };

            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(responseModel);

            responseModel.AccountBalance = 120;
            responseModel.ConsolidatedBalance = 365;

            _updateUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<AccountResponse>()))
                .ReturnsAsync(responseModel);

            var patchDoc = new JsonPatchDocument<AccountUpdate>();
            patchDoc.Add(_ => _.AccountBalance, 120);
            patchDoc.Add(_ => _.ConsolidatedBalance, 365);

            var result = await _sut.Patch(guid, patchDoc).ConfigureAwait(false);

            result.Should().NotBeNull();

            var resultResponse = result as OkObjectResult;

            resultResponse.Should().NotBeNull();

            resultResponse.StatusCode.Should().Be((int) HttpStatusCode.OK);

            var responseValue = resultResponse.Value as AccountResponse;

            responseValue.Should().NotBeNull();

            responseValue.Should().BeEquivalentTo(responseModel);
        }

        [Fact]
        public async Task Update_InvalidId_Returns404()
        {
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(Guid.NewGuid()))
                .ReturnsAsync((AccountResponse) null);

            var patchDoc = new JsonPatchDocument<AccountUpdate>();
            patchDoc.Add(_ => _.AccountBalance, 120);

            var result = await _sut.Patch(Guid.NewGuid(), patchDoc).ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            notFoundResult.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            notFoundResult.Value.Should().NotBeNull();

            var baseError = notFoundResult.Value as BaseErrorResponse;

            baseError.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            baseError.Message.Should().BeEquivalentTo("The Account by provided id not found!");

            baseError.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task Update_InvalidModel_Returns400()
        {
            var result = await _sut.Patch(Guid.NewGuid(), null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as BadRequestObjectResult;

            notFoundResult.Should().NotBeNull();

            notFoundResult.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            notFoundResult.Value.Should().NotBeNull();

            var baseError = notFoundResult.Value as BaseErrorResponse;

            baseError.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            baseError.Message.Should().BeEquivalentTo("Account model cannot be null!");

            baseError.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task Update_UseCaseThrowException_Returns500()
        {
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Test exception"));

            async Task<IActionResult> Func() => await _sut.Patch(Guid.NewGuid(), new JsonPatchDocument<AccountUpdate>()).ConfigureAwait(false);

            Exception exception = await Assert.ThrowsAsync<Exception>((Func<Task<IActionResult>>) Func).ConfigureAwait(false);
            exception.Message.Should().BeEquivalentTo("Test exception");
        }

        #endregion

        #region Arrears

        [Fact]
        public async Task GetAllArrears_UseCaseReturnsList_Returns200()
        {
            var useCaseResponse = new AccountResponses()
            {
                AccountResponseList = new List<AccountResponse>()
                {
                    _fixture.Create<AccountResponse>(),
                    _fixture.Create<AccountResponse>(),
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

            var result = await _sut.GetArrears(request).ConfigureAwait(false);

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

            var result = await _sut.GetArrears(request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequest = result as BadRequestObjectResult;

            badRequest.Should().NotBeNull();

            badRequest.Value.Should().NotBeNull();

            var baseErrorResponse = badRequest.Value as BaseErrorResponse;

            baseErrorResponse.Should().NotBeNull();

            baseErrorResponse.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            baseErrorResponse.Message.Should().BeEquivalentTo("ArrearRequest can not be null");

            baseErrorResponse.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task GetAllArrears_UseCaseThrowException_Returns500()
        {
            var request = new ArrearRequest();

            _getAllArrearsUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<ArrearRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            async Task<IActionResult> Func() => await _sut.GetArrears(new ArrearRequest()).ConfigureAwait(false);
            Exception exception = await Assert.ThrowsAsync<Exception>((Func<Task<IActionResult>>) Func).ConfigureAwait(false);
            exception.Message.Should().Be("Test exception");
        }

        #endregion
    }
}
