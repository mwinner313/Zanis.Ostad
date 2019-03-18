using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Teachers.Commands.ActiveDeactiveCourse
{
    public class ActiveDeactiveCourseCommandHandler : IRequestHandler<ActiveCourseCommand, Response>,
        IRequestHandler<DeactiveCourseCommand, Response>
    {
        private readonly IRepository<Course, long> _repository;
        private readonly IWorkContext _workContext;

        public ActiveDeactiveCourseCommandHandler(IRepository<Course, long> repository, IWorkContext workContext)
        {
            _repository = repository;
            _workContext = workContext;
        }
        public async Task<Response> Handle(DeactiveCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await GetCourse(request.CourseId, cancellationToken);
            if (CanNotChangeApprovalStatus(course, out var failed)) return failed;
            return await ChangeState(course,CourseApprovalStatus.DeactivatedByTeacher);
        }
        public async Task<Response> Handle(ActiveCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await GetCourse(request.CourseId, cancellationToken);
            if (CanNotChangeApprovalStatus(course, out var failed)) return failed;
            return await ChangeState(course,CourseApprovalStatus.ApprovedByAdminOrActivatedByTeacher);
        }

        private async Task<Response> ChangeState(Course course ,CourseApprovalStatus state)
        {
            course.ApprovalStatus = state;
            await _repository.EditAsync(course);
            return Response.Success();
        }

        private async Task<Course> GetCourse(long courseId, CancellationToken cancellationToken)
        {
            return await _repository.GetQueryable()
                .FirstAsync(x =>
                        x.Id == courseId && x.TeacherLessonMapping.TeacherId == _workContext.CurrentUserId,
                    cancellationToken);
        }

        private static bool CanNotChangeApprovalStatus(Course course, out Response res)
        {
            switch (course.ApprovalStatus)
            {
                case CourseApprovalStatus.PendingToApproveByAdmin:
                {
                    res = Response.Failed(
                        "برای تغییر وضعیت این مورد ابتدا محتوا میبایست توسط مدیر سیستم تعیین وضعیت شود");
                    return true;
                }
                case CourseApprovalStatus.RejectedByAdmin:
                {
                    res = Response.Failed(
                        "این مورد توسط مدیر سیستم رد شده شما میتوانید برای باز بینی این مورد درخواست داشته باشید");
                    return true;
                }
            }

            res = null;
            return false;
        }

       
    }
}