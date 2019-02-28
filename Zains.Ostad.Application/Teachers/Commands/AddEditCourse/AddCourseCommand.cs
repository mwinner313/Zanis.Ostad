using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddCourseCommand:IRequest<Response>
    {
        public int Price { get; set; }
        public string Description { get; set; }
        public int CourseTitleId { get; set; }
        public List<CourseItemDto> Items { get; set; }
    }
}