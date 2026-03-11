using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Infrastructure.Repository
{
    public class UserManagerDecorator : IUserManagerDecorator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerDecorator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) => _userManager.AddToRoleAsync(user, role);

        public Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles) => _userManager.AddToRolesAsync(user, roles);

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => _userManager.CheckPasswordAsync(user, password);

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) => _userManager.CreateAsync(user, password);

        public Task<ApplicationUser?> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

        public Task<ApplicationUser?> FindByIdAsync(string id) => _userManager.FindByIdAsync(id);

        public Task<IList<string>> GetRolesAsync(ApplicationUser user) => _userManager.GetRolesAsync(user);
    }
}
