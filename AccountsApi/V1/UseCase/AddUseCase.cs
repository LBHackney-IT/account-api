using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AccountResponseObject Execute(Account account)
        {
            _gateway.Add(account);
            return account.ToResponse();

        }

        public async Task<AccountResponseObject> ExecuteAsync(Account account)
        {
            await _gateway.AddAsync(account).ConfigureAwait(false);
            return account.ToResponse();
        }
    }
}
