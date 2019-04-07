using System.Collections.Generic;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class LessonFieldMapping : BaseEntity<long>
    {
        public Lesson Lesson { get; set; }
        public long LessonId { get; set; }
        public Field Field { get; set; }
        public int FieldId { get; set; }
        public Grade Grade { get; set; }
        public int GradeId { get; set; }
        public ICollection<CourseLessonFieldGradeMapping> Courses { get; set; }
    }

    public class CourseLessonFieldGradeMapping:BaseEntity<long>
    {
        public Course Course { get; set; }
        public long CourseId { get; set; }
        public LessonFieldMapping Lesson { get; set; }
        public long LessonId { get; set; }
    }
}