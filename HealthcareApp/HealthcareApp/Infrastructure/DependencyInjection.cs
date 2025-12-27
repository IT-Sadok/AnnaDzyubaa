using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
