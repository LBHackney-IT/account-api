using AccountsApi.V1.Infrastructure;
using AccountsApi.V1.Infrastructure.Sorting.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountSearchRequest
    {
        [FromQuery(Name = "searchText")]
        public string SearchText { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = Constants.DefaultPageSize;

        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "sortBy")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortBy SortBy { get; set; }

        [FromQuery(Name = "isDesc")]
        public bool IsDesc { get; set; }
    }
}
