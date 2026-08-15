using MultiplePlatformServices.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using System.Security.Claims;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string token, string id)
        {
            var isConfirmed = await _authenticationService.confirmEmailAsync(token, id);
            if (!isConfirmed)
                return BadRequest(new { message = "Email confirmation failed. The link may have expired." });
            return Ok(new { message = "Your email has been successfully confirmed." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var profile = await _authenticationService.GetUserProfileAsync(userId);
            if (profile == null) return NotFound(new { message = "User not found." });

            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var success = await _authenticationService.UpdateUserProfileAsync(userId, request);
            if (!success) return NotFound(new { message = "User not found." });

            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
