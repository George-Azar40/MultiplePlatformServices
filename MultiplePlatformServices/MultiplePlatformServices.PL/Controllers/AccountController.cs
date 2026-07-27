using Azure.Core;
using MultiplePlatformServices.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;

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
        public async Task<IActionResult> ConfirmEmail(string token,string id)
        {
            var isConfirmed =await _authenticationService.confirmEmailAsync(token, id);
            return Ok(new
            {
                message = "Your email Successfully Confirmed"
            });
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

    }
}
