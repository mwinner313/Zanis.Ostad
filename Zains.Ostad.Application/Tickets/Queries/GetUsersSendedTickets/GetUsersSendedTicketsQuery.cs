using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Queries.GetUsersSendedTickets
{
    public class GetUsersSendedTicketsQuery : Pagenation, IRequest<TicketDto>
    {
        public string Search { get; set; }
        public bool NotSeen { get; set; }
        public TicketState? State { get; set; }
        public int? CategoryId { get; set; }
    }
}