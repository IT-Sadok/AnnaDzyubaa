using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Application.DTOs.Login
{
    public record LoginUserDTO(string Email, string Password);
}
