using FluentValidation;

namespace API_CobraApp.Application.Features.Users.Patch
{
    public class PatchUserCommandValidator
        : AbstractValidator<PatchUserCommand>
    {
        public PatchUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Dto)
                .NotNull()
                .WithMessage("Patch data is required.");

            RuleFor(x => x.Dto.FirstName)
                .MinimumLength(2)
                .WithMessage("First name must be at least {MinLength} characters.")
                .MaximumLength(100)
                .WithMessage("First name must not exceed {MaxLength} characters.")
                .When(x => x.Dto.FirstName is not null);

            RuleFor(x => x.Dto.LastName)
                .MinimumLength(2)
                .WithMessage("Last name must be at least {MinLength} characters.")
                .MaximumLength(100)
                .WithMessage("Last name must not exceed {MaxLength} characters.")
                .When(x => x.Dto.LastName is not null);

            RuleFor(x => x.Dto.Email)
                .EmailAddress()
                .WithMessage("Email format is invalid.")
                .When(x => x.Dto.Email is not null);

            RuleFor(x => x.Dto)
                .Must(dto =>
                    dto.FirstName is not null ||
                    dto.LastName is not null ||
                    dto.Email is not null)
                .WithMessage("At least one field must be provided.");
        }
    }
}