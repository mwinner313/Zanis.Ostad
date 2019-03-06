using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.ActiveDeactiveCourse
{
    public class ActiveCourseCommand:IRequest<Response>
    {
        public long CourseId { get; set; }
    }
}