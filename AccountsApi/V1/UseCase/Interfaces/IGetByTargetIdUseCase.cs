using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IGetByTargetIdUseCase
    {
        public Task<AccountResponse> ExecuteAsync(Guid targetId);
    }
}
