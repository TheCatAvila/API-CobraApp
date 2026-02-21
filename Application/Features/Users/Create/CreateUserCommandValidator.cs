using FluentValidation;

namespace API_CobraApp.Application.Features.Users.Create
{
    public class CreateUserCommandValidator
        : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.User.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2);

            RuleFor(x => x.User.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2);

            RuleFor(x => x.User.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.User.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.User.LinkedCode)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}