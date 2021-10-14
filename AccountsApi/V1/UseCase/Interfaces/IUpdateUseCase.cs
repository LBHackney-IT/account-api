using AccountsApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IUpdateUseCase
    {
        public Task<AccountResponse> ExecuteAsync(AccountResponse account);
    }
}
