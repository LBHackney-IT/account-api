using AccountsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        public AccountResponseObject Execute(Guid id);
        public Task<AccountResponseObject> ExecuteAsync(Guid id);
    }
}
