using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Application.DTOs.DataImporter;
using HealthcareApp.Domain.Entities;
using HealthcareApp.Infrastructure.Persistance;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareApp.Infrastructure.Repository
{
    public class MigrationsRepository : IMigrationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserManagerDecorator _userManagerDecorator;
        private readonly ILogger<MigrationsRepository> _logger;

        public MigrationsRepository(ApplicationDbContext dbContext, IUserManagerDecorator userManagerDecorator, ILogger<MigrationsRepository> logger)
        {
            _dbContext = dbContext;
            _userManagerDecorator = userManagerDecorator;
            _logger = logger;
        }

        public async Task MigrateBatchAsync(List<JsonUserDTO> batch)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var dto in batch)
                {
                    var existingUser = await _dbContext.Users
                        .FirstOrDefaultAsync(u => u.ExternalId == dto.ExternalId);

                    ApplicationUser currentUser;

                    if (existingUser == null)
                    {
                        currentUser = dto.Adapt<ApplicationUser>();
                        currentUser.UserName = dto.Email;

                        var result = await _userManagerDecorator.CreateAsync(currentUser, "ImportedUser123!");

                        if (result.Succeeded && dto.Roles != null)
                        {
                            foreach (var role in dto.Roles)
                            {
                                await _userManagerDecorator.AddToRoleAsync(currentUser, role);
                            }
                        }
                    }
                    else
                    {
                        currentUser = existingUser;
                    }

                    if (dto.Appointments != null && dto.Appointments.Any())
                    {
                        foreach (var appointmentDto in dto.Appointments)
                        {
                            var patient = await _dbContext.Users
                                .FirstOrDefaultAsync(u => u.ExternalId == appointmentDto.PatientExternalId);

                            if (patient != null)
                            {
                                var appointment = appointmentDto.Adapt<Appointment>();
                                appointment.DoctorId = currentUser.Id;
                                appointment.PatientId = patient.Id;

                                await _dbContext.Appointments.AddAsync(appointment);
                            }
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error saving batch");
                throw;
            }
            finally
            {
                _dbContext.ChangeTracker.Clear();
            }
        }
    }
}
