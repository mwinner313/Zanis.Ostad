using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class EditCourseCommand: IRequest<Response>
    {
        public long CourseId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int CourseTitleId { get; set; }
        public long LessonFieldId { get; set; }
    }
}