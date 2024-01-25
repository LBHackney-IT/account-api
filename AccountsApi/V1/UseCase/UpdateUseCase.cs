using System;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;
using Hackney.Core.Logging;
using Hackney.Core.Sns;

namespace AccountsApi.V1.UseCase
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IAccountApiGateway _gateway;
        private readonly ISnsGateway _snsGateway;
        private readonly ISnsFactory _snsFactory;

        public UpdateUseCase(IAccountApiGateway gateway, ISnsGateway snsGateway, ISnsFactory snsFactory)
        {
            _gateway = gateway;
            _snsGateway = snsGateway;
            _snsFactory = snsFactory;
        }

        [LogCall]
        public async Task<AccountResponse> ExecuteAsync(AccountResponse account)
        {
            account.LastUpdatedAt = DateTime.UtcNow;
            await _gateway.UpdateAsync(account.ToDomain()).ConfigureAwait(false);

            var accountSnsMessage = _snsFactory.Update(account.ToDomain());
            var accountTopicArn = Environment.GetEnvironmentVariable("ACCOUNTS_SNS_ARN");
            await _snsGateway.Publish(accountSnsMessage, accountTopicArn).ConfigureAwait(false);

            return account;
        }
    }
}
