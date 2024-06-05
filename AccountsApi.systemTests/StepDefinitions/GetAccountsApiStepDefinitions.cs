using AccountsApi.systemTests.Model;
using AccountsApi.systemTests.Utilities;
using FluentAssertions;
using Gherkin;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.WebSockets;
using TechTalk.SpecFlow;

namespace AccountsApi.systemTests.StepDefinitions
{
    [Binding]
    public class GetAccountsApiStepDefinitions : BaseTest
    {
        private Settings _settings;
        private readonly RestRequest _restRequest;
        public static RestClient _restClient;

        public GetAccountsApiStepDefinitions(Settings settings)
        {
            _settings = settings;
            _restRequest = new RestRequest();
            _restClient = new RestClient(ApplicationUrl);
            _restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _settings.AccessToken));
        }

        [When(@"I request a GET on accounts api for '(.*)' and '(.*)'")]
        public async Task WhenIRequestAGETOnAccountsApiForAnd(string targetId, string accountType)
        {
            string Url;
            if (targetId.Length == 0)
                Url = $"/api/v1/accounts?accountType={accountType}";
            else
                Url = $"/api/v1/accounts?targetId={targetId}&accountType={accountType}";
            var response = await GetApi<AccountApiModel>(Url, HttpStatusCode.OK, _settings.AccessToken);
            response.AccountResponseList.Count.Should().Be(1);
            _settings.GetAccountApiRestResponse = response;
          //  _settings.GetAccountApiRestResponse = response;
           // _settings.GetAccountApiRestResponse.Should().NotBeNull();
           
        }

        [Then(@"I am returned a valid response")]
        public async Task ThenIAmReturnedAValidResponse()
        {
            var accountResponseList = _settings.GetAccountApiRestResponse.AccountResponseList[0];
            _settings.GetAccountApiRestResponse.Should().NotBeNull();
            accountResponseList.AccountType.Should().Be("Master");

        }

        [When(@"I request a GET request on accounts api for '([^']*)' and '([^']*)'")]
        public async Task WhenIRequestAGETRequestOnAccountsApiForAnd(string targetId, string accountType)
        {
            string Url;
            Url = $"/api/v1/accounts?targetId={targetId}&accountType={accountType}";
            var restRequest = new RestRequest();
            restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _settings.AccessToken));
            restRequest.Method = Method.Get;
            restRequest.Resource = Url;
            var response = await _restClient.ExecuteAsync<GetAccountUnauthorizedModel>(restRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            
            _settings.GetAccountApiInvalidResponse =  JsonConvert.DeserializeObject<GetAccountUnauthorizedModel>(response.Content);
        }


        [Then(@"I am returned an invalid response")]
        public async Task ThenIAmReturnedAnInvalidResponse()
        {
            _settings.GetAccountApiInvalidResponse.Message.Should().Be("User is not authorized to access this resource with an explicit deny");
        }

    }
}
