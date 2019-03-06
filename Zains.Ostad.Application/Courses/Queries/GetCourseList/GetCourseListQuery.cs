using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery:Pagenation,IRequest<PagenatedList<CourseDto>>
    {
    }
}