using Microsoft.AspNetCore.Mvc;
using SocialPlatform.Core.DTOs;
using SocialPlatform.Service;
using System;
using System.Threading.Tasks;

namespace SocialPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/signup
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                var response = await _userService.SignUpAsync(userRequestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var response = await _userService.LoginAsync(loginRequestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // PUT: api/user/update-password/{userId}
        [HttpPut("update-password/{userId}")]
        public async Task<IActionResult> UpdatePassword(int userId, [FromBody] string newPassword)
        {
            try
            {
                var response = await _userService.UpdatePasswordAsync(userId, newPassword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/user/activate/{userId}
        [HttpPut("activate/{userId}")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            try
            {
                await _userService.ActivateUserAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/user/deactivate/{userId}
        [HttpPut("deactivate/{userId}")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            try
            {
                await _userService.DeactivateUserAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
