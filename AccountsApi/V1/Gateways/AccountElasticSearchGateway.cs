using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Domain.QueryableModels;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.Infrastructure.Helpers.Interfaces;
using Microsoft.Extensions.Logging;

namespace AccountsApi.V1.Gateways
{
    public class AccountElasticSearchGateway : IAccountElasticSearchGateway
    {
        private readonly ISearchElasticSearchHelper<AccountSearchRequest, QueryableAccount> _elasticClient;
        private readonly ILogger<IAccountElasticSearchGateway> _logger;

        public AccountElasticSearchGateway(ISearchElasticSearchHelper<AccountSearchRequest, QueryableAccount> elasticClient, ILogger<IAccountElasticSearchGateway> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<List<AccountResponse>> Search(AccountSearchRequest searchRequest)
        {
            var searchResponse = await _elasticClient.Search(searchRequest).ConfigureAwait(false);

            List<AccountResponse> responses = searchResponse.Documents.Select(p =>
                new AccountResponse
                {
                    Id = p.Id,
                    Tenure = p.Tenure == null ? null : new Tenure
                    {
                        FullAddress = p.Tenure.FullAddress,
                        PrimaryTenants = p.Tenure.PrimaryTenants?.Select(t => new PrimaryTenants
                        {
                            Id = t.Id,
                            FullName = t.FullNameName
                        }).ToList()
                    },
                    ConsolidatedCharges = p.ConsolidatedCharges?.Select(c => new ConsolidatedCharge
                    {
                        Amount = c.Amount,
                        Frequency = c.Frequency,
                        Type = c.Type
                    }).ToList(),
                    CreatedBy = p.CreatedBy,
                    LastUpdatedBy = p.LastUpdatedBy,
                    AccountBalance = p.AccountBalance,
                    StartDate = p.StartDate,
                    AccountType = p.AccountType,
                    LastUpdatedAt = p.LastUpdatedAt,
                    EndDate = p.EndDate,
                    TargetId = p.TargetId,
                    AccountStatus = p.AccountStatus,
                    RentGroupType = p.RentGroupType,
                    AgreementType = p.AgreementType,
                    ConsolidatedBalance = p.ConsolidatedBalance,
                    CreatedAt = p.CreatedAt,
                    ParentAccountId = p.ParentAccountId,
                    PaymentReference = p.PaymentReference,
                    TargetType = p.TargetType
                }).ToList();

            return responses;
        }
    }
}
