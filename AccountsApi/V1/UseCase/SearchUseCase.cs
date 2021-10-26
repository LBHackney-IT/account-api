using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class SearchUseCase:ISearchUseCase
    {
        public SearchUseCase()
        {
            
        }
        public Task<AccountResponse> ExecuteAsync(AccountSearchRequest accountSearchRequest)
        {
            throw new NotImplementedException();
        }
    }
}
