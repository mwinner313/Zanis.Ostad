using MediatR;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseList
{
    public class GetAllCoursesOverView:IRequest<AllCoursesOverViewQuery>
    {
    }

    public class AllCoursesOverViewQuery
    {
        public long PendingToApproveByAdmin { get; set; }
        public long ApprovedByAdminOrActivatedByTeacher { get; set; }
        public long RejectedByAdmin { get; set; }
        public long DeactivatedByTeacher { get; set; }
    }
}