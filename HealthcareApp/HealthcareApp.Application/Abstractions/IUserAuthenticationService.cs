using HealthcareApp.Application.DTOs.Login;
using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Application.DTOs.Result;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Abstractions
{
    public interface IUserAuthenticationService
    {
        public Task<Result<RegisterResponse>> RegisterAsync(RegisterUserDTO registerUserDTO);

        public Task<Result<LoginResponse>> LoginAsync(LoginUserDTO loginUserDTO);
    }
}
