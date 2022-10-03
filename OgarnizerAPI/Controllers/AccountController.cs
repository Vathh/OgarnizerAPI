using Microsoft.AspNetCore.Mvc;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using OgarnizerAPI.Models.CreateDtos;

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
        public ActionResult CreateUser([FromBody]CreateUserDto dto)
        {
            _accountService.CreateUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginUserDto dto)
        {
            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
