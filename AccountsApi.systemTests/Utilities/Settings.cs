

using AccountsApi.systemTests.Model;
using RestSharp;

namespace AccountsApi.systemTests.Utilities
{
    public class Settings
    {
        public RestResponse Response { get; set; }
        public string AccessToken { get; set; }
        public RestResponse RestResponse { get; set; }
        public string RestPingResponse { get; set; }
        public AccountApiModel GetAccountApiRestResponse { get; set; }
    }
}
