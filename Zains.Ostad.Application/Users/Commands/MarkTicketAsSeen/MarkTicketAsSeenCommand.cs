using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.MarkTicketAsSeen
{
    public class MarkTicketAsSeenCommand:IRequest<Response>
    {
        public long TicketId { get; set; }
    }
}