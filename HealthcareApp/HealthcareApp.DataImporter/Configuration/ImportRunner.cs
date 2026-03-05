using HealthcareApp.DataImporter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.DataImporter.Configuration
{
    public class ImportRunner
    {
        private readonly JsonImportService _importService;
        private readonly string _jsonPath;
        private readonly ILogger<ImportRunner> _logger;

        public ImportRunner(JsonImportService importService, ILogger<ImportRunner> logger)
        {
            _importService = importService;
            _logger = logger;

            var baseDirectory = AppContext.BaseDirectory;

            _jsonPath = Path.Combine(baseDirectory, "Data", "data.json");
        }

        public async Task RunAsync()
        {
            if (!File.Exists(_jsonPath))
            {
                _logger.LogError("Source data file not found at path: {JsonPath}. Please ensure the file is deployed with the application (e.g., 'Copy to Output Directory' is set to 'Copy always').", _jsonPath);
                return;
            }

            _logger.LogInformation("Initiating data migration from source file: {JsonPath}...", _jsonPath);

            try
            {
                await _importService.MigrateDataAsync(_jsonPath);

                _logger.LogInformation("Data migration completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "A critical error occurred during the data migration process.");
            }
        }
    }
}