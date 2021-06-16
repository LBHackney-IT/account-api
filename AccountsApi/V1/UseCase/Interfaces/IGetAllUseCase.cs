using AccountsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        public Task<AccountResponseObjectList> ExecuteAsync(Guid targetId);
    }
}
