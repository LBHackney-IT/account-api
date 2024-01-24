using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;

namespace AccountsApi.V1.UseCase
{
    public class AddBatchUseCase : IAddBatchUseCase
    {
        private readonly IAccountApiGateway _gateway;
        private readonly ISnsGateway _snsGateway;
        private readonly ISnsFactory _snsFactory;

        public AddBatchUseCase(IAccountApiGateway gateway, ISnsGateway snsGateway, ISnsFactory snsFactory)
        {
            _gateway = gateway;
            _snsGateway = snsGateway;
            _snsFactory = snsFactory;
        }

        [LogCall]
        public async Task<int> ExecuteAsync(IEnumerable<AccountRequest> accounts)
        {
            var accountsList = new List<Account>();

            accounts.ToList().ToDomainList().ForEach(item =>
            {
                item.Id = Guid.NewGuid();
                item.LastUpdatedAt = DateTime.UtcNow;
                item.CreatedAt = DateTime.UtcNow;
                item.StartDate = DateTime.UtcNow;
                item.LastUpdatedBy = item.CreatedBy;
                accountsList.Add(item);
            });

            var response = await _gateway.AddBatchAsync(accountsList).ConfigureAwait(false);

            if (!response) return 0;
            var processingCount = 0;
            foreach (var accountSnsMessage in accountsList.Select(item => _snsFactory.Create(item)))
            {
                await _snsGateway.Publish(accountSnsMessage).ConfigureAwait(false);
                processingCount++;
            }
            return accountsList.Count == processingCount ? accountsList.Count : 0;
        }
    }
}
