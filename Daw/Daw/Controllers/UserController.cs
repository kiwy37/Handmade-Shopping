using Daw.DataLayer.DTO;
using Daw.DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Daw.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register",Name = "CreateUser")]
        public async Task<IActionResult> Create(UserRegistrationDTO user)
        {
            await _userService.AddUserAsync(user);
            return Ok("User created");
        }
        [HttpPost("login", Name = "LoginUser")]
        public async Task<IActionResult> Login(UserRegistrationDTO user)
        {
            var userFromDb = await _userService.Login(user);
            if(userFromDb is null)
            {
                return Ok("User not found");
            }
            if(userFromDb.Password != user.Password)
            {
                return Ok("Incorrect Password");
            }
            return Ok("User found");
        }
        [HttpGet("getUsers", Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
      
    }
}
