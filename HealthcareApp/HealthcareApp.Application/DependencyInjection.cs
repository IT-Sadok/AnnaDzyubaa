using FluentValidation;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
        services.AddScoped<IUserAuthenticationService, AuthenticationService>();

        return services;
    }
}
