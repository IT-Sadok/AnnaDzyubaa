using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthcareApp.Application.DTOs.DataImporter
{
    public class JsonAppointmentDTO
    {
        [JsonPropertyName("patientExternalId")]
        public string? PatientExternalId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("durationMinutes")]
        public int DurationMinutes { get; set; }
    }
}
