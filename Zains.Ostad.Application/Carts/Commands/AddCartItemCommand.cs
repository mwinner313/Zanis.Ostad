using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zains.Ostad.Application.Carts.Commands
{
    public class AddCartItemCommand:IRequest<Response>
    {
       
        public ProductType ItemType { get; set; }
        public long ItemId { get; set; }
    }
}