using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zanis.Ostad.Core.Contracts
{
    public interface IPaymentProvider
    {
        OrderPayRequestResponse CreatePayment(Order order, string returnUrl);
        Response Confirm(PaymentConfirmationContext context, Order order);
    }
}