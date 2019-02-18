using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Tickets.Queries.GetUserTicketDetails
{
    public class GetUserTicketDetailsQuery:IRequest<TicketViewModel>
    {
        public long TicketId { get; set; }
    }
}