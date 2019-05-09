using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AutoMapperProfiles;
using Zains.Ostad.Application.Carts.Dto;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Carts.Queries
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, List<CartItemDto>>
    {
        private readonly IRepository<CartItem, Guid> _cartItemRepository;
        private readonly IWorkContext _workContext;

        public GetCartItemsQueryHandler(IRepository<CartItem, Guid> cartItemRepository, IWorkContext workContext)
        {
            _cartItemRepository = cartItemRepository;
            _workContext = workContext;
        }

        public async Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _cartItemRepository.GetQueryable()
                .Include(x => x.Course)
                .ThenInclude(x => x.Lessons).ThenInclude(x => x.Lesson).ThenInclude(x => x.Lesson)
                .Include(x => x.Course)
                .ThenInclude(x => x.CourseCategory)
                .Include(x => x.LessonExam)
                .Where(x => x.UserId == _workContext.CurrentUserId)
                .ToListAsync();
            return items.Select(x => new CartItemDto
            {
                CourseId = x.CourseId.HasValue ? x.CourseId : null,
                LessonExamId = x.LessonExamId.HasValue ? x.LessonExamId : null,
                ItemType = x.ItemType,
                Id = x.Id,
                ProductTitle =
                    x.ItemType == ProductType.LessonExam
                        ? " نمونه سوالات " + x.LessonExam.LessonName
                        : x.Course.CourseCategory.Name + " - " + x.Course.Lessons.First().Lesson.Lesson.LessonName,
                Price = x.ItemType == ProductType.LessonExam ? x.LessonExam.ExamSamplesPrice : x.Course.Price
            }).ToList();
        }
    }
}