using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Application.DTOs.Result;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Abstractions
{
    public interface IUserAuthenticationService
    {
        public Task<Result<string>> RegisterAsync(RegisterUserDTO registerUserDTO);
    }
}
