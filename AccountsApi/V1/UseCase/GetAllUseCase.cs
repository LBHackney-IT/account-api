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

        public async Task<AccountResponseObjectList> ExecuteAsync(Guid targetId)
        {
            string accountType = "Test";
            AccountResponseObjectList accountResponseObjectList = new AccountResponseObjectList();
            List<Account> data = await _gateway.GetAllAsync(targetId, accountType).ConfigureAwait(false);
            accountResponseObjectList.AccountResponseObjects = data?.Select(p => p.ToResponse()).ToList();
            return accountResponseObjectList;
        }
    }
}
