using HealthcareApp.DataImporter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.DataImporter
{
    public class ImportRunner
    {
        private readonly JsonImportService _importService;

        public ImportRunner(JsonImportService importService)
        {
            _importService = importService;
        }

        public async Task RunAsync()
        {
            await _importService.ImportAsync();
        }
    }
}
