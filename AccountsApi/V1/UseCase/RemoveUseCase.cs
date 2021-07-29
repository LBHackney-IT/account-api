using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase
{
    public class RemoveUseCase : IRemoveUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public RemoveUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);

            await _gateway.RemoveAsync(data).ConfigureAwait(false);
        }
    }
}
