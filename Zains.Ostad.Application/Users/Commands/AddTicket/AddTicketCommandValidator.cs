using FluentValidation;

namespace Zains.Ostad.Application.Users.Commands.AddTicket
{
    public class AddTicketCommandValidator:AbstractValidator<AddTicketCommand>
    {
        public AddTicketCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEqual(0);
        }
    }
}