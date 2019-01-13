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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetTicketDetailsQueryHandler(IRepository<Ticket, long> ticketRepo, IWorkContext workContext,
            IMapper mapper, IMediator mediator)
        {
            _ticketRepo = ticketRepo;
            _workContext = workContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TicketViewModel> Handle(GetTicketDetailsQuery request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new MarkTicketAsSeenCommand()
            {
                TicketId = request.TicketId
            }, cancellationToken);
            return await _ticketRepo.GetQueriable()
                .Where(x => x.UserId == _workContext.CurrentUserId)
                .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                .SingleAsync(x => x.Id == request.TicketId, cancellationToken);
        }
    }
}