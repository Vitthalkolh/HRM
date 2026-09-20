using HRM.API.Models;
using HRM.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _userService.Login(
                request.UserName,
                request.Password
            );

            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid username or password"
                });
            }

            return Ok(user);
        }
    }
}