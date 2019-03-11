using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Lessons.Dtos;

namespace Zains.Ostad.Application.Lessons.Queries.GetLessonList
{
    public class GetLessonListWithDetailsQuery : IRequest<List<LessonFieldViewModel>>
    {
        public string Term { get; set; }
        public long? FieldId { get; set; }
        public long? GradeId { get; set; }
    }
}