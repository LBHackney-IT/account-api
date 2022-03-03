using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IAddBatchUseCase
    {
        public Task<int> ExecuteAsync(IEnumerable<AccountRequest> accounts);
    }
}
