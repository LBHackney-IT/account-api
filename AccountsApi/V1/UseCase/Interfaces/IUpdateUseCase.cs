using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IUpdateUseCase
    {
        public AccountResponseObject Execute(Account account);
        public Task<AccountResponseObject> ExecuteAsync(Account account);
    }
}
