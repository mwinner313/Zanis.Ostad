using System;
using MediatR;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Carts.Commands
{
    public class RemoveCartItemCommand:IRequest<Response>
    {
     
        public Guid CartItemId { get; set; }
    }
}