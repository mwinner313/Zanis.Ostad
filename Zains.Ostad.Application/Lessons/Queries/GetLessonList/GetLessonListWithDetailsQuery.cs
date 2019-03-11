using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Lessons.Dtos;

namespace Zains.Ostad.Application.Lessons.Queries.GetLessonList
{
    public class GetLessonListWithDetailsQuery : IRequest<List<LessonFieldViewModel>>
    {
        public string Term { get; set; }
    }

    public class LessonFieldViewModel
    {
        public long Id { get; set; }
        public string LessonName { get; set; }
        public long LessonId { get; set; }
        public string FieldName { get; set; }
        public int FieldId { get; set; }
        public string GradeName { get; set; }
        public int GradeId { get; set; }
    }
}