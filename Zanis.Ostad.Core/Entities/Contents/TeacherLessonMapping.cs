using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Contents
{
    public class TeacherLessonMapping: BaseEntity<long>
    {
        public User Teacher { get; set; }
        public LessonFieldMapping LessonFieldMapping { get; set; }
        public long TeacherId { get; set; }
        public long LessonId { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}