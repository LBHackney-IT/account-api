using AccountApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        public Task<AccountResponseObjectList> ExecuteAsync(Guid targetId);
    }
}
