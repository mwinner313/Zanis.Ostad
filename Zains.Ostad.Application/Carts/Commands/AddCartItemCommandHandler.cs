using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Carts.Commands
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Response>
    {
        private readonly IRepository<CartItem, Guid> _cartItemRepository;
        private readonly IWorkContext _workContext;
        public AddCartItemCommandHandler(IRepository<CartItem, Guid> cartItemRepository, IWorkContext workContext)
        {
            _cartItemRepository = cartItemRepository;
            _workContext = workContext;
        }

        public async Task<Response> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = CreateCartItem(request);
            switch (request.ItemType)
            {
                case ProductType.LessonExam:
                    if (_cartItemRepository.GetQueryable()
                        .Any(x => x.UserId == _workContext.CurrentUserId && x.LessonExamId == request.ItemId))
                        return Response.Success();
                    break;
                case ProductType.TeacherCourse:
                    if (_cartItemRepository.GetQueryable()
                        .Any(x => x.UserId == _workContext.CurrentUserId && x.CourseId == request.ItemId))
                        return Response.Success();
                    break;
            }

            await _cartItemRepository.AddAsync(cartItem);
            return Response.Success();
        }

        private CartItem CreateCartItem(AddCartItemCommand request)
        {
            var cartItem = new CartItem()
            {
                UserId = _workContext.CurrentUserId,
                ItemType = request.ItemType
            };

            switch (request.ItemType)
            {
                case ProductType.LessonExam:
                    cartItem.LessonExamId = request.ItemId;
                    break;
                case ProductType.TeacherCourse:
                    cartItem.CourseId = request.ItemId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return cartItem;
        }
    }
}