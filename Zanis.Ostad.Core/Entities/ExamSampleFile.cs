using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class ExamSampleFile:BaseEntity<int>
    {
        public ICollection<LessonExamMapping> Lessons { get; set; }
        public string FilePath { get; set; }
        public ExamSampleFileType Type { get; set; }
        public Semester Semester { get; set; }
        public int SemesterId { get; set; }
    }
}