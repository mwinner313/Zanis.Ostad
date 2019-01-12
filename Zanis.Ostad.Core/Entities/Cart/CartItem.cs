using System;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Cart
{
    public class CartItem:BaseEntity<Guid>
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public ProductType ItemType { get; set; }
        public Course Course { get; set; }
        public long? CourseId { get; set; }
        public long? LessonExamId { get; set; }
        public Lesson LessonExam { get; set; }
    }
}