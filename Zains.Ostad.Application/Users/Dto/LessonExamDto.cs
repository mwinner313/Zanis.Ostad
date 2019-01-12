using System.Collections.Generic;
using Zains.Ostad.Application.ExamSamples.Dto;

namespace Zains.Ostad.Application.Users.Dto
{
    public class LessonExamDto
    {
        public long Id { get; set; }
        public string LessonName { get; set; }
        public string LessonCode { get; set; }
        public List<ExamSampleDto> Exams { get; set; }
        public int ExamSamplesPrice { get; set; }
    }
}