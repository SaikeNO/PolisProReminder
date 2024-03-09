using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [AllowAnonymous]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_accountService.Login(dto));
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<TokenDto> RefreshToken([FromBody] TokenDto dto )
        {
            return Ok(_accountService.RefreshToken(dto.Token));
        }

        [HttpPost("reset-password")]
        public ActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _accountService.ResetPassword(dto);

            return NoContent();
        }
    }
}
