using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface ISearchUseCase
    {
        Task<APIResponse<AccountResponse>> ExecuteAsync(AccountSearchRequest accountSearchRequest);
    }
}
