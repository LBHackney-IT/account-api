using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AccountsApi.Tests.V1.Helper;
using AccountsApi.V1.Boundary;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Controllers;
using AccountsApi.V1.Domain;
using AccountsApi.V1.UseCase.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient.Server;
using Moq;
using Xunit;

namespace AccountsApi.Tests.V1.Controllers
{
    public class AccountsApiControllerTests
    {
        private readonly AccountApiController _sut;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _httpContext;

        private readonly Mock<IGetAllUseCase> _getAllUseCase;
        private readonly Mock<IGetByIdUseCase> _getByIdUseCase;
        private readonly Mock<IAddUseCase> _addUseCase;
        private readonly Mock<IUpdateUseCase> _updateUseCase;

        private readonly Fixture _fixture = new Fixture();

        #region GetAllAsync
        public AccountsApiControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();
            _getByIdUseCase = new Mock<IGetByIdUseCase>();
            _addUseCase = new Mock<IAddUseCase>();
            _updateUseCase = new Mock<IUpdateUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _sut = new AccountApiController(_getAllUseCase.Object, _getByIdUseCase.Object,
                _addUseCase.Object, _updateUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }
        [Fact]
        public async Task GetAllAsyncFoundReturnResponse()
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
                            LastUpdatedDate= new DateTime(2021,07,30),
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
                                FullAddress = "Hamilton Street 123 Alley 4.12",
                                TenancyId = "123"
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
                            LastUpdatedDate= new DateTime(2021,07,30),
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

            var result = await _sut.GetAllAsync(Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                AccountType.Master).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var accounts = okResult.Value as AccountResponses;

            accounts.Should().NotBeNull();

            accounts.AccountResponseList.Should().HaveCount(2);

            accounts.AccountResponseList[0].Id.Should().Be(Guid.Parse("82aa6932-e98d-41a1-a4d4-2b44135554f8"));
            accounts.AccountResponseList[0].TargetType.Should().Be(TargetType.Block);
            accounts.AccountResponseList[0].TargetId.Should().Be(Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"));
            accounts.AccountResponseList[0].AccountType.Should().Be(AccountType.Master);
            accounts.AccountResponseList[0].RentGroupType.Should().Be(RentGroupType.Garages);
            accounts.AccountResponseList[0].AgreementType.Should().Be("Agreement type 001");
            accounts.AccountResponseList[0].AccountBalance.Should().Be(125.23M);
            accounts.AccountResponseList[0].CreatedBy.Should().Be("Admin");
            accounts.AccountResponseList[0].LastUpdatedBy.Should().Be("Staff-001");
            accounts.AccountResponseList[0].CreatedDate.Should().Be(new DateTime(2021, 07, 30));
            accounts.AccountResponseList[0].LastUpdatedDate.Should().Be(new DateTime(2021, 07, 30));
            accounts.AccountResponseList[0].StartDate.Should().Be(new DateTime(2021, 07, 30));
            accounts.AccountResponseList[0].EndDate.Should().Be(new DateTime(2021, 07, 30));
            accounts.AccountResponseList[0].AccountStatus.Should().Be(AccountStatus.Active);

            accounts.AccountResponseList[0].ConsolidatedCharges.Should().HaveCount(2);
            accounts.AccountResponseList[0].ConsolidatedCharges.ToList()[1].Amount.Should().Be(123);
            accounts.AccountResponseList[0].ConsolidatedCharges.ToList()[1].Frequency.Should().Be("Weekly");
            accounts.AccountResponseList[0].ConsolidatedCharges.ToList()[1].Type.Should().Be("Elevator");

            accounts.AccountResponseList[0].Tenure.FullAddress.Should().Be("Hamilton Street 123 Alley 4.12");
            accounts.AccountResponseList[0].Tenure.TenancyType.Should().Be("INT");
            accounts.AccountResponseList[0].Tenure.TenancyId.Should().Be("123");
            accounts.AccountResponseList[0].Tenure.AssetId.Should().Be(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66a7af"));

        }

        [Theory]
        [MemberData(nameof(MockParametersForNotFound.GetTestData), MemberType = typeof(MockParametersForNotFound))]
        public async Task GetAllAsyncReturnsNotFoundReturnsNotFound(Guid id, AccountType accountType)
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
                .ThrowsAsync(new Exception("Mock Exception"));

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
                .ThrowsAsync(new Exception("Mock Exception"));

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
            AccountModel accountModel = _fixture.Create<AccountModel>();
            accountModel.Id = id;
            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(accountModel);

            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult) result).Value.Should().Be(accountModel);
        }

        [Fact]
        public async Task GetByIdAsyncNotFoundReturnsNotResponse()
        {
            Guid id = Guid.NewGuid();

            _getByIdUseCase.Setup(_ => _.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync((AccountModel) null);

            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            result.Should().BeOfType<NotFoundObjectResult>();
            ((NotFoundObjectResult) result).Value.Should().Be(id);
        }

        [Theory]
        [InlineData("b45d2bbf-abec-454c-a843-4667786177a1")]
        public void GetByIdAsyncExceptionReturnsException(Guid id)
        {
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
                .ReturnsAsync(() => new AccountModel());

            // Act
            var result = await _sut.GetByIdAsync(id).ConfigureAwait(false);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        } 
        #endregion

        [Fact]
        public async Task PostSuccessfullReturnsAccountModel()
        {
            // Arrange
            AccountRequest request = new AccountRequest
            {
                TargetType = TargetType.Block,
                TargetId = Guid.NewGuid(),
                AccountType = AccountType.Recharge,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement",
                CreatedBy = "Admin",
                LastUpdatedBy = "Admin",
                CreatedDate = new DateTime(2021, 8, 3),
                LastUpdatedDate = new DateTime(2021, 8, 3),
                StartDate = new DateTime(2021, 9, 1),
                EndDate = new DateTime(2022, 9, 1),
                AccountStatus = AccountStatus.Active
            };
            Guid id = Guid.NewGuid();
            AccountModel response = new AccountModel
            {
                Id = id,
                TargetType = TargetType.Block,
                TargetId = Guid.NewGuid(),
                AccountType = AccountType.Recharge,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement",
                CreatedBy = "Admin",
                LastUpdatedBy = "Admin",
                CreatedDate = new DateTime(2021, 8, 3),
                LastUpdatedDate = new DateTime(2021, 8, 3),
                StartDate = new DateTime(2021, 9, 1),
                EndDate = new DateTime(2022, 9, 1),
                AccountStatus = AccountStatus.Active,
                Tenure = null,
                ConsolidatedCharges = null,
                AccountBalance = 0
            };

            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AccountRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _sut.Post(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();

            AccountModel model = ((CreatedAtActionResult) result).Value as AccountModel;

            model.Should().NotBeNull();

            model.Id.Should().NotBeEmpty();
            model.ConsolidatedCharges.Should().BeNull();
            model.Tenure.Should().BeNull();
            model.TargetType.Should().Be(TargetType.Block);
            model.TargetId.Should().NotBeEmpty();
            model.AccountType.Should().Be(AccountType.Recharge);
            model.RentGroupType.Should().Be(RentGroupType.Garages);
            model.AgreementType.Should().Be("Agreement");
            model.CreatedBy.Should().Be("Admin");
            model.LastUpdatedBy.Should().Be(model.CreatedBy);
            model.CreatedDate.Should().Be(new DateTime(2021, 8, 3));
            model.LastUpdatedDate.Should().Be(new DateTime(2021, 8, 3));
            model.StartDate.Should().Be(new DateTime(2021, 9, 1));
            model.EndDate.Should().Be(new DateTime(2022, 9, 1));
            model.AccountStatus.Should().Be(AccountStatus.Active);
            model.AccountBalance.Should().Be(0);

        }

    }
}
