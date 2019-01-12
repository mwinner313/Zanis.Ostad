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
        public ICollection<TeacherLessonMapping> TeacherLessonMappings { get; set; }
      
    }
}