using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zains.Ostad.Application.Users.Queries.GetTicketes
{
    public class GetTicketsQuery:Pagenation,IRequest<List<TicketListViewModel>>
    {
     
    }
}