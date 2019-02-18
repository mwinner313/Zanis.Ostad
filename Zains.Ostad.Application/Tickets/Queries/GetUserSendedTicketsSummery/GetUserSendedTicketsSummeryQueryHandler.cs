using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Queries.GetUserSendedTicketsSummery
{
    public class GetUserSendedTicketsSummeryQueryHandler:IRequestHandler<GetUserSendedTicketsSummeryQuery,TicketMetaData>
    {
        private readonly IRepository<Ticket, long> _ticketRepository;

        public GetUserSendedTicketsSummeryQueryHandler(IRepository<Ticket, long> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketMetaData> Handle(GetUserSendedTicketsSummeryQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _ticketRepository.GetQueriable();
            if (request.NotSeen)
                dbQuery = dbQuery.Where(x => x.OperatorUnReadedMessagesCount > 0);
            if (!string.IsNullOrEmpty(request.UserName))
                dbQuery = dbQuery.Where(x => x.User.UserName.Contains(request.UserName));
            return new TicketMetaData
            {
                AllCount = dbQuery.Count(),
                UnReadTicketItemCount = dbQuery.Sum(x=>x.OperatorUnReadedMessagesCount)
            };
        }
    }
}