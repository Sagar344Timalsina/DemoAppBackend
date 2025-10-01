using DemoAppBE.Features.Auth.DTOs;
using FluentValidation;

namespace DemoAppBE.Features.Auth.Validators
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Full Name is required");
        }
    }
}
