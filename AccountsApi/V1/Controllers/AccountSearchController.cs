using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.UseCase;
using AccountsApi.V1.UseCase.Interfaces;

namespace AccountsApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/search")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class AccountSearchController : Controller
    {
        private readonly ISearchUseCase _searchUseCase;

        public AccountSearchController(ISearchUseCase searchUseCase)
        {
            _searchUseCase = searchUseCase;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AccountSearchRequest request)
        {
            var result = await _searchUseCase.ExecuteAsync(request).ConfigureAwait(false);
            if (result.Count == 0)
                return NotFound();

            var apiResponse = new APIResponse<AccountResponse>(result);
            return Ok(apiResponse);
        }
    }
}
