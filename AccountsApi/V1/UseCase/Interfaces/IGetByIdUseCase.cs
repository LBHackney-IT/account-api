using AccountsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        public Task<AccountResponse> ExecuteAsync(Guid id);
    }
}
