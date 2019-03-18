using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Notifications;
using Response = Zanis.Ostad.Core.Dtos.Response;

namespace Zains.Ostad.Application.Teachers.Commands.MarkNotifiactionAsSeen
{
    public class MarkNotificationAsSeenCommandHandler:IRequestHandler<MarkNotificationAsSeenCommand,Response>
    {
        private readonly IRepository<Notification, long> _notifRepo;
        private readonly IWorkContext _workContext;
        public MarkNotificationAsSeenCommandHandler(IRepository<Notification, long> notifRepo, IWorkContext workContext)
        {
            _notifRepo = notifRepo;
            _workContext = workContext;
        }

        public async Task<Response> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
        {
            var notif =await _notifRepo.GetQueryable().FirstOrDefaultAsync(x =>
                x.Id == request.NotificationId && x.ReceiverId == _workContext.CurrentUserId, cancellationToken);
            if (notif is null)
                return Response.Failed("چنین پیامی برای کاربر ثبت نشده");
            notif.IsSeen = true;
            await _notifRepo.EditAsync(notif);
            return Response.Success();
        }
    }
}