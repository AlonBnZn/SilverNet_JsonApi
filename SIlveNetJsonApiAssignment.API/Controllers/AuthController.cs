using Microsoft.AspNetCore.Mvc;
using SilveNetJsonApiAssignment.Service.DTOs;
using SilveNetJsonApiAssignment.Service.Services;

namespace SilveNetJsonApiAssignment.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDto.TenantId, loginDto.UserId);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(result);
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {


            var result = await _authService.RegisterAsync();

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(result);
        }
    }
}
