using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Mini_E_commerce_SystemAPI.Interfaces;
using Mini_E_commerce_SystemAPI.Models;

namespace Mini_E_commerce_SystemAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("All-User")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userService.GetUsers();
            return Ok(users.ToList());
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] Models.LoginRequest request)
        //{
        //    var user = await _userService.AuthenticateAsync(request.name, request.Password);
        //    if (user == null)
        //    {
        //        return Unauthorized("Invalid username or password.");
        //    }

        //    // Generate JWT token (mocked for now)
        //    var token = $"mock-jwt-token-for-{user.Username}";
        //    return Ok(new { Token = token });
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.RegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.Password,
                Role = "User"
            };
            await _userService.RegisterUserAsync(user.Username, user.PasswordHash);
            return Ok("User registered successfully.");
        }
    }

}
