using MediatR;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos.Orders;

namespace Zains.Ostad.Application.Orders.Commands
{
    public class CreateOrderCommand:IRequest<OrderPayRequestResponse>
    {
        public long UserId { get; set; }
        public string PaymentStrategy { get; set; }
        public string ReturnUrl { get; set; }
    }
}