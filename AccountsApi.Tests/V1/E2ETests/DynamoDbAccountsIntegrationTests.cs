using AccountsApi.V1.Boundary;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace AccountsApi.Tests.V1.E2ETests
{
    [Collection("MainCollection")]
    public class DynamoDbAccountsIntegrationTests : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Account ConstructAssetSummary()
        {
            var entity = new Account()
            {
                Id = Guid.NewGuid(),
                TargetType = TargetType.Block,
                TargetId = Guid.Parse("74c5fbc4-2fc8-40dc-896a-0cfa671fc832"),
                AccountType = AccountType.Master,
                RentGroupType = RentGroupType.Garages,
                AgreementType = "Agreement type 001",
                AccountBalance = _fixture.Create<decimal>(),
                CreatedBy = "Admin",
                LastUpdatedBy = "Staff-001",
                CreatedDate = new DateTime(2021, 07, 30),
                LastUpdatedDate = new DateTime(2021, 07, 30),
                StartDate = new DateTime(2021, 07, 30),
                EndDate = new DateTime(2021, 07, 30),
                AccountStatus = AccountStatus.Active,
                ConsolidatedCharges = new List<ConsolidatedCharges>
                {
                    new ConsolidatedCharges
                    {
                        Amount = 125, Frequency = "Weekly", Type = "Water"
                    },
                    new ConsolidatedCharges
                    {
                        Amount = 123, Frequency = "Weekly", Type = "Elevator"
                    }
                },
                Tenure = new Tenure
                {
                    TenancyType = "INT",
                    AssetId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66a7af"),
                    FullAddress = "Hamilton Street 123 Alley 4.12",
                    TenancyId = "123"
                }
            };

            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(Account entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AccountDbEntity>(entity.Id).ConfigureAwait(false));
        }

        [Fact]
        public async Task GetAccountById_WithInvalidId_Returns404()
        {
            Guid id = Guid.NewGuid();

            var uri = new Uri($"api/v1/accounts/{id}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeEquivalentTo("The Account by provided id not found!");
            apiEntity.StatusCode.Should().Be(404);
            apiEntity.Details.Should().BeEquivalentTo(string.Empty);
        }

        [Fact]
        public async Task HealchCheck_Returns200()
        {
            var uri = new Uri($"api/v1/healthcheck/ping", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<HealthCheckResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeNull();
            apiEntity.Success.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAccount_WithValidModel_Returns201()
        {
            var accountDomain = ConstructAssetSummary();

            await CreateAccountAndValidateResponse(accountDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateAccountAndThenGetById_Returns201And200()
        {
            var accountDomain = ConstructAssetSummary();

            var guid = await CreateAccountAndValidateResponse(accountDomain).ConfigureAwait(false);

            accountDomain.Id = guid;

            await GetAccountByIdAndValidateResponse(accountDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateTwoAccountsAndGetAll_Returns201AndReturns200()
        {
            var accountDomain = new[] { ConstructAssetSummary(), ConstructAssetSummary() };

            var guids = new List<Guid>();

            foreach (var acc in accountDomain)
            {
                var guid = await CreateAccountAndValidateResponse(acc).ConfigureAwait(false);
                guids.Add(guid);
            }

            var uri = new Uri($"api/v1/accounts?targetId=74c5fbc4-2fc8-40dc-896a-0cfa671fc832&accountType=Master", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AccountResponses>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.AccountResponseList.Count.Should().BeGreaterOrEqualTo(2);

            var firstAsset = apiEntity.AccountResponseList.Find(a => a.Id == guids[0]);
            var secondAsset = apiEntity.AccountResponseList.Find(a => a.Id == guids[1]);

            firstAsset.Should().BeEquivalentTo(accountDomain[0], opt => opt.Excluding(x => x.ConsolidatedCharges).Excluding(x => x.AccountBalance).Excluding(x => x.Tenure).Excluding(x => x.Id));
            secondAsset.Should().BeEquivalentTo(accountDomain[1], opt => opt.Excluding(x => x.ConsolidatedCharges).Excluding(x => x.AccountBalance).Excluding(x => x.Tenure).Excluding(x => x.Id));
        }

        [Fact]
        public async Task CreateAccountAndUpdateField_Returns201AndReturns200()
        {
            var accountDomain = ConstructAssetSummary();

            var guid = await CreateAccountAndValidateResponse(accountDomain).ConfigureAwait(false);

            var patchDoc = new JsonPatchDocument<AccountModel>();
            patchDoc.Add(_ => _.AccountBalance, -6000);

            var uri = new Uri($"api/v1/accounts/{guid}", UriKind.Relative);

            string body = JsonConvert.SerializeObject(patchDoc);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PatchAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var apiEntity = JsonConvert.DeserializeObject<AccountModel>(responseContent);

            apiEntity.AccountBalance.Should().Be(-6000);
        }

        [Fact]
        public async Task CreateAndGetArrears_Returns200()
        {
            var accountDomain = new[] { ConstructAssetSummary(), ConstructAssetSummary() };
            Random rnd = new Random();
            var guids = new List<Guid>();

            foreach (var acc in accountDomain)
            {
                var guid = await CreateAccountAndValidateResponse(acc).ConfigureAwait(false);
                guids.Add(guid);
            }

            foreach (var guid in guids)
            {
                var patchDoc = new JsonPatchDocument<AccountModel>();
                patchDoc.Add(_ => _.AccountBalance, (decimal) (-900 * rnd.NextDouble()));

                var uriPatch = new Uri($"api/v1/accounts/{guid}", UriKind.Relative);

                string body = JsonConvert.SerializeObject(patchDoc);

                using StringContent stringContent = new StringContent(body);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using var responsePatch = await Client.PatchAsync(uriPatch, stringContent).ConfigureAwait(false);

                responsePatch.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            var uri = new Uri($"api/v1/accounts/arrears?accountType=Master&sortBy=AccountBalance&Direction=Asc", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AccountResponses>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.AccountResponseList.Count.Should().BeGreaterOrEqualTo(2);

            var sorted = apiEntity.AccountResponseList.OrderBy(x => x.AccountBalance).ToList();

            sorted.Should().BeEquivalentTo(apiEntity.AccountResponseList);
        }

        private async Task<Guid> CreateAccountAndValidateResponse(Account accountDomain)
        {
            var uri = new Uri($"api/v1/accounts", UriKind.Relative);

            string body = JsonConvert.SerializeObject(accountDomain);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AccountModel>(responseContent);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AccountDbEntity>(apiEntity.Id).ConfigureAwait(false));

            apiEntity.Should().NotBeNull();

            apiEntity.Should().BeEquivalentTo(accountDomain, opt => opt.Excluding(x => x.ConsolidatedCharges).Excluding(x => x.AccountBalance).Excluding(x => x.Id).Excluding(x => x.Tenure));

            return apiEntity.Id;
        }

        private async Task GetAccountByIdAndValidateResponse(Account accountDomain)
        {
            var uri = new Uri($"api/v1/accounts/{accountDomain.Id}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AccountModel>(responseContent);

            apiEntity.Should().NotBeNull();

            apiEntity.Should().BeEquivalentTo(accountDomain, options => options.Excluding(x => x.ConsolidatedCharges).Excluding(x => x.AccountBalance).Excluding(x => x.Id).Excluding(x => x.Tenure));
        }
    }
}
