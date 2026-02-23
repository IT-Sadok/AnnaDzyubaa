using HealthcareApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.DataImporter.DataTypes;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace HealthcareApp.DataImporter.Services
{
    public class JsonImportService
    {
        private readonly ApplicationDbContext _context;

        public JsonImportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ImportAsync()
        {
            Console.WriteLine("Import started ...");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "data.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {filePath}");
            }

            Console.WriteLine("Opening file stream ...");

            using (FileStream fileStream = File.OpenRead(filePath))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = jsonReader.Value?.ToString();

                        if (propertyName == "doctors")
                        {
                            Console.WriteLine("Found 'doctor' section. Starting import ...");
                            await ProcessDoctors(jsonReader);
                        }
                        else if (propertyName == "patients")
                        {
                            Console.WriteLine("Found 'patients' section. Starting import...");
                            await ProcessPatients(jsonReader);
                        }

                    }
                }
            }

            Console.WriteLine("Import completed successfully!");

        }

        private async Task ProcessDoctors(JsonTextReader reader)
        {
            Console.WriteLine("--- Starting to process Doctors in batches ---");
            var serializer = new JsonSerializer();
            var doctorsBatch = new List<JsonDoctor>();

            int batchSize = 3;
            int totalSaved = 0;

            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    var doctorDto = serializer.Deserialize<JsonDoctor>(reader);

                    if (doctorDto != null)
                    {
                        doctorsBatch.Add(doctorDto);
                    }

                    if (doctorsBatch.Count >= batchSize)
                    {
                        await SaveDoctorsBatchAsync(doctorsBatch);
                        totalSaved += doctorsBatch.Count;
                        doctorsBatch.Clear();
                    }
                }
            }

            Console.WriteLine($"--- Finished processing Doctors. Total scanned: {totalSaved} ---");
        }

        private async Task SaveDoctorsBatchAsync(List<JsonDoctor> batch)
        {
            Console.WriteLine($"[DB] Saving a batch of {batch.Count} doctors...");

            await Task.Delay(50);
        }

        private async Task ProcessPatients(JsonTextReader reader)
        {
            Console.WriteLine("--- Processing Patients (Skipped for now) ---");
            reader.Skip();
        }
    }
}
