using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;

namespace OgarnizerAPI.Controllers
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

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser([FromBody]CreateUserDto dto)
        {
            _accountService.CreateUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody]LoginUserDto dto)
        {
            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser([FromBody] DeleteUserDto dto)
        {
            _accountService.DeleteUser(dto);

            return Ok();
        }
    }
}
