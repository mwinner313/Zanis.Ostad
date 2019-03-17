using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.Tickets.Commands.ChangeTicketCategory
{
    public class ChangeTicketCategoryCommandHandler : IRequestHandler<ChangeTicketCategoryCommand, Response>
    {
        private readonly IRepository<Ticket, long> _repository;

        public ChangeTicketCategoryCommandHandler(IRepository<Ticket, long> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(ChangeTicketCategoryCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetById(request.TicketId);
            ticket.CategoryId = request.CategoryId;
            await _repository.EditAsync(ticket);
            return Response.Success();
        }
    }
}