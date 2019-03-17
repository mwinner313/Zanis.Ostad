using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Tickets.Commands.ChangeTicketCategory
{
    public class ChangeTicketCategoryCommand : IRequest<Response> 
    {
        public long TicketId { get; set; }
        public int CategoryId { get; set; }
    }
}