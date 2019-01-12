using System.Collections.Generic;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.ExamSamples.Dto;

namespace Zains.Ostad.Application.Lessons.Dtos
{
    public class LessonDto
    {
        public long Id { get; set; }
        public string LessonName { get; set; }
        public string LessonCode { get; set; }
        public int ExamSamplesPrice { get; set; }
        public List<CourseDto> Courses { get; set; }
        public List<ExamSampleDto> Exams { get; set; }
        public bool IsOwnedByCurrentUser { get; set; }
        public long LessonId { get; set; }
    }
}