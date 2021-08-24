using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IAddUseCase
    {
        public Task<AccountModel> ExecuteAsync(AccountRequest account);
    }
}
