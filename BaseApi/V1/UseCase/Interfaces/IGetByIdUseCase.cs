using AccountApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        public Task<AccountResponseObject> ExecuteAsync(Guid id);
    }
}
