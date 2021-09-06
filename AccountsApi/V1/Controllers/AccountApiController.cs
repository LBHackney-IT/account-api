using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v1/residents")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class AccountApiController : BaseController
    {


    }
}
