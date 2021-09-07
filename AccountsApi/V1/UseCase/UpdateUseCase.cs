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
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public UpdateUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public Task<AccountModel> ExecuteAsync(AccountModel account)
        {
            throw new NotImplementedException();
        }
    }
}
