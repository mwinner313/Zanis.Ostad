using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.AnswerTicket
{
    public class AnswerTicketCommandHandler : IRequestHandler<AnswerTicketCommand, Response<TicketItemViewModel>>
    {
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IRepository<Ticket, long> _ticketRepo;

        public AnswerTicketCommandHandler(IWorkContext workContext, IRepository<TicketItem, long> ticketItemRepo, IRepository<Ticket, long> ticketRepo, IMapper mapper)
        {
            _workContext = workContext;
            _ticketItemRepo = ticketItemRepo;
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }

        public async Task<Response<TicketItemViewModel>> Handle(AnswerTicketCommand request,
            CancellationToken cancellationToken)
        {
            var ticketItem = CreateTicketItem(request);
            await UpdateTicketStats(ticketItem);
            await _ticketItemRepo.AddAsync(ticketItem);
            return Response<TicketItemViewModel>.Success(_ticketItemRepo.GetQueriable().Include(x => x.User).ProjectTo
                <TicketItemViewModel>(_mapper.ConfigurationProvider).Single(x => x.Id == ticketItem.Id));
        }

        private async Task UpdateTicketStats(TicketItem ticketItem)
        {
            var ticket = await _ticketRepo.GetById(ticketItem.TicketId);
            ticket.LastMessageText = ticketItem.Message;
            ticket.UpdatedOn = DateTime.Now;
            ticket.TicketOwnerUnReadedMessagesCount++;
            await _ticketRepo.EditAsync(ticket);
        }

        private TicketItem CreateTicketItem(AnswerTicketCommand request)
        {
            return new TicketItem
            {
                TicketId = request.TicketId,
                Message = request.Message,
                UserId = _workContext.CurrentUserId
            };
        }
    }
}