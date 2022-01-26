using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Response;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IGetAccountByPrnUseCase
    {
        Task<AccountResponse> ExecuteAsync(string paymentReference);
    }
}
