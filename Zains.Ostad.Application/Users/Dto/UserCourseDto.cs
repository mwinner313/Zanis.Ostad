using System.Collections.Generic;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLesson;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Users.Dto
{
    public class UserCourseDto
    {
        public long Id { get; set; }
        public string LessonName { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public string Title { get; set; }
        public List<CourseItemDto> Contents { get; set; }
    }
}