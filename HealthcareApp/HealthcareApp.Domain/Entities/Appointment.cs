using HealthcareApp.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }
        public ApplicationUser? Patient { get; set; }
        public ApplicationUser? Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentStatuses Status { get; set; }
        public int DurationMinutes { get; set; }

    }
}