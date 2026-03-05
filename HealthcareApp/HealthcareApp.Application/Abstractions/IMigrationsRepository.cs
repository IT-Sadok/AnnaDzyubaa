using System.Collections.Generic;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs.DataImporter;
namespace HealthcareApp.Application.Abstractions
{
    public interface IMigrationsRepository
    {
        Task MigrateBatchAsync(List<JsonUserDTO> batch);
    }
}
