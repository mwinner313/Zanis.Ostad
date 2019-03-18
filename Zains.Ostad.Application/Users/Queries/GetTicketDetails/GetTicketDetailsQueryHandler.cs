using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zains.Ostad.Application.Users.Commands.MarkTicketAsSeen;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Queries.GetTicketDetails
{
    public class GetTicketDetailsQueryHandler : IRequestHandler<GetTicketDetailsQuery, TicketViewModel>
    {
        private readonly IRepository<Ticket, long> _ticketRepo;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public GetTicketDetailsQueryHandler(IRepository<Ticket, long> ticketRepo, IWorkContext workContext,
            IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<TicketViewModel> Handle(GetTicketDetailsQuery request, CancellationToken cancellationToken)
        {
          
            return await _ticketRepo.GetQueryable()
                .Where(x => x.UserId == _workContext.CurrentUserId)
                .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                .SingleAsync(x => x.Id == request.TicketId, cancellationToken);
        }
    }
}