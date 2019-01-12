using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class Lesson: BaseEntity<long>
    {
        public string LessonName { get; set; }
        public string LessonCode { get; set; }
        public ICollection<LessonExamMapping> ExamSamples { get; set; }
        public ICollection<LessonFieldMapping> Fields { get; set; }
        public int ExamSamplesPrice { get; set; }
    }
}