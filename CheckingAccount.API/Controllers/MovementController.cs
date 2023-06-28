using CheckingAccount.API.Domain.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheckingAccount.API.Controllers
{
    [Route("api/movement")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> CreateMovementAsync([FromBody] Movement movement)
        {
            return Ok();
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
            return Ok();
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
            return Ok();
        }
    }
}
