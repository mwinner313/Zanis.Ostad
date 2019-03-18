using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Queries.GetUserTicketsSummery
{
    public class GetUserTicketsSummeryQueryHandler : IRequestHandler<GetUserTicketsSummeryQuery, TicketMetaData>
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IRepository<Ticket, long> _ticketRepo;


        public GetUserTicketsSummeryQueryHandler(IWorkContext workContext, IRepository<TicketItem, long> ticketItemRepo,
            IRepository<Ticket, long> ticketRepo)
        {
            _workContext = workContext;
            _ticketItemRepo = ticketItemRepo;
            _ticketRepo = ticketRepo;
        }

        public async Task<TicketMetaData> Handle(GetUserTicketsSummeryQuery request,
            CancellationToken cancellationToken)
        {
            return new TicketMetaData
            {
                UnReadTicketItemCount = _ticketRepo.GetQueryable().Where(x => x.UserId == _workContext.CurrentUserId)
                    .Sum(x => x.TicketOwnerUnReadedMessagesCount),
                AllCount = _ticketRepo.GetQueryable().Count(x => x.UserId == _workContext.CurrentUserId)
            };
        }
    }
}