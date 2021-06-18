using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetClaimantByIdUseCase
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IAccountApiGateway _gateway;
        public GetByIdUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountResponseObject> ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetByIdAsync(id).ConfigureAwait(false);
            return data?.ToResponse();
        }
    }
}
