using Microsoft.AspNetCore.Mvc;
using RestaurantAPI5.Models;
using RestaurantAPI5.Services;

namespace RestaurantAPI5.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        public IAccountService AccountService { get; }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            AccountService.RegisterUser(dto);
            return Ok();
        }
    }
}
