using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;
using AccountApi.V1.Factories;
using AccountApi.V1.Gateways;
using AccountApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetAllClaimantsUseCase
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IAccountApiGateway _gateway;
        public GetAllUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountResponseObjectList> ExecuteAsync(Guid targetId)
        {
            AccountResponseObjectList accountResponseObjectList = new AccountResponseObjectList();
            List<Account> data = await _gateway.GetAllAsync(targetId).ConfigureAwait(false);

            accountResponseObjectList.AccountResponseObjects = data?.Select(p => p.ToResponse()).ToList();
            return accountResponseObjectList;
        }
    }
}
