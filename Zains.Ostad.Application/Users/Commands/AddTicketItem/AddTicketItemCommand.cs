using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.AddTicketItem
{
    public class AddTicketItemCommand:IRequest<Response<TicketItemViewModel>>
    {
        public string Message { get; set; }
        public long TicketId { get; set; }
    }
}