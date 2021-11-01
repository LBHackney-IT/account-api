using Nest;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;

namespace AccountsApi.V1.Infrastructure.Helpers.Interfaces
{
    public interface ISearchElasticSearchHelper
    {
        Task<ISearchResponse<QueryableAccount>> Search(AccountSearchRequest request);
    }
}
