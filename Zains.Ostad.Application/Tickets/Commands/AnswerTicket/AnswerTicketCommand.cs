using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Tickets.Commands.AnswerTicket
{
    public class AnswerTicketCommand:IRequest<Response<TicketItemViewModel>>
    {
        public string Message { get; set; }
        public long TicketId { get; set; }
    }
}