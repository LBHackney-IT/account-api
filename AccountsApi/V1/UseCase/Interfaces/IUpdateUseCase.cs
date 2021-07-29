using AccountsApi.V1.Boundary.Response;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IUpdateUseCase
    {
        public Task<AccountModel> ExecuteAsync(AccountModel account, JsonPatchDocument<AccountModel> patchDoc);
    }
}
