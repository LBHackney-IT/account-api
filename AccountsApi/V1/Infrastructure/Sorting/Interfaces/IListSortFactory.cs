using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using Nest;

namespace AccountsApi.V1.Infrastructure.Sorting.Interfaces
{
    public interface IListSortFactory
    {
        SortDescriptor<QueryableAccount> DynamicSort(SortDescriptor<QueryableAccount> sortDescriptor, AccountSearchRequest request);
    }
}
