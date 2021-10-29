using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Infrastructure.Helpers.Interfaces;
using AccountsApi.V1.Infrastructure.Interfaces;
using AccountsApi.V1.Infrastructure.Sorting.Interfaces;
using Microsoft.Extensions.Logging;
using Nest;

namespace AccountsApi.V1.Infrastructure.Helpers
{
    public class SearchElasticSearchHelper : ISearchElasticSearchHelper
    {
        private readonly IElasticClient _esClient;
        private readonly ISearchQueryContainerOrchestrator _containerOrchestrator;
        private readonly IPagingHelper _pagingHelper;
        private readonly IListSortFactory _listSortFactory;
        private readonly ILogger<SearchElasticSearchHelper> _logger;
        private readonly Indices.ManyIndices _indices;

        public SearchElasticSearchHelper(IElasticClient esClient, ISearchQueryContainerOrchestrator containerOrchestrator,
            IPagingHelper pagingHelper, IListSortFactory listSortFactory, ILogger<SearchElasticSearchHelper> logger)
        {
            _esClient = esClient;
            _containerOrchestrator = containerOrchestrator;
            _pagingHelper = pagingHelper;
            _listSortFactory = listSortFactory;
            _logger = logger;
            _indices = Indices.Index(new List<IndexName> { "accounts" });
        }

        public async Task<ISearchResponse<QueryableAccount>> Search(AccountSearchRequest request)
        {
            try
            {
                _logger.LogDebug($"ElasticSearch Search begins {Environment.GetEnvironmentVariable("ELASTICSEARCH_DOMAIN_URL")}");

                if (request == null)
                {
                    return new SearchResponse<QueryableAccount>();
                }

                var pageOffset = _pagingHelper.GetPageOffset(request.PageSize, request.PageNumber);

                var result = await _esClient.SearchAsync<QueryableAccount>(x => x.Index(_indices)
                    .Query(q => BaseQuery(request, q))
                    .Sort(s => _listSortFactory.DynamicSort(s, request))
                    .Size(request.PageSize)
                    .Skip(pageOffset)
                    .TrackTotalHits()).ConfigureAwait(false);

                _logger.LogDebug("ElasticSearch Search ended");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ElasticSearch Search threw an exception");
                throw;
            }
        }

        private QueryContainer BaseQuery(AccountSearchRequest request, QueryContainerDescriptor<QueryableAccount> queryDescriptor)
        {
            return _containerOrchestrator.Create(request, queryDescriptor);
        }

    }
}
