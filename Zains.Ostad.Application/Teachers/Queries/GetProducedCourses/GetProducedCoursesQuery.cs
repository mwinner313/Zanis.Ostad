using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Teachers.Queries.GetProducedCourses
{
    public class GetProducedCoursesQuery:Pagenation,IRequest<PagenatedList<CourseDto>>
    {
    }
}