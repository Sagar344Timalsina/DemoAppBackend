using DemoAppBE.Features.Auth.DTOs;
using FluentValidation;

namespace DemoAppBE.Features.Auth.Validators
{
    public class LoginRequestValidator:AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        }
    }
}
