using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AccountsApi.systemTests.Utilities
{
    public class BaseTest : IDisposable
    {

        protected static readonly AppsettingsProvider _appsettingsProvider = new AppsettingsProvider();
        public static Uri ApplicationUrl => new Uri(_appsettingsProvider.Get("ApplicationUrl"));
        public static RestClient _restClient;
        public static string Access_Token => _appsettingsProvider.Get("E2E_ACCESS_TOKEN_DEV");
        protected static int TimesToRetryRequests => int.Parse(_appsettingsProvider.Get("TimesToRetryRequests"));

        public BaseTest()
        {
            _restClient = new RestClient(ApplicationUrl);
        }
        private RestRequest AuthenticationRestRequest(string token)
        {
            var restRequest = new RestRequest();
            restRequest.AddHeader("Authorization", string.Format("Bearer {0}", token));
            restRequest.Method = Method.Get;
            return restRequest;
        }

        protected async Task<T> GetApi<T>(string url, HttpStatusCode statusCode, string token)
        {
            var restRequest = AuthenticationRestRequest(token);
            restRequest.Resource = url;
            var getResponse = await ExecuteRequest(url, restRequest, statusCode);
            return JsonConvert.DeserializeObject<T>(getResponse.Content);

        }
        public async Task<RestResponse> ExecuteRequest(string url, RestRequest restRequest, HttpStatusCode statusCode)
        {
                var getResponse = await _restClient.ExecuteAsync(restRequest);
                for (var i = 0; i <= TimesToRetryRequests; i++)
                {
                    if (getResponse.StatusCode == statusCode &&
                        !string.IsNullOrWhiteSpace(getResponse.Content) &&
                        !getResponse.Content.Contains("\":\"Error"))
                    {
                        return getResponse;
                    }
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
                    getResponse = await _restClient.ExecuteAsync(restRequest);
                }
                throw new Exception($"The rest request failed '{url}'\nError '{getResponse.StatusDescription}'\nMessage '{getResponse.ErrorMessage}'", getResponse.ErrorException);
            }


        public void Dispose()
        {

        }
    }
}
