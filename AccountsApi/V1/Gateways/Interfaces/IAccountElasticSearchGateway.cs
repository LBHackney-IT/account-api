using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using Nest;

namespace AccountsApi.V1.Gateways.Interfaces
{
    public interface IAccountElasticSearchGateway
    {
        public Task<AccountResponse> Search(AccountSearchRequest searchRequest);
    }
}
