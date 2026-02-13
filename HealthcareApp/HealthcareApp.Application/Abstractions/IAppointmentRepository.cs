using HealthcareApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.Application.DTOs.Appointment;

namespace HealthcareApp.Application.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentDTO>> GetPatientAppointments(string patientId, int pageNumber, int pageSize, DateTime? start = null, DateTime? end = null);
        Task<Appointment?> GetByIdAsync(int id);
        Task<bool> IsAvailableAsync(string doctorId, DateTime date, TimeSpan startTime);
        Task AddAppointmentAsync(Appointment appointment);
    }
}
