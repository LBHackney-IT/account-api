using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using Nest;

namespace AccountsApi.V1.Infrastructure.Interfaces
{
    public interface ISearchQueryContainerOrchestrator
    {
        QueryContainer Create(AccountSearchRequest request, QueryContainerDescriptor<QueryableAccount> q);
    }
}
