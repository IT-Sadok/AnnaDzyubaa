using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Application.DTOs.Appointment
{
    public record AppointmentDTO(int Id, 
        string? DoctorId,
        DateTime AppointmentDate,
        TimeSpan StartTime,
        TimeSpan EndTime,
        string Status
        );
}
