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

        public  Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            return _cartItemRepository.GetQueryable().Where(x => x.UserId == _workContext.CurrentUserId)
                .Select(CartProfile.Projection).ToListAsync();
        }
    }
}