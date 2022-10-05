using AccountsApi.systemTests.Utilities;
using TechTalk.SpecFlow;

namespace AccountsApi.systemTests.StepDefinitions
{
    [Binding]
    public class CommonSteps : BaseTest
    {
        private readonly Settings _settings;

        public CommonSteps(Settings settings)
        {
            _settings = settings;
        }
        [Given(@"I get JWT authentication as a finance user")]
        public async Task GivenIGetJWTAuthenticationAsAFinanceUser()
        {
            _settings.AccessToken = Access_Token;
        }

        [Given(@"I don't get JWT authentication as a finance user")]
        public void GivenIDontGetJWTAuthenticationAsAFinanceUser()
        {
            _settings.AccessToken = String.Empty;
        }
    }
}
