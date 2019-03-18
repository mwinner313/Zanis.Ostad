using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.MarkUserSendedTicketITemsAsSeen
{
    public class MarkUserSendedTicketITemsAsSeenCommandHandler:IRequestHandler<MarkTicketITemsAsReadCommand,Response>
    {
        private readonly IRepository<Ticket, long> _ticketRepo;
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IUnitOfWork _unitOfWork;

        public MarkUserSendedTicketITemsAsSeenCommandHandler(IRepository<Ticket, long> ticketRepo, IRepository<TicketItem, long> ticketItemRepo, IUnitOfWork unitOfWork, IWorkContext workContext)
        {
            _ticketRepo = ticketRepo;
            _ticketItemRepo = ticketItemRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(MarkTicketITemsAsReadCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepo.GetById(request.TicketId);
            var unReadedTicketItems =
                _ticketItemRepo.GetQueryable().Where(x =>
                    !x.IsSeen && x.TicketId == request.TicketId && x.UserId == ticket.UserId).ToList();
            ticket.OperatorUnReadedMessagesCount = 0;
            unReadedTicketItems.ForEach(x => x.IsSeen = true);
            _unitOfWork.Begin();
            await _ticketRepo.EditAsync(ticket);
            foreach (var item in unReadedTicketItems)
                await _ticketItemRepo.EditAsync(item);
            _unitOfWork.Commit();
            return Response.Success();
        }
    }
}