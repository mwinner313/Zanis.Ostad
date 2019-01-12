namespace Zanis.Ostad.Core.Dtos.Orders
{
    public abstract class PaymentConfirmationContext
    {
        public virtual long OrderId { get; set; }
    }
}