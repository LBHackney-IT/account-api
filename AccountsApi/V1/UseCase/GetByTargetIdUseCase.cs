using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class GetByTargetIdUseCase : IGetByTargetIdUseCase
    {
        private readonly IAccountApiGateway _gateway;
        public GetByTargetIdUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountResponse> ExecuteAsync(Guid targetId)
        {
            AccountResponses accountResponseObjectList = new AccountResponses();
            Account data = await _gateway.GetByTargetIdAsync(targetId).ConfigureAwait(false);
            return data.ToResponse();
        }
    }
}
