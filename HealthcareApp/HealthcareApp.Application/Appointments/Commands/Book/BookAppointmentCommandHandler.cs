using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Application.DTOs.Result;
using HealthcareApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.Application.Abstractions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using HealthcareApp.Domain.Constants;

namespace HealthcareApp.Application.Appointments.Commands.Book
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, Result<int>>
    {
        private readonly TimeSpan _appointmentDuration = TimeSpan.FromMinutes(20);

        private readonly IUserManagerDecorator _userManagerDecorator;
        private readonly IAppointmentRepository _appointmentRepository;

        public BookAppointmentCommandHandler(IUserManagerDecorator userManager, IAppointmentRepository appointmentRepository)
        {
            _userManagerDecorator = userManager;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<int>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userManagerDecorator.FindByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                return "No doctor with this ID was found";
            }

            var isAvailable = await _appointmentRepository.IsAvailableAsync(request.DoctorId, request.StartTime);
            if (!isAvailable)
            {
                return "This time slot has already booked by another patient";
            }

            var appointment = request.Adapt<Appointment>();

            appointment.Status = AppointmentStatuses.Requested;
            appointment.EndTime = appointment.StartTime + _appointmentDuration;
            appointment.DurationMinutes = (int)Math.Ceiling(_appointmentDuration.TotalMinutes);

            try
            {
                await _appointmentRepository.AddAppointmentAsync(appointment);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return appointment.Id;
        }
    }
}
