using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleTask.Application.Contracts.Identity;
using SampleTask.Application.Models.Identity;

namespace SampleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegisterationResponse>> Register(RegisterationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
    }
}
