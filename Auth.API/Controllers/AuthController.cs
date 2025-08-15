using Auth.API.Application.InputModels;
using Auth.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel input)
        {
            var result = await userService.Login(input);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(new { Token = result.Data });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddUserInputModel input)
        {
            var result = await userService.AddUser(input);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await userService.GetAllUsers();
            
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
