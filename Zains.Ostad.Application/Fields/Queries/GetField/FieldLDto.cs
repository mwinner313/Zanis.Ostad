using System.Collections.Generic;
using Zains.Ostad.Application.Lessons.Dtos;
using Zains.Ostad.Application.Lessons.Queries.GetLessonList;

namespace Zains.Ostad.Application.Fields.Queries.GetField
{
    public class FieldLDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? CollegeId { get; set; }
        public int GradeId { get; set; }
        public string GradeTitle { get; set; }
        public List<LessonFieldListViewModel> Lessons { get; set; }
    }
}