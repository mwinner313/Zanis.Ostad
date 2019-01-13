using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Users.Queries.GetUserTicketsSummery
{
    public class GetUserTicketsSummeryQuery:IRequest<TicketMetaData>
    {
    }
}