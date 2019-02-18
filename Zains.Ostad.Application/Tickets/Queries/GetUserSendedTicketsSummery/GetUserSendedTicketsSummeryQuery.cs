using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Tickets.Queries.GetUserSendedTicketsSummery
{
    public class GetUserSendedTicketsSummeryQuery:IRequest<TicketMetaData>
    {
        public string  UserName { get; set; }
        public bool NotSeen { get; set; }
    }
}