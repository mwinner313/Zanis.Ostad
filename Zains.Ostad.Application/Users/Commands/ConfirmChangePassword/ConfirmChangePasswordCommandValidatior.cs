using FluentValidation;

namespace Zains.Ostad.Application.Users.Commands.ConfirmChangePassword
{
    public class ConfirmChangePasswordCommandValidatior:AbstractValidator<ConfirmChangePasswordCommand>
    {
        public ConfirmChangePasswordCommandValidatior()
        {
            RuleFor(x => x.Token).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).NotEmpty().NotNull();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmPassword);
        }
    }
}