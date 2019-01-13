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
        private readonly IRepository<Ticket, long> _ticketRepository;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public AddTicketItemCommandHandler(IRepository<TicketItem, long> repository, IWorkContext workContext,
            IMapper mapper, IRepository<Ticket, long> ticketRepository)
        {
            _repository = repository;
            _workContext = workContext;
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }

        public async Task<Response<TicketItemViewModel>> Handle(AddTicketItemCommand request,
            CancellationToken cancellationToken)
        {
            var ticketItem = CreateTicketItem(request);
            await _repository.AddAsync(ticketItem);
            await HandleTicketInfo(ticketItem);
            return Response<TicketItemViewModel>.Success(_mapper.Map<TicketItemViewModel>(GetTicketItem(ticketItem)));
        }

        private async Task HandleTicketInfo(TicketItem ticketItem)
        {
            var ticket = await GetTicket(ticketItem.TicketId);
            ticket.LastMessageText = ticketItem.Message;
            ticket.UnReadedMessagesCount =
                _repository.GetQueriable().Count(x => x.TicketId == ticketItem.TicketId && !x.IsSeen);
            await _ticketRepository.EditAsync(ticket);
        }

        private async Task<Ticket> GetTicket(long ticketId)
        {
            return await _ticketRepository.GetById(ticketId);
        }

       

        private TicketItem GetTicketItem(TicketItem ticketItem)
        {
            return _repository.GetQueriable()
                .Include(x => x.User).Single(x => x.Id == ticketItem.Id);
        }

        private TicketItem CreateTicketItem(AddTicketItemCommand request)
        {
            return new TicketItem
            {
                TicketId = request.TicketId,
                UserId = _workContext.CurrentUserId,
                Message = request.Message
            };
        }
    }
}