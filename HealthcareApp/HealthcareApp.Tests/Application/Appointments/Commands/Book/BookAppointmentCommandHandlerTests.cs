using FluentAssertions;
using FluentAssertions.Common;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Application.Appointments.Commands.Book;
using HealthcareApp.Application.DTOs.Appointment;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Tests.Application.Appointments.Commands.Book
{
    public class BookAppointmentCommandHandlerTests
    {
        private readonly IUserManagerDecorator _userManagerDecorator;
        private readonly IAppointmentRepository _appointmentRepository;

        private readonly BookAppointmentCommandHandler _bookAppointmentCommandHandler;

        public BookAppointmentCommandHandlerTests()
        {
            _userManagerDecorator = Substitute.For<IUserManagerDecorator>();
            _appointmentRepository = Substitute.For<IAppointmentRepository>();

            _bookAppointmentCommandHandler = new BookAppointmentCommandHandler(_userManagerDecorator, _appointmentRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSucceededResult_WhenAppointmentIsBookedSuccessfully()
        {
            //Arrange

            var command = new BookAppointmentCommand(
        "20a72b61-4170-4f70-97e4-8c793f2f5280",
        "2036e496-deab-4feb-9fd7-53d9a6ba57f6",
        new DateTime (2023, 1, 1, 10, 0, 0),
        TimeSpan.FromHours(10));

            _userManagerDecorator
                .FindByIdAsync(command.DoctorId)
                .Returns(new ApplicationUser { Id = command.DoctorId});

            _appointmentRepository
                .IsAvailableAsync(command.DoctorId,
                Arg.Any<DateTime>(),
                command.StartTime)
            .Returns(true);

            //Act

            var result = await _bookAppointmentCommandHandler.Handle(command, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeTrue();
            result.Body.Should().NotBeNullOrEmpty();

            await _appointmentRepository.Received(1).AddAppointmentAsync(Arg.Any<Appointment>());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenAddAppointmentFails()
        {

            //Arrange

            var command = new BookAppointmentCommand(
        "20a72b61-4170-4f70-97e4-8c793f2f5280",
        "2036e496-deab-4feb-9fd7-53d9a6ba57f6",
        new DateTime(2023, 1, 1, 10, 0, 0),
        TimeSpan.FromHours(10));

            var errorText = "Error text";

            _userManagerDecorator
                .FindByIdAsync(command.DoctorId)
                .Returns(new ApplicationUser { Id = command.DoctorId });

            _appointmentRepository
                .IsAvailableAsync(command.DoctorId,
                Arg.Any<DateTime>(),
                command.StartTime)
            .Returns(true);

            _appointmentRepository
                .AddAppointmentAsync(Arg.Any<Appointment>())
                .Returns(Task.FromException(new Exception(errorText)));

            //Act

            var result = await _bookAppointmentCommandHandler.Handle(command, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain(errorText);
            result.Body.Should().BeNullOrEmpty();

            await _appointmentRepository.Received(1).AddAppointmentAsync(Arg.Any<Appointment>());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenIsAvailableReturnsFalse()
        {
            //Arrange

            var command = new BookAppointmentCommand(
        "20a72b61-4170-4f70-97e4-8c793f2f5280",
        "2036e496-deab-4feb-9fd7-53d9a6ba57f6",
        new DateTime(2023, 1, 1, 10, 0, 0),
        TimeSpan.FromHours(10));

            _userManagerDecorator
                .FindByIdAsync(command.DoctorId)
                .Returns(new ApplicationUser { Id = command.DoctorId });

            _appointmentRepository
                .IsAvailableAsync(command.DoctorId,
                Arg.Any<DateTime>(),
                command.StartTime)
            .Returns(false);

            //Act

            var result = await _bookAppointmentCommandHandler.Handle(command, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("This time slot has already booked by another patient");
            result.Body.Should().BeNullOrEmpty();

            await _appointmentRepository.DidNotReceive().AddAppointmentAsync(Arg.Any<Appointment>());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenDoctorIdDoesNotExist()
        {
            //Arrange

            var command = new BookAppointmentCommand(
        "20a72b61-4170-4f70-97e4-8c793f2f5280",
        "2036e496-deab-4feb-9fd7-53d9a6ba57f6",
        new DateTime(2023, 1, 1, 10, 0, 0),
        TimeSpan.FromHours(10));

            ApplicationUser? user = null;

            _userManagerDecorator
                .FindByIdAsync(command.DoctorId)
                .Returns(user);

            //Act

            var result = await _bookAppointmentCommandHandler.Handle(command, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("No doctor with this ID was found");
            result.Body.Should().BeNullOrEmpty();

            await _appointmentRepository
                .DidNotReceiveWithAnyArgs()
                .IsAvailableAsync(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<TimeSpan>());
        }
    }
}
