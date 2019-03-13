using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery:Pagenation,IRequest<PagenatedList<CourseDto>>
    {
        public CourseApprovalStatus? Status { get; set; }
    }
}