using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Lessons.Dtos;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Lessons.Queries.GetLessonList
{
    public class GetLessonListQuery : IRequest<List<LessonFieldListViewModel>>
    {
        public int? FieldId { get; set; }
        public int? GradeId { get; set; }
        public string SearchText { get; set; } 
        public ProductType ProductType { get; set; }
    }
}