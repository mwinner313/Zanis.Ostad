using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Edits;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.Admin.Edits.Commands.AddEditAssignment
{
    public class ChangeEditAssignmentStateCommandHandler : IRequestHandler<ChangeEditAssignmentStateCommand, Response>
    {
        private readonly IRepository<EditAssignment, long> _editAssignRepo;
        private readonly IRepository<Notification, long> _notificationRepo;
        private readonly IRepository<CourseItem, long> _courseItemRepo;

        public ChangeEditAssignmentStateCommandHandler(IRepository<EditAssignment, long> editAssignRepo,
            IRepository<Notification, long> notificationRepo, IRepository<CourseItem, long> courseItemRepo)
        {
            _editAssignRepo = editAssignRepo;
            _notificationRepo = notificationRepo;
            _courseItemRepo = courseItemRepo;
        }

        public async Task<Response> Handle(ChangeEditAssignmentStateCommand request,
            CancellationToken cancellationToken)
        {
            var edit = await _editAssignRepo.GetQueryable().Include(x => x.CourseItem)
                .FirstAsync(x => x.Id == request.EditId, cancellationToken);
            edit.Status = request.Status;
            edit.CourseItem.LatestEditStatus = request.Status;
            await _editAssignRepo.EditAsync(edit);
            await _notificationRepo.AddAsync(new Notification
            {
                ReceiverId = edit.EditorId,
                Text = "وضعیت تدوین شما توسط مدیر سیستم تعیین شد",
                RelatedItemType = NotificationRelatedItemType.EditAssignment,
                JsonExtraData = JsonConvert.SerializeObject(request)
            });
            await _courseItemRepo.EditAsync(edit.CourseItem);
            return Response.Success();
        }
    }
}