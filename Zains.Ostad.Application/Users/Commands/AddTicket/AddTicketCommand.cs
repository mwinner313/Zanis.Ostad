using MediatR;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Users.Commands.AddTicket
{
    public class AddTicketCommand:IRequest<Response<TicketViewModel>>
    {
        public int? CategoryId { get; set; }
        public long? CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}