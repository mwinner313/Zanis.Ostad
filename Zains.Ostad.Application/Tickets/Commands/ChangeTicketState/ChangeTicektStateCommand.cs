using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.ChangeTicketState
{
    public class ChangeTicektStateCommand:IRequest<Response>
    {
        public long TicketId { get; set; }
        public TicketState State { get; set; }
    }
}