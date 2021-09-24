using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;

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

        public async Task<AccountResponse> ExecuteAsync(AccountResponse account)
        {
            await _gateway.UpdateAsync(account.ToDomain()).ConfigureAwait(false);
            var accountSnsMessage = _snsFactory.Update(account.ToDomain());
            await _snsGateway.Publish(accountSnsMessage).ConfigureAwait(false);
            return account;
        }
    }
}
