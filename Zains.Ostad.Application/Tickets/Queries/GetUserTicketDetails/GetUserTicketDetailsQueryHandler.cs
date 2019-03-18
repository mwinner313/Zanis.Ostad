using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Queries.GetUserTicketDetails
{
    public class GetUserTicketDetailsQueryHandler : IRequestHandler<GetUserTicketDetailsQuery, TicketViewModel>
    {
        private readonly IRepository<Ticket, long> _repository;
        private readonly IMapper _mapper;

        public GetUserTicketDetailsQueryHandler(IRepository<Ticket, long> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<TicketViewModel> Handle(GetUserTicketDetailsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetQueryable().ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Id == request.TicketId,  cancellationToken);
        }
    }
}