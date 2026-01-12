using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Infrastructure.Configuration
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";
        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public int DurationInMinutes { get; set; }
    }
}
