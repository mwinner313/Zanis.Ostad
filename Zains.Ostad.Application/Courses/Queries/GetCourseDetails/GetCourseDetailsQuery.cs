using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;

namespace Zains.Ostad.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQuery:IRequest<CourseDto>
    {
        public long CourseId { get; set; }
        public long? CurrentUserId { get; set; }
    }
}