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
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetAllClaimantsUseCase
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IAccountApiGateway _gateway;
        public GetAllUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public Task<AccountResponses> ExecuteAsync(Guid targetId, AccountType accountType)
        {
            throw new NotImplementedException();
        }
    }
}
