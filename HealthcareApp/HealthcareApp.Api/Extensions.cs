using HealthcareApp.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareApp
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, HealthcareApp.Mediator.Interfaces.Mediator>();

            return services;
        }
    }
}
