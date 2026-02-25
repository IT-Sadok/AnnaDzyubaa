using HealthcareApp.Application.DTOs.Result;
using HealthcareApp.Application.DTOs.Appointment;
using MediatR;
using System;
using System.Collections.Generic;
namespace HealthcareApp.Application.Appointments.Queries
{
    public record GetPatientAppointmentsQuery(string PatientId, int PageNumber = 1, int PageSize = 10, DateTime? DateFrom = null, DateTime? DateTo = null) 
        : IRequest<Result<List<AppointmentDTO>>>;
}
