using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IAddUseCase
    {
        public AccountResponseObject Execute(Account account);
        public Task<AccountResponseObject> ExecuteAsync(Account account);
    }
}
