using Microsoft.AspNetCore.Mvc;
using MatchSpark.Data.Services;
using MatchSpark.API.DTOs;
using MatchSpark.Core.Entities;

namespace MatchSpark.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userServices;
        public UsersController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto newUserDto)
        {
            AppUser newUser = new AppUser { Email = newUserDto.Email };
            bool result = await _userServices.RegisterUserAsync(newUser, newUserDto.Password);
            if (result)
                return Ok("User registered successfully.");
            return BadRequest("User registration failed.");
        }
    }
}