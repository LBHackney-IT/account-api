using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class SearchUseCase:ISearchUseCase
    {
        private readonly AccountElasticSearchGateway _gateway;

        public SearchUseCase(AccountElasticSearchGateway gateway)
        {
            _gateway = gateway;
        }
        public async Task<List<AccountResponse>> ExecuteAsync(AccountSearchRequest accountSearchRequest)
        {
            var result = await _gateway.Search(accountSearchRequest).ConfigureAwait(false);
            return result;
        }
    }
}
