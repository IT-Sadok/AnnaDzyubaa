using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Application.DTOs.Appointment;
using HealthcareApp.Application.DTOs.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Application.Appointments.Queries
{
    public class GetPatientAppointmentsQueryHandler : IRequestHandler<GetPatientAppointmentsQuery, Result<List<AppointmentDTO>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetPatientAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<List<AppointmentDTO>>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetPatientAppointments(request.PatientId, request.PageNumber, request.PageSize, request.DateFrom, request.DateTo);

            return Result<List<AppointmentDTO>>.Success(appointments);
        }
    }
}
