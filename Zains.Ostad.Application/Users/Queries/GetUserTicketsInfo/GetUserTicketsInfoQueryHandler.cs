using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Queries.GetUserTicketsInfo
{
    public class GetUserTicketsInfoQueryHandler : IRequestHandler<GetUserTicketsInfoQuery, TicketMetaData>
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IRepository<Ticket, long> _ticketRepo;
        

        public GetUserTicketsInfoQueryHandler(IWorkContext workContext, IRepository<TicketItem, long> ticketItemRepo, IRepository<Ticket, long> ticketRepo)
        {
            _workContext = workContext;
            _ticketItemRepo = ticketItemRepo;
            _ticketRepo = ticketRepo;
        }

        public async Task<TicketMetaData> Handle(GetUserTicketsInfoQuery request, CancellationToken cancellationToken)
        {
            return new TicketMetaData()
            {
                UnReadTicketItemCount = await _ticketItemRepo.GetQueriable()
                    .CountAsync(x => !x.IsSeen && x.Ticket.UserId == _workContext.CurrentUserId &&x.UserId!=_workContext.CurrentUserId,
                        cancellationToken),
                AllCount = _ticketRepo.GetQueriable().Count(x=>x.UserId==_workContext.CurrentUserId)
            };
        }
    }
}