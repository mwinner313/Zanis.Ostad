using MediatR;
using Microsoft.AspNetCore.Http;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class UpdateCourseItemByTeacherCommand : IRequest<Response<CourseItemViewModel>>
    {
        public long Id { get; set; }
        public bool IsPreview { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public IFormFile File { get; set; }
    }
}