using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Queries.GetTicketes
{
    public class GetTicketsQueryHandler:IRequestHandler<GetTicketsQuery,List<TicketListViewModel>>
    {
        private readonly IRepository<Ticket, long> _ticketRepo;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public GetTicketsQueryHandler(IRepository<Ticket, long> ticketRepo, IWorkContext workContext, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _workContext = workContext;
            _mapper = mapper;
        }

        public Task<List<TicketListViewModel>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            return _ticketRepo.GetQueryable()
                .Where(x => x.UserId == _workContext.CurrentUserId).OrderByDescending(x => x.UpdatedOn)
                .Pagenate(request).ProjectTo<TicketListViewModel>(_mapper.ConfigurationProvider) 
             .ToListAsync(cancellationToken);
        }
    }
}