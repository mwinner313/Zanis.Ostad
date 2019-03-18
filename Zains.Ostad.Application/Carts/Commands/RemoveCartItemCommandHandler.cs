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
    public class RemoveCartItemCommandHandler:IRequestHandler<RemoveCartItemCommand,Response>
    {
        private readonly IRepository<CartItem, Guid> _cartItemRepository;
        private readonly IWorkContext _workContext;
        public RemoveCartItemCommandHandler(IRepository<CartItem, Guid> cartItemRepository, IWorkContext workContext)
        {
            _cartItemRepository = cartItemRepository;
            _workContext = workContext;
        }

        public async Task<Response> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _cartItemRepository.GetQueryable()
                .FirstOrDefault(x => x.UserId == _workContext.CurrentUserId && x.Id == request.CartItemId);
            if (cartItem is null)
                return Response.UnKnown("چنین موردی در سبد خرید شما وجود ندارد");
            await _cartItemRepository.Delete(cartItem.Id);    
            return Response.Success();
        }
    }
}