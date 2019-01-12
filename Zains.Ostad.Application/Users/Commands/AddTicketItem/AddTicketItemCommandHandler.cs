using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Commands.AddTicketItem
{
    public class AddTicketItemCommandHandler : IRequestHandler<AddTicketItemCommand, Response<TicketItemViewModel>>
    {
        private readonly IRepository<TicketItem, long> _repository;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public AddTicketItemCommandHandler(IRepository<TicketItem, long> repository, IWorkContext workContext,
            IMapper mapper)
        {
            _repository = repository;
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<Response<TicketItemViewModel>> Handle(AddTicketItemCommand request,
            CancellationToken cancellationToken)
        {
            var ticketItem = new TicketItem
            {
                TicketId = request.TicketId,
                UserId = _workContext.CurrentUserId,
                Message = request.Message
            };
            await _repository.AddAsync(ticketItem);
            return Response<TicketItemViewModel>.Success(_mapper.Map<TicketItemViewModel>(_repository.GetQueriable()
                .Include(x => x.User).Single(x => x.Id == ticketItem.Id)));
        }
    }
}