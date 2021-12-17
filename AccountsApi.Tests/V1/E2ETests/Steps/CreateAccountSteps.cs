using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using FluentAssertions;

namespace AccountsApi.Tests.V1.E2ETests.Steps
{
    public class CreateAccountSteps : BaseSteps
    {
        public CreateAccountSteps(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task WhenPostAccountCalled(AccountRequest accountRequest)
        {
            var route = new Uri($"api/v1/accounts", UriKind.Relative);
            var body = JsonSerializer.Serialize(accountRequest);

            using var stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _lastResponse = await _httpClient.PostAsync(route, stringContent).ConfigureAwait(false);
        }

        public void ThenTHeLastResponseShouldBe201()
        {
            _lastResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

    }
}
