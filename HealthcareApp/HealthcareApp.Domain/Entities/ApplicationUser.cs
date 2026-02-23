using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ExternalId { get; set; }
    }
}
