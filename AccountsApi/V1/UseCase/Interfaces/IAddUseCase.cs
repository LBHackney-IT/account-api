using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IAddUseCase
    {
        public Task<AccountResponse> ExecuteAsync(AccountRequest account);
    }
}
