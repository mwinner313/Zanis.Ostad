using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.ChangeTicketState
{
    public class ChangeTicektStateCommandHandler : IRequestHandler<ChangeTicektStateCommand, Response>
    {
        private readonly IRepository<Ticket, long> _ticketRepository;

        public ChangeTicektStateCommandHandler(IRepository<Ticket, long> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Response> Handle(ChangeTicektStateCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetById(request.TicketId);
            ticket.State = request.State;
            await _ticketRepository.EditAsync(ticket);
            return Response.Success();
        }
    }
}