using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Tickets.Commands.EditAnswer
{
    public class EditAnswerCommand:IRequest<Response<TicketItemViewModel>>
    {
        public long TicketItemId { get; set; }
        public string Message { get; set; }
    }
}