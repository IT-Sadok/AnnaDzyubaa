using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace HealthcareApp.Application.DTOs.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name cannot be empty.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("First Name can only contain letters.")
                .MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name cannot be empty.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("Last Name can only contain letters.")
                .MaximumLength(50).WithMessage("Last Name cannot be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password cannot be less than 6 characters");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role cannot be empty.");

        }
    }
}