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

        public AddUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountModel> ExecuteAsync(AccountRequest account)
        {
            Account domain = account.ToDomain();

            domain.Id = Guid.NewGuid();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            return domain.ToResponse();
        }
    }
}
