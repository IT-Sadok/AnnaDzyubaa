using Microsoft.AspNetCore.Mvc;
using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Application.DTOs.Login;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.Result;
using Microsoft.AspNetCore.Identity.Data;

namespace HealthcareApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
    
        public AuthController(IUserAuthenticationService userAuthenticationService) =>
            _userAuthenticationService = userAuthenticationService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            var result = await _userAuthenticationService.RegisterAsync(registerUserDto);

            return result.ToResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            return result.ToResponse();
        }
    };
}
