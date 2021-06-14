using AccountApi.V1.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase
{
    public class RemoveUseCase:IRemoveuseCase
    {
        private readonly IAccountApiGateway _gateway;
        public RemoveUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public void Execute(Guid id)
        {
            var data = _gateway.GetById(id);
            _gateway.Remove(data);
        }

        public async Task ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);
            await _gateway.RemoveAsync(data).ConfigureAwait(false);
        }
    }
}
