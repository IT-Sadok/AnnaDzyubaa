using HealthcareApp.Application.DTOs.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Appointments.Commands.Book
{
    public record BookAppointmentCommand(
        string DoctorId, 
        string PatientId, 
        DateTime StartTime)
        : IRequest<Result<int>>;
}
