using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase
{
    public class AddUseCase : IAddUseCase
    {
        private readonly IAccountApiGateway _gateway;
        private readonly ISnsGateway _snsGateway;
        private readonly ISnsFactory _snsFactory;

        public AddUseCase(IAccountApiGateway gateway, ISnsGateway snsGateway, ISnsFactory snsFactory)
        {
            _gateway = gateway;
            _snsGateway = snsGateway;
            _snsFactory = snsFactory;
        }
        public async Task<AccountResponse> ExecuteAsync(AccountRequest account)
        {
            if (account == null)
                throw new ArgumentNullException($"{nameof(account).ToString()} ModelStateExtension shouldn't be null");

            DateTime curDate = DateTime.Now;
            Account domain = account.ToDomain();
            domain.Id = Guid.NewGuid();
            domain.LastUpdatedAt = curDate;
            domain.CreatedAt = curDate;
            domain.LastUpdatedBy = account.CreatedBy;
            await _gateway.AddAsync(domain).ConfigureAwait(false);
            var accountSnsMessage = _snsFactory.Create(domain);
            await _snsGateway.Publish(accountSnsMessage).ConfigureAwait(false);
            return domain.ToResponse();
        }
    }
}
