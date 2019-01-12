using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Users.Queries.GetTicketDetails
{
    public class GetTicketDetailsQuery:IRequest<TicketViewModel>
    {
        public long TicketId { get; set; }
    }
}