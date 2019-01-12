using MediatR;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;

namespace Zains.Ostad.Application.Orders.Commands
{
    public class ConfirmOrderPaymentCommand:IRequest<Response>
    {
        public PaymentConfirmationContext context { get; set; }
        public string PaymentStrategy { get; set; }
    }
}