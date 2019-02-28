using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Queries.GetUsersSendedTickets
{
    public class GetUsersSendedTicketsQueryHandler : IRequestHandler<GetUsersSendedTicketsQuery, TicketDto>
    {
        private readonly IRepository<Ticket, long> _ticketRepository;
        private readonly IMapper _mapper;

        public GetUsersSendedTicketsQueryHandler(IRepository<Ticket, long> ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketDto> Handle(GetUsersSendedTicketsQuery request,
            CancellationToken cancellationToken)
        {
            var dbQuery = _ticketRepository.GetQueriable();
            if (request.State.HasValue)
                dbQuery = dbQuery.Where(x => x.State == request.State.Value);
            if (request.NotSeen)
                dbQuery = dbQuery.Where(x => x.OperatorUnReadedMessagesCount > 0);
            if (request.CategoryId.HasValue)
                dbQuery = dbQuery.Where(x => x.CategoryId ==request.CategoryId);
            if (!string.IsNullOrEmpty(request.Search))
                dbQuery = dbQuery.Where(x => 
                    x.User.UserName.Contains(request.Search)||
                    x.User.FullName.Contains(request.Search) ||
                    x.TicketItems.Any(i=>i.Message.Contains(request.Search))||
                    x.TicketReason.Contains(request.Search)
                );
            return new TicketDto
            {
                Items = await dbQuery.OrderByDescending(x => x.CreatedOn)
                    .ProjectTo<TicketListViewModel>(_mapper.ConfigurationProvider)
                    .Pagenate(request).ToListAsync(cancellationToken),
                MetaData = new TicketMetaData
                {
                    AllCount =  dbQuery.OrderByDescending(x => x.CreatedOn)
                        .Count(),
                    UnReadTicketItemCount =  dbQuery.Sum(x=>x.OperatorUnReadedMessagesCount)
                }
            };
        }
    }
}