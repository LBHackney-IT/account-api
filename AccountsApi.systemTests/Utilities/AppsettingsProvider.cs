using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsApi.systemTests.Model;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow.Configuration;

namespace AccountsApi.systemTests.Utilities
{
    public class AppsettingsProvider
    {
        private readonly IConfiguration _config;
        private FetchTokenParams _fetchTokenParams;

        public AppsettingsProvider()
        {
            _config = InitConfiguration();
        }

        public string Get(string name)
        {
            return _config[name];
        }

        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testsettings.json")
                .AddEnvironmentVariables()
                .Build();
            _fetchTokenParams = new FetchTokenParams();
            config.Bind("Authentication",_fetchTokenParams);
            return config;
        }
    }
}
