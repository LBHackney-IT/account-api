using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Gateways.Interfaces;
using Microsoft.Extensions.Logging;
using Nest;

namespace AccountsApi.V1.Gateways
{
    public class AccountElasticSearchGateway: IAccountElasticSearchGateway
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<IAccountElasticSearchGateway> _logger;

        public AccountElasticSearchGateway(IElasticClient elasticClient, ILogger<IAccountElasticSearchGateway> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public Task<AccountResponse> Search(AccountSearchRequest searchRequest)
        {
            var response = _elasticClient.
        }
    }
}
