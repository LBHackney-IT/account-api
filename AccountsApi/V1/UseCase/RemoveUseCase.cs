using AccountsApi.V1.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class RemoveUseCase : IRemoveUseCase
    {
        private readonly IAccountApiGateway _gateway;
        public RemoveUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task Execute(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);
            await _gateway.RemoveAsync(data).ConfigureAwait(false);
        }

        public async Task ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);
            await _gateway.RemoveAsync(data).ConfigureAwait(false);
        }
    }
}
