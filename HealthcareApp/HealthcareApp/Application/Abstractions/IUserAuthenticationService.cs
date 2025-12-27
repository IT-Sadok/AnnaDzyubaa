using HealthcareApp.Application.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Abstractions
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO);
    }
}
