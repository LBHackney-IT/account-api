using AccountsApi.systemTests.Model;
using AccountsApi.systemTests.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace AccountsApi.systemTests.StepDefinitions
{
    [Binding]
    public class GetAccountsByIdStepDefinitions : Utilities.BaseTest
    {

        private Settings _settings;
        private readonly RestRequest _restRequest;
        public static RestClient _restClient;
        public GetAccountsByIdStepDefinitions(Settings settings)
        {
            _settings = settings;
            _restRequest = new RestRequest();
            _restClient = new RestClient(ApplicationUrl);
            _restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _settings.AccessToken));

        }
        [When(@"I request a GET request for an '(.*)'")]
        public async Task WhenIRequestAGETRequestForAnBefbc_Fe_Bcb(string id)
        {
            string Url;
            Url = $"/api/v1/accounts/{id}";
           var response = await GetApi<AccountApiByIdModel>(Url, HttpStatusCode.OK, _settings.AccessToken);
            _settings.GetAccountApiByIdRestResponse = response;

        }

        [Then(@"I am returned a valid account details")]
        public async Task ThenIAmReturnedAValidAccountDetails()
        {
            var accountDetails = _settings.GetAccountApiByIdRestResponse;
            accountDetails.startDate.Should().BeBefore(DateTime.Now);
            accountDetails.endDate.Should().BeBefore(DateTime.Now);
        }

        [When(@"I request a GET request on accounts api for an '(.*)'")]
        public async Task WhenIRequestAGETRequestOnAccountsApiForAn(string id)
        {
            string Url;
            Url = $"/api/v1/accounts/{id}";
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
