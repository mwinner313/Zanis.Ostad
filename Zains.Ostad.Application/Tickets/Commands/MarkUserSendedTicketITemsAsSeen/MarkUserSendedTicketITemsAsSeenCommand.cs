using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Tickets.Commands.MarkUserSendedTicketITemsAsSeen
{
    public class MarkTicketITemsAsReadCommand:IRequest<Response>
    {
        public long TicketId { get; set; }
    }
}