using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class SearchUseCase : ISearchUseCase
    {
        private readonly IAccountElasticSearchGateway _gateway;

        public SearchUseCase(IAccountElasticSearchGateway gateway)
        {
            _gateway = gateway;
        }
        public async Task<APIResponse<AccountResponse>> ExecuteAsync(AccountSearchRequest accountSearchRequest)
        {
            return await _gateway.Search(accountSearchRequest).ConfigureAwait(false);
        }
    }
}
