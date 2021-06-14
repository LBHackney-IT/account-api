using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;
using AccountApi.V1.Factories;
using AccountApi.V1.Gateways;
using AccountApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public UpdateUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public AccountResponseObject Execute(Account account)
        {
            _gateway.Update(account);
            return account.ToResponse();
        }

        public async Task<AccountResponseObject> ExecuteAsync(Account account)
        {
            await _gateway.UpdateAsync(account).ConfigureAwait(false);
            return account.ToResponse();
        }
    }
}
