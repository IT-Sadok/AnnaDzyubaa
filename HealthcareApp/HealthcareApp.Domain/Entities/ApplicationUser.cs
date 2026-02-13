using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace HealthcareApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public ICollection<Appointment>? PatientAppointments { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public ICollection<Appointment>? DoctorAppointments { get; set; }
    }
}
