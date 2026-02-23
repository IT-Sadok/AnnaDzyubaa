using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.DataImporter.DataTypes
{
    public class JsonDoctor
    {
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("appointments")]
        public string Appointments { get; set; }
    }
}
