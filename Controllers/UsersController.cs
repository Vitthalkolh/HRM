using HRM.API.Models;
using HRM.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<UserResponse> users = _userService.GetAllUsers();

            return Ok(users);
        }
        [HttpPost]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            int createdBy = 1; // Temporary admin user ID

            int userId = _userService.CreateUser(request, createdBy);

            if (userId == -1)
            {
                return BadRequest(new
                {
                    Message = "Username already exists"
                });
            }

            return Ok(new
            {
                Id = userId,
                Message = "User created successfully"
            });
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UpdateUserRequest request)
        {
            int changedBy = 1; // Temporary admin user ID

            int userId = _userService.UpdateUser(id, request, changedBy);

            if (userId == -1)
            {
                return NotFound(new
                {
                    Message = "User not found"
                });
            }

            return Ok(new
            {
                Id = userId,
                Message = "User updated successfully"
            });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            int changedBy = 1; // Temporary admin user ID

            int userId = _userService.DeleteUser(id, changedBy);

            if (userId == -1)
            {
                return NotFound(new
                {
                    Message = "User not found or already inactive"
                });
            }

            return Ok(new
            {
                Id = userId,
                Message = "User deleted successfully"
            });
        }
    }
}