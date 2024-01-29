using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;
using Hackney.Core.Logging;

namespace AccountsApi.V1.UseCase
{
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IAccountApiGateway _gateway;
        public GetByIdUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        [LogCall]
        public async Task<AccountResponse> ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);

            return data?.ToResponse();
        }
    }
}
