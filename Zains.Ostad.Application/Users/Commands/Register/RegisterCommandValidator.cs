using FluentValidation;

namespace Zains.Ostad.Application.Users.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.FullName).NotNull();
            RuleFor(x => x.FullName).MinimumLength(5);
            RuleFor(x => x.StudentNo).NotEmpty();
            RuleFor(x => x.StudentNo).NotNull();
            RuleFor(x => x.StudentNo).MinimumLength(5);
        }
    }
}