using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.DataImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.DataImporter.Abstractions
{
    public abstract class MigrationsServiceBase
    {
        private readonly IMigrationsRepository _migrationsRepository;

        protected MigrationsServiceBase(IMigrationsRepository migrationsRepository)
        {
            _migrationsRepository = migrationsRepository;
        }
        public async Task MigrateDataAsync(string path)
        {
            var batch = new List<JsonUserDTO>();
            const int batchSize = 100;

            await foreach (var record in ReadDataStreamAsync(path))
            {
                if (record != null)
                {
                    batch.Add(record);
                }

                if (batch.Count >= batchSize)
                {
                    await _migrationsRepository.MigrateBatchAsync(batch);
                    batch.Clear();
                }
            }

            if (batch.Any())
            {
                await _migrationsRepository.MigrateBatchAsync(batch);
            }
        }

        protected abstract IAsyncEnumerable<JsonUserDTO?> ReadDataStreamAsync(string path);
    }
}
