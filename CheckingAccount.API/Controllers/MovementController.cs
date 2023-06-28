using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.Services;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace CheckingAccount.API.Controllers
{
    [Route("api/movement")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateMovementAsync([FromBody] MovementDTO movement)
        {
            var result = await _movementService.CreateAsync(movement);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        /// <summary>
        /// Get the account movement by period
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        [HttpGet("{accountId}/movement")]
        public async Task<IActionResult> GetMovement([FromRoute] string accountId, [FromQuery] DateTime initialDate, [FromQuery] DateTime finalDate)
        {
            var result = await _movementService.GetMovement(accountId, initialDate, finalDate);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        /// <summary>
        /// Get the account movement by period
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [HttpGet("{accountId}/movement/{operationType}")]
        public async Task<IActionResult> GetMovementByOperationType([FromRoute] string accountId, [FromRoute] string operationType)
        {
            var result = await _movementService.GetMovementByOperationTypeAsync(accountId, operationType);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("{accountId}/transfer")]
        public async Task<IActionResult> Transfer([FromRoute] string accountId, [FromQuery]string destinationAccountId, [FromQuery] decimal amount)
        {
            var result = await _movementService.TransferAsync(accountId, destinationAccountId, amount);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
