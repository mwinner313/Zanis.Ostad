using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.Carts.Dto;

namespace Zains.Ostad.Application.Carts.Queries
{
    public class GetCartItemsQuery:IRequest<List<CartItemDto>>
    {
     
    }
}