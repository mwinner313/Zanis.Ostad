using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Teachers.Queries.GetProducedCourses
{
    public class GetProducedCourseDetailQuery:IRequest<CourseDto>
    {
        public long CourseId { get; set; }
    }
}