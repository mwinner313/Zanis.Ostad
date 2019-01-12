using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class Semester:BaseEntity<int>
    {
        public ICollection<ExamSampleFile> ExamSampleFiles { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }
}