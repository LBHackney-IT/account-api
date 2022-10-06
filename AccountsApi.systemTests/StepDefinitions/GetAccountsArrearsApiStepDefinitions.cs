using AccountsApi.systemTests.Model;
using AccountsApi.systemTests.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace AccountsApi.systemTests.StepDefinitions
{
    [Binding]
    public class GetAccountsArrearsApiStepDefinitions : BaseTest
    {
        private Settings _settings;
        private readonly RestRequest _restRequest;
        public static RestClient _restClient;

        public GetAccountsArrearsApiStepDefinitions(Settings settings)
        {
            _settings = settings;
            _restRequest = new RestRequest();
            _restClient = new RestClient(ApplicationUrl);
            _restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _settings.AccessToken));

        }
        [When(@"I request a GET on accounts arrears api for '(.*)', '(.*)' and '(.*)'")]
        public async Task WhenIRequestAGETOnAccountsArrearsApiForAnd(string accountType, string sort, string direction)
        {
            string Url;
                Url = $"/api/v1/accounts/arrears?SortBy={sort}&Type={accountType}&direction={direction}";
            var response = await GetApi<AccountApiModel>(Url, HttpStatusCode.OK, _settings.AccessToken);
            response.AccountResponseList.Count.Should().BeGreaterThanOrEqualTo(1);
            _settings.GetAccountApiRestResponse = response;
            response.AccountResponseList[0].AccountBalance.Should().BeLessThan(0);
        }

        [When(@"I request a GET request on accounts arrears api for '(.*)', '(.*)' and '(.*)'")]
        public async Task WhenIRequestAGETRequestOnAccountsArrearsApiForAnd(string accountType, string sort, string direction)
        {
            string Url;
            Url = $"/api/v1/accounts/arrears?SortBy={sort}&Type={accountType}&direction={direction}";
            var restRequest = new RestRequest();
            restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _settings.AccessToken));
            restRequest.Method = Method.Get;
            restRequest.Resource = Url;
            var response = await _restClient.ExecuteAsync<GetAccountUnauthorizedModel>(restRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            _settings.GetAccountApiInvalidResponse = JsonConvert.DeserializeObject<GetAccountUnauthorizedModel>(response.Content);

        }

    }
}
