using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;

namespace AccountsApi.V1.Gateways.Interfaces
{
    public interface IAccountElasticSearchGateway
    {
        public Task<AccountResponses> Search(AccountSearchRequest searchRequest);
    }
}
