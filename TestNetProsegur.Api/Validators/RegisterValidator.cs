using FluentValidation;
using TestNetProsegur.Application.Dtos.Auth;

namespace TestNetProsegur.Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email es nulo o vacío.");

            RuleFor(x => x.Roles)
                .Must(x => x != null && x.Length > 0)
                .WithMessage("Los roles debe tener al menos un elemento.");
        }
    }
}
