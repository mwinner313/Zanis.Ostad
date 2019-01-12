using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class StudentExamSampleMapping:BaseEntity<long>
    {
        public User Student { get; set; }
        public long StudentId { get; set; }
        public Lesson Lesson { get; set; }
        public long LessonId { get; set; }
    }
}