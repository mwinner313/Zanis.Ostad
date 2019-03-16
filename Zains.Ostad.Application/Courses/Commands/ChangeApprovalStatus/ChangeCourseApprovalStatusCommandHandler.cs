using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.Courses.Commands.ChangeApprovalStatus
{
    public class ChangeCourseApprovalStatusCommandHandler:IRequestHandler<ChangeCourseApprovalStatusCommand,Response>
    {
        private readonly IRepository<Course, long> _repository;
        private readonly IRepository<Notification, long> _notificationRepo;
        public ChangeCourseApprovalStatusCommandHandler(IRepository<Course, long> repository, IRepository<Notification, long> notificationRepo)
        {
            _repository = repository;
            _notificationRepo = notificationRepo;
        }

        public async Task<Response> Handle(ChangeCourseApprovalStatusCommand request, CancellationToken cancellationToken)
        {
            var course = _repository.GetQueriable().First(x => x.Id == request.CourseId);
            course.ApprovalStatus = request.CourseApprovalStatus;
            if (request.UpdateMessage)
                course.AdminMessageForTeacher = request.AdminMessageForTeacher;
            await SendNotifToTeacher(request);
            await _repository.EditAsync(course);
            return Response.Success();
        }

        private async Task SendNotifToTeacher(ChangeCourseApprovalStatusCommand request)
        {
            await _notificationRepo.AddAsync(new Notification
            {
                RelatedItemType = NotificationRelatedItemType.Course,
                ReceiverId = GetCourseOwnerId(request.CourseId),
                Text = "وضعیت دوره آموزشی شما توسط مدیر سیستم تعیین شد",
                JsonExtraData = JsonConvert.SerializeObject(request)
            });
        }

        private long GetCourseOwnerId(long courseId)
        {
            return _repository.GetQueriable().Include(x => x.TeacherLessonMapping).First(x => x.Id == courseId)
                .TeacherLessonMapping.TeacherId;
        }
    }
}