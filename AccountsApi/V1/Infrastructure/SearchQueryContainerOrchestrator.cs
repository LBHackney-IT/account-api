using System;
using System.Collections.Generic;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Infrastructure.Interfaces;
using Hackney.Core.ElasticSearch.Interfaces;
using Nest;

namespace AccountsApi.V1.Infrastructure
{
    public class SearchQueryContainerOrchestrator : ISearchQueryContainerOrchestrator
    {
        private readonly IQueryBuilder<QueryableAccount> _builder;

        public SearchQueryContainerOrchestrator(IQueryBuilder<QueryableAccount> builder)
        {
            _builder = builder;
        }
        public QueryContainer Create(AccountSearchRequest request, QueryContainerDescriptor<QueryableAccount> q)
        {
            if (request == null)
                throw new ArgumentNullException($"{nameof(request).ToString()} shouldn't be null.");

            _builder
                .WithWildstarQuery(request.SearchText,
                    new List<string> { "paymentReference", "tenure.fullAddress", "tenure.primaryTenants.fullName" })
                .WithExactQuery(request.SearchText,
                    new List<string> { "paymentReference", "tenure.fullAddress", "tenure.primaryTenants.fullName" });

            return _builder.Build(q);
        }
    }
}
