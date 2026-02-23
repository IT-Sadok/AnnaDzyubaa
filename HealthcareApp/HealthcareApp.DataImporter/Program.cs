using HealthcareApp.DataImporter;
using HealthcareApp.DataImporter.Configuration;
using Microsoft.Extensions.DependencyInjection;

var host = HostBuilderConfigurator.CreateHostBuilder(args).Build();

var app = host.Services.GetRequiredService<ImportRunner>();

await app.RunAsync();