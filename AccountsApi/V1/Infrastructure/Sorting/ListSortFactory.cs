using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Infrastructure.Sorting.Enum;
using AccountsApi.V1.Infrastructure.Sorting.Interfaces;
using Nest;

namespace AccountsApi.V1.Infrastructure.Sorting
{
    public class ListSortFactory : IListSortFactory
    {
        public SortDescriptor<QueryableAccount> DynamicSort(SortDescriptor<QueryableAccount> sortDescriptor, AccountSearchRequest request)
        {
            if (request.IsDesc)
            {
                switch (request.SortBy)
                {
                    case SortBy.Address:
                        return sortDescriptor
                            .Descending(f => f.Tenure.FullAddress);
                    case SortBy.Name:
                        return sortDescriptor
                            .Descending(f => f.Tenure.PrimaryTenants.Select(r => r.FullNameName));
                    case SortBy.Prn:
                        return sortDescriptor
                            .Descending(f => f.PaymentReference);
                }
            }
            else
            {
                switch (request.SortBy)
                {
                    case SortBy.Address:
                        return sortDescriptor
                            .Ascending(f => f.Tenure.FullAddress);
                    case SortBy.Name:
                        return sortDescriptor
                            .Ascending(f => f.Tenure.PrimaryTenants.Select(r => r.FullNameName));
                    case SortBy.Prn:
                        return sortDescriptor
                            .Ascending(f => f.PaymentReference);
                }
            }

            return null;
        }
    }
}
