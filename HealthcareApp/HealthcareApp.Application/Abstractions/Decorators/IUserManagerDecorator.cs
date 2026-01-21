using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.Abstractions.Decorators
{
    public interface IUserManagerDecorator<TUser> where TUser : class
    {
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<IdentityResult> AddToRoleAsync(TUser user, string role);
        Task<TUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        Task<IList<string>> GetRolesAsync(TUser user);
    }
}
