using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Entities;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
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

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
