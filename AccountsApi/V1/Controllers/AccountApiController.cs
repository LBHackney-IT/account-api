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
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly IGetByIdUseCase _getByIdUseCase;
        private readonly IAddUseCase _addUseCase;
        private readonly IUpdateUseCase _updateUseCase;
        private readonly IRemoveUseCase _removeUseCase;

        public AccountApiController(
            IGetAllUseCase getAllUseCase,
            IGetByIdUseCase getByIdUseCase,
            IAddUseCase addUseCase,
            IUpdateUseCase updateUseCase,
            IRemoveUseCase removeUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _addUseCase = addUseCase;
            _updateUseCase = updateUseCase;
            _removeUseCase = removeUseCase;
        }

        [ProducesResponseType(typeof(AccountResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var accounts = await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);
            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

        [ProducesResponseType(typeof(AccountResponseObjectList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string targetid)
        {
            var accounts = await _getAllUseCase.ExecuteAsync(Guid.Parse(targetid)).ConfigureAwait(false);
            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

    }
}
