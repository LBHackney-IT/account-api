using Nest;

namespace AccountsApi.V1.Infrastructure.Interfaces
{
    public interface ISearchQueryContainerOrchestrator<TRequest, TQueryable>
        where TRequest : class
        where TQueryable : class
    {
        QueryContainer Create(TRequest request, QueryContainerDescriptor<TQueryable> q);
    }
}
