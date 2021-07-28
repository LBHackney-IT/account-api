using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using AccountApi.V1.Infrastructure;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.Controllers
{
    [ApiController] 
    [Route("api/v1/accounts")]
    [Produces("application/json")]
    [ApiVersion("1.0")] 
    public class AccountApiController : BaseController
    {
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly IGetByIdUseCase _getByIdUseCase;
        private readonly IAddUseCase _addUseCase;
        private readonly IUpdateUseCase _updateUseCase;

        public AccountApiController(
            IGetAllUseCase getAllUseCase,
            IGetByIdUseCase getByIdUseCase,
            IAddUseCase addUseCase,
            IUpdateUseCase updateUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _addUseCase = addUseCase;
            _updateUseCase = updateUseCase; 
        }

        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse),StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var accounts = await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);
            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

        [ProducesResponseType(typeof(AccountResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string targetId,[FromQuery] AccountType accountType)
        {
            var accounts = await _getAllUseCase.ExecuteAsync(Guid.Parse(targetId), accountType).ConfigureAwait(false);
            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(AccountRequest account)
        {
            if (account == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Account model cannot be null!"));
            }

            if (ModelState.IsValid)
            {
                var accountResponse = await _addUseCase.ExecuteAsync(account).ConfigureAwait(false);

                return CreatedAtAction($"Get",new { id = accountResponse.Id }, accountResponse);
            }
            else
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, ModelState.GetErrorMessages()));
            }
        }

        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromRoute]Guid id,[FromBody]AccountResponse account)
        {
            if (account == null)
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest,
                    "Account model can't be null"));

            if (id != account.Id)
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest,
                    "Ids in route and model are different"));

            AccountResponse accountResponseObject = await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (accountResponseObject == null)
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound,
                    "The Account not found"));

            await _updateUseCase.ExecuteAsync(account.ToDomain()).ConfigureAwait(false);

            return CreatedAtAction($"Get", new {id = id}, account);

        }

    }
}
