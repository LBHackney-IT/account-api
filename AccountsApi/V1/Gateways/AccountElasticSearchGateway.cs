using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.Gateways.Model;
using AccountsApi.V1.Infrastructure.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using Nest;

namespace AccountsApi.V1.Gateways
{
    public class AccountElasticSearchGateway : IAccountElasticSearchGateway
    {
        private readonly ISearchElasticSearchHelper<AccountSearchRequest, QueryableAccount> _elasticClient;
        private readonly ILogger<IAccountElasticSearchGateway> _logger;

        public AccountElasticSearchGateway(ISearchElasticSearchHelper<AccountSearchRequest, QueryableAccount> elasticClient, ILogger<IAccountElasticSearchGateway> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<AccountResponses> Search(AccountSearchRequest searchRequest)
        {
            var searchResponse = await _elasticClient.Search(searchRequest).ConfigureAwait(false);

            AccountResponses accountResponses = new AccountResponses();
            /*accountResponses.AccountResponseList.AddRange(searchResponse.Documents.Select(p =>
                new AccountResponse
                {
                    Id = p.Create()
                }).ToList());*/
            return accountResponses;
        }
    }
}
