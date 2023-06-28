using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace CheckingAccount.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDTO account)
        {
            var result = await _accountService.CreateAsync(account);
            if(result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        /// <summary>
        /// Get the balance of the account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance(string id)
        {
            var result = await _accountService.GetBalanceAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
