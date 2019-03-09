using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.MarkNotifiactionAsSeen
{
    public class MarkNotificationAsSeenCommand:IRequest<Response>
    {
        public long NotificationId { get; set; }
    }
}