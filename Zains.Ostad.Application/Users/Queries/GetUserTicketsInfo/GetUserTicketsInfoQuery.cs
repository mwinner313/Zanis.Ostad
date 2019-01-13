using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Users.Queries.GetUserTicketsInfo
{
    public class GetUserTicketsInfoQuery:IRequest<TicketMetaData>
    {
    }
}