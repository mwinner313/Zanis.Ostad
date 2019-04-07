using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Zains.Ostad.Application.Carts.Dto;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class CartProfile:Profile
    {
        public CartProfile()
        {
        }
        public static Expression<Func<CartItem, CartItemDto>> Projection
        {
            get { return x => new CartItemDto
            {
                CourseId = x.CourseId.HasValue?x.CourseId:null,
                LessonExamId = x.LessonExamId.HasValue?x.LessonExamId:null,
                ItemType = x.ItemType,
                Id = x.Id,
                ProductTitle=
                    x.ItemType==ProductType.LessonExam?" نمونه سوالات "+x.LessonExam.LessonName :x.Course.CourseTitle.Name + " - " +x.Course.Lessons.First().Lesson.Lesson.LessonName,
                Price=x.ItemType==ProductType.LessonExam?x.LessonExam.ExamSamplesPrice:x.Course.Price
         }; }
        }
    }
}