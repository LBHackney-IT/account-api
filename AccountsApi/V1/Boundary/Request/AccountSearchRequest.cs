using AccountsApi.V1.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountSearchRequest
    {
        [FromQuery(Name = "searchText")]
        public string SearchText { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = Constants.DefaultPageSize;

        [FromQuery(Name = "page")]
        public int Page { get; set; }

        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; }

        [FromQuery(Name = "isDesc")]
        public bool IsDesc { get; set; }
    }
}
