using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class LessonExamMapping:BaseEntity<long>
    {
        public Lesson Lesson { get; set; }
        public long LessonId { get; set; }
        public ExamSampleFile ExamSampleFile { get; set; }
        public int ExamSampleFileId { get; set; }
    }
}