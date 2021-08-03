using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
using AccountsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

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
        private readonly IGetAllArrearsUseCase _getAllArrearsUseCase;

        public AccountApiController(
            IGetAllUseCase getAllUseCase,
            IGetByIdUseCase getByIdUseCase,
            IAddUseCase addUseCase,
            IUpdateUseCase updateUseCase,
            IGetAllArrearsUseCase getAllArrearsUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _addUseCase = addUseCase;
            _updateUseCase = updateUseCase;
            _getAllArrearsUseCase = getAllArrearsUseCase;
        }

        /// <summary>
        /// Get an Account model
        /// </summary>
        /// <param name="id">The value by which we are looking for accounts</param>
        /// <response code="200">Success. Account model was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Account with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var account = await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Get a list of Account models
        /// </summary>
        /// <param name="targetId">The target id by which we are looking for account</param>
        /// <param name="accountType">The account type by which we are looking for accounts</param>
        /// <response code="200">Success. Account models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Accounts with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AccountResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid targetId, [FromQuery] AccountType accountType)
        {
            var accounts = await _getAllUseCase.ExecuteAsync(targetId, accountType).ConfigureAwait(false);

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        /// <summary>
        /// Create an Account model
        /// </summary>
        /// <param name="account">Account model to create</param>
        /// <response code="201">Success. Account model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status201Created)]
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

                return CreatedAtAction("GetById", new { id = accountResponse.Id }, accountResponse);
            }
            else
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, ModelState.GetErrorMessages()));
            }
        }

        /// <summary>
        /// Update an account model
        /// </summary>
        /// <param name="id">The id by which we are looking for account</param>
        /// <param name="patchDoc">Account model for update</param>
        /// <response code="200">Success. Account model was updated successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Account with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<AccountModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Account model can't be null"));
            }

            var accountResponseObject = await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (accountResponseObject == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "The Account not found"));
            }

            patchDoc.ApplyTo(accountResponseObject, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, ModelState.GetErrorMessages()));
            }

            var result = await _updateUseCase.ExecuteAsync(accountResponseObject).ConfigureAwait(false);

            return CreatedAtAction("GetById", id, result);
        }

        /// <summary>
        /// Get a list of Arrear Account models
        /// </summary>
        /// <param name="arrearRequest">Model for search and sort</param>
        /// <response code="200">Success. Account models were received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AccountResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("arrears")]
        [HttpGet]
        public async Task<IActionResult> GetArrears([FromQuery] ArrearRequest arrearRequest)
        {
            if (arrearRequest == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "ArrearRequest can't be null"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, ModelStateExtension.GetErrorMessages(ModelState)));
            }

            var accounts = await _getAllArrearsUseCase.ExecuteAsync(arrearRequest).ConfigureAwait(false);

            return Ok(accounts);
        }
    }
}
