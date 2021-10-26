using Nest;
using System.Threading.Tasks;

namespace AccountsApi.V1.Infrastructure.Helpers.Interfaces
{
    public interface ISearchElasticSearchHelper<TRequest, TQueryable>
        where TRequest : class
        where TQueryable : class
    {
        Task<ISearchResponse<TQueryable>> Search(TRequest request);
    }
}
