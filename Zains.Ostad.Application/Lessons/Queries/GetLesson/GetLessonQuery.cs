using MediatR;
using Zains.Ostad.Application.Lessons.Dtos;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Lessons.Queries.GetLesson
{
    public class GetLessonQuery:IRequest<LessonDto>
    {
        public long LessonId { get; set; }
        public long RequestingUserId { get; set; }
    }
}