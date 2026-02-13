using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.DTOs.Appointment;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int appointmentId) => 
            await _context.Appointments.FindAsync(appointmentId);

        public async Task<List<AppointmentDTO>> GetPatientAppointments(string patientId, int pageNumber, int pageSize, DateTime? start = null, DateTime? end = null)
        {
            var query = _context.Appointments
                .AsNoTracking()
                .Where(x => x.PatientId == patientId);

            if (start != null)
            {
                query = query.Where(x => x.AppointmentDate >= start.Value);
            }

            if (end != null)
            {
                query = query.Where(x => x.AppointmentDate <= end.Value);
            }


                return await query.OrderBy(x => x.AppointmentDate)
                .ThenBy(x => x.StartTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new AppointmentDTO(
                    x.Id, x.DoctorId, 
                    x.AppointmentDate,
                    x.StartTime, 
                    x.EndTime, 
                    x.Status.ToString()))
                .ToListAsync();
        }

        public async Task<bool> IsAvailableAsync(string doctorId, DateTime date, TimeSpan startTime)
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            var endTime = startTime.Add(TimeSpan.FromMinutes(20));

            var existingAppointment = await _context.Appointments
                .SingleOrDefaultAsync(a => a.DoctorId == doctorId &&
                a.AppointmentDate == date && 
                (a.StartTime < endTime && a.EndTime > startTime));

            return existingAppointment == null;
        }
    }
}
