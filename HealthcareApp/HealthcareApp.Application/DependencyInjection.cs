using FluentValidation;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HealthcareApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<IUserAuthenticationService, AuthenticationService>();

        return services;
    }
}
