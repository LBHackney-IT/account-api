using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/search")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class AccountSearchController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
