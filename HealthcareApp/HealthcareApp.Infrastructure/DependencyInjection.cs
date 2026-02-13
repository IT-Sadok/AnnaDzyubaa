using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Infrastructure.Configuration;
using HealthcareApp.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HealthcareApp.Application.Abstractions.Decorators;

namespace HealthcareApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = 

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly("HealthcareApp.Infrastructure"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""))
                    };
                });

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IUserManagerDecorator, UserManagerDecorator>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            return services;
        }
    }
}
