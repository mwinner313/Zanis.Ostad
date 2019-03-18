using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseList
{
    public class GetAllCoursesOverViewQueryHandler : IRequestHandler<GetAllCoursesOverView,AllCoursesOverViewQuery>
    {
        private readonly IRepository<Course,long> _repository;

        public GetAllCoursesOverViewQueryHandler(IRepository<Course, long> repository)
        {
            _repository = repository;
        }

        public async Task<AllCoursesOverViewQuery> Handle(GetAllCoursesOverView request, CancellationToken cancellationToken)
        {
            return new AllCoursesOverViewQuery
            {
                DeactivatedByTeacher = _repository.GetQueryable()
                    .Count(x => x.ApprovalStatus == CourseApprovalStatus.DeactivatedByTeacher),
                ApprovedByAdminOrActivatedByTeacher = _repository.GetQueryable().Count(x =>
                    x.ApprovalStatus == CourseApprovalStatus.ApprovedByAdminOrActivatedByTeacher),
                RejectedByAdmin = _repository.GetQueryable()
                    .Count(x => x.ApprovalStatus == CourseApprovalStatus.RejectedByAdmin),
                PendingToApproveByAdmin = _repository.GetQueryable()
                    .Count(x => x.ApprovalStatus == CourseApprovalStatus.PendingToApproveByAdmin)
            };
        }
    }
}