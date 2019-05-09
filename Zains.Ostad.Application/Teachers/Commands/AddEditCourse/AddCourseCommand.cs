using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Common;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddCourseCommand:IRequest<Response<CourseDto>>
    {
        public int Price { get; set; }
        public string Description { get; set; }
        public string TeacherMessageForAdmin { get; set; }
        public int CourseCategoryId { get; set; }
        public string Title { get; set; }
        public List<long> LessonFieldIds { get; set; }
        public string Permalink => Title.ToUrlSegment();
    }
}