using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Users.Commands.MarkTicketAsSeen
{
    public class MarkTicketAsSeenCommandHandler : IRequestHandler<MarkTicketAsSeenCommand, Response>
    {
        private readonly IRepository<Ticket, long> _ticketRepository;
        private readonly IRepository<TicketItem, long> _ticketItemRepo;
        private readonly IWorkContext _workContext;
        private readonly IUnitOfWork _unitOfWork;

        public MarkTicketAsSeenCommandHandler(IRepository<Ticket, long> ticketRepository,
            IRepository<TicketItem, long> ticketItemRepo, IUnitOfWork unitOfWork, IWorkContext workContext)
        {
            _ticketRepository = ticketRepository;
            _ticketItemRepo = ticketItemRepo;
            _unitOfWork = unitOfWork;
            _workContext = workContext;
        }

        public async Task<Response> Handle(MarkTicketAsSeenCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetById(request.TicketId);
            var unReadedTicketItems =
                _ticketItemRepo.GetQueryable().Where(x =>
                    !x.IsSeen && x.TicketId == request.TicketId && x.UserId != _workContext.CurrentUserId).ToList();
            ticket.TicketOwnerUnReadedMessagesCount = 0;
            unReadedTicketItems.ForEach(x => x.IsSeen = true);
            _unitOfWork.Begin();
            await _ticketRepository.EditAsync(ticket);
            foreach (var item in unReadedTicketItems)
                await _ticketItemRepo.EditAsync(item);
            _unitOfWork.Commit();
            return Response.Success();
        }
    }
}