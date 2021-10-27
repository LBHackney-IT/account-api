using System;
using Nest;

namespace AccountsApi.V1.Domain.QueryableModels
{
    public class QueryablePrimaryTenant
    {
        [Text(Name = "id")]
        public Guid Id { get; set; }
        [Text(Name = "fullName")]
        public string FullNameName { get; set; }
    }
}
