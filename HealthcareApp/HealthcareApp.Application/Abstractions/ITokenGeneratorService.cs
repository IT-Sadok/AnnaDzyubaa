using HealthcareApp.Domain.Entities;
using System.Collections.Generic;

namespace HealthcareApp.Application.Abstractions
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateJwtToken(ApplicationUser user, List<string> userRoles);
    }
}
