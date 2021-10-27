using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.UseCase;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AccountsApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/search")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class AccountSearchController : Controller
    {
        private readonly SearchUseCase _searchUseCase;

        public AccountSearchController(SearchUseCase searchUseCase)
        {
            _searchUseCase = searchUseCase;
        }
        [HttpGet]
        public async Task<IActionResult> Get(AccountSearchRequest request)
        {
            var result = await _searchUseCase.ExecuteAsync(request).ConfigureAwait(false);
            if (result.Count == 0)
                return NotFound();

            var apiResponse = new APIResponse<AccountResponse>(result);
            return Ok(apiResponse);
        }
    }
}
