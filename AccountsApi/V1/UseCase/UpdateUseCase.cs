using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public UpdateUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountModel> ExecuteAsync(AccountModel account)
        {
            await _gateway.UpdateAsync(account.ToDomain()).ConfigureAwait(false);

            return account;
        }
    }
}
