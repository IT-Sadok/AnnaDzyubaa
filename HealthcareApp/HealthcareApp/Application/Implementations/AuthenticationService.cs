using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Domain.Constants;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Implementations
{
    public class AuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            if (!UserRolesConstants.IsRoleAllowed(registerUserDTO.Role))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRole", 
                    Description = $"Role {registerUserDTO.Role} is not valid."
                });
            }

            var user = new ApplicationUser
            {
                UserName = registerUserDTO.Email,
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName
            };

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerUserDTO.Role);
            }

            return result;
        }
    }
}
