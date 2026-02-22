using FluentValidation;

namespace API_CobraApp.Application.Features.Users.Update
{
    public class UpdateUserCommandValidator
        : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least {MinLength} characters.")
                .MaximumLength(100).WithMessage("First name must not exceed {MaxLength} characters.");

            RuleFor(x => x.User.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least {MinLength} characters.")
                .MaximumLength(100).WithMessage("Last name must not exceed {MaxLength} characters.");

            RuleFor(x => x.User.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");

        }
    }
}