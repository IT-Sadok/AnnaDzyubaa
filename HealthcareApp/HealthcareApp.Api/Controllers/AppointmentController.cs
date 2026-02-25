using HealthcareApp.Application.Appointments.Commands.Book;
using HealthcareApp.Application.Appointments.Queries;
using HealthcareApp.Application.DTOs.Result;
using HealthcareApp.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = UserRolesConstants.Patient)]
        public async Task<IActionResult> RequestAppointment([FromBody] BookAppointmentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var safeCommand = command with { PatientId = userId };

            var result = await _mediator.Send(safeCommand);

            return result.ToResponse();
        }

        [HttpGet]
        [Authorize(Roles = UserRolesConstants.Patient)]
        public async Task<IActionResult> GetPatientAppointments([FromQuery] GetPatientAppointmentsQuery query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var safeQuery = query with { PatientId = userId };

            var resultListAppoinments = await _mediator.Send(safeQuery);

            return resultListAppoinments.ToResponse();
        }
    }
}
