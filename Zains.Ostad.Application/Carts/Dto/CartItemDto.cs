using System;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Carts.Dto
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public ProductType ItemType { get; set; }
        public long? CourseId { get; set; }
        public long? LessonExamId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
    }
}