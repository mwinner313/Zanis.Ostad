using FluentValidation;

namespace Zains.Ostad.Application.Users.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Password).NotNull().MinimumLength(6).NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.FullName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.StudentNo).NotEmpty().NotNull().MinimumLength(5);
        }
    }
}