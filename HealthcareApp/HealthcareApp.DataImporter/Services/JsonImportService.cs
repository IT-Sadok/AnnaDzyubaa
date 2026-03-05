using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Persistance;
using HealthcareApp.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json; 
using System.Threading.Tasks;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.DataImporter.Abstractions;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.DataImporter;

namespace HealthcareApp.DataImporter.Services
{
    public class JsonImportService : BaseMigrationsService
    {
        public JsonImportService(IMigrationsRepository migrationsRepository)
            : base (migrationsRepository)
        {
        }

        protected override async IAsyncEnumerable<JsonUserDTO?> ReadDataStreamAsync(string path)
        {
            using var fileStream = File.OpenRead(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            await foreach (var user in JsonSerializer.DeserializeAsyncEnumerable<JsonUserDTO>(fileStream, options))
            {
                yield return user;
            }
        }
    }
}
