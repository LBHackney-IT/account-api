using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Infrastructure.Helpers;
using AccountsApi.V1.Infrastructure.Helpers.Interfaces;
using AccountsApi.V1.Infrastructure.Interfaces;
using AccountsApi.V1.Infrastructure.Sorting.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Nest;
using Xunit;

namespace AccountsApi.Tests.V1.Infrastructure.Helper
{
    public class SearchElasticSearchHelperTests
    {
        private readonly Mock<IElasticClient> _esClient;
        private readonly Mock<ISearchQueryContainerOrchestrator> _containerOrchestrator;
        private readonly Mock<IPagingHelper> _pagingHelper;
        private readonly Mock<IListSortFactory> _listSortFactory;
        private readonly Mock<ILogger<SearchElasticSearchHelper>> _logger;
        private readonly Mock<Indices.ManyIndices> _indices;
        private readonly Fixture _fixture = new Fixture();
        private SearchElasticSearchHelper _sutHelper;

        public SearchElasticSearchHelperTests()
        {
            _esClient = new Mock<IElasticClient>();
            _containerOrchestrator = new Mock<ISearchQueryContainerOrchestrator>();
            _pagingHelper = new Mock<IPagingHelper>();
            _listSortFactory = new Mock<IListSortFactory>();
            _logger = new Mock<ILogger<SearchElasticSearchHelper>>();
            _indices = new Mock<Indices.ManyIndices>();
            _sutHelper = new SearchElasticSearchHelper(_esClient.Object,
                _containerOrchestrator.Object,
                _pagingHelper.Object,
                _listSortFactory.Object,
                _logger.Object);
        }

        [Fact]
        public async Task SearchExistenceDataReturnsResult()
        {
            var response = new SearchResponse<QueryableAccount>()
            {
                
            };
            var accountSearchRequest = _fixture.Create<AccountSearchRequest>();

            _esClient.Setup(p =>
                    p.SearchAsync<QueryableAccount>(It.IsAny<ISearchRequest>(),new CancellationToken(false)))
                .ReturnsAsync(response);

            var result = await _sutHelper.Search(accountSearchRequest).ConfigureAwait(false);
            result.Should().NotBeNull();
        }

    }
}
