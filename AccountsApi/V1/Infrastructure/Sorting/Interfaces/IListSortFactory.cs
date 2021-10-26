using Nest;

namespace AccountsApi.V1.Infrastructure.Sorting.Interfaces
{
    public interface IListSortFactory<TRequest, TQueryable>
        where TRequest : class
        where TQueryable : class
    {
        SortDescriptor<TQueryable> DynamicSort(SortDescriptor<TQueryable> sortDescriptor, TRequest request);
    }
}
