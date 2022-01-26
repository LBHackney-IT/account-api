using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class GetAccountByPrnUseCase : IGetAccountByPrnUseCase
    {
        private readonly IAccountApiGateway _accountApiGateway;

        public GetAccountByPrnUseCase(IAccountApiGateway accountApiGateway)
        {
            _accountApiGateway = accountApiGateway;
        }

        public async Task<AccountResponse> ExecuteAsync(string paymentReference)
        {
            var result = await _accountApiGateway.GetByPrnAsync(paymentReference).ConfigureAwait(false);

            return result?.ToResponse();
        }
    }
}
