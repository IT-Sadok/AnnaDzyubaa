using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.DataImporter.Services;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Persistance;
using HealthcareApp.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.DataImporter.Configuration
{
    public class HostBuilderConfigurator
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                IConfiguration config = context.Configuration;
            
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("Database")));

                services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

                services.AddScoped<IUserManagerDecorator, UserManagerDecorator>();
                services.AddScoped<IMigrationsRepository, MigrationsRepository>();

                services.AddTransient<JsonImportService>();

                services.AddTransient<ImportRunner>();
            });
    }
}
