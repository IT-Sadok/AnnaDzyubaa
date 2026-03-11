using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HealthcareApp.Application.DTOs.DataImporter
{
    public class JsonUserDTO
    {
        [JsonPropertyName("roles")]
        public List<string>? Roles { get; set; } = new();

        [JsonPropertyName("externalId")]
        public string? ExternalId { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("appointments")]
        public List<JsonAppointmentDTO>? Appointments { get; set; } = new();
    }
}
