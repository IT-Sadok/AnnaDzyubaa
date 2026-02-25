using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Application.DTOs.Result;
using HealthcareApp.Domain.Constants;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Mapster;
using HealthcareApp.Application.DTOs.Login;
using Microsoft.Extensions.Configuration;
using HealthcareApp.Application.Abstractions.Decorators;

namespace HealthcareApp.Application.Implementations
{
    public class AuthenticationService : IUserAuthenticationService
    {
        private readonly IUserManagerDecorator _userManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthenticationService(IUserManagerDecorator userManager, ITokenGeneratorService tokenGeneratorService)
        {
            _userManager = userManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                return "Invalid email or password";
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = await _tokenGeneratorService.GenerateJwtToken(user, roles.ToList());

            return new LoginResponse(token);
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            if (!UserRolesConstants.IsRoleAllowed(registerUserDTO.Role))
            {
                return $"Role {registerUserDTO.Role} is not valid.";
            }

            var user = registerUserDTO.Adapt<ApplicationUser>();
            user.UserName = registerUserDTO.Email;

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerUserDTO.Role);
                return new RegisterResponse(user.Id);
            }

            var errors = result.Errors.Select(e => e.Description);
            return Result<RegisterResponse>.Failure(result.Errors.Select(e => e.Description));
        }
    }
} 
