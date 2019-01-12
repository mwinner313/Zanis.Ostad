using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class Course : BaseEntity<long>
    {
        public int Price { get; set; }
        public string Description { get; set; }
        public ICollection<CourseItem> Contents { get; set; }
        public TeacherLessonMapping TeacherLessonMapping { get; set; }
        public long TeacherLessonMappingId { get; set; }
        public CourseTitle CourseTitle { get; set; }
        public int CourseTitleId { get; set; }
        public ICollection<StudentCourseMapping> Students { get; set; }
    }
}