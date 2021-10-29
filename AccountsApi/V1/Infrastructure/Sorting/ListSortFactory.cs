using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Infrastructure.Sorting.Interfaces;
using Nest;

namespace AccountsApi.V1.Infrastructure.Sorting
{
    public class ListSortFactory : IListSortFactory
    {
        public SortDescriptor<QueryableAccount> DynamicSort(SortDescriptor<QueryableAccount> sortDescriptor, AccountSearchRequest request)
        {
            var propertyInfo = typeof(QueryableAccount).GetProperties();
            var sortByName = propertyInfo.ToList().FirstOrDefault(p => p.Name == request.SortBy);
            var name = nameof(sortByName);

            if (request.IsDesc)
            { 
                return sortDescriptor
                    .Ascending(f => f.AccountBalance);
            }
            else
            {
                return sortDescriptor
                    .Descending(f => f.AccountBalance);
            }
        }
    }
}
