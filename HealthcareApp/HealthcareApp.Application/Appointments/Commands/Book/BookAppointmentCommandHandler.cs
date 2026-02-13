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
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, Result<string>>
    {
        private readonly IUserManagerDecorator _userManagerDecorator;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly TimeSpan _appointmentDuration = TimeSpan.FromMinutes(20);
        public BookAppointmentCommandHandler(IUserManagerDecorator userManager, IAppointmentRepository appointmentRepository)
        {
            _userManagerDecorator = userManager;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<string>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userManagerDecorator.FindByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                return Result<string>.Failure("No doctor with this ID was found");
            }

            var isAvailable = await _appointmentRepository.IsAvailableAsync(request.DoctorId, request.AppointmentDate, request.StartTime);
            if (!isAvailable)
            {
                return Result<string>.Failure("This time slot has already booked by another patient");
            }

            var appointment = request.Adapt<Appointment>();

            appointment.AppointmentDate = DateTime.SpecifyKind(appointment.AppointmentDate, DateTimeKind.Utc);
            appointment.Status = AppointmentStatuses.Requested;
            appointment.EndTime = appointment.StartTime + _appointmentDuration;
            appointment.DurationMinutes = (int)_appointmentDuration.TotalMinutes;

            try
            {
                await _appointmentRepository.AddAppointmentAsync(appointment);
            }
            catch (Exception exception)
            {
                return Result<string>.Failure(exception.Message);
            }

            return Result<string>.Success($"Your appointment for {request.AppointmentDate} at {request.StartTime} has been successfully confirmed");
        }
    }
}
