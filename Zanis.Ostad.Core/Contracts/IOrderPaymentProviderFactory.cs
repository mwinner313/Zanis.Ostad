namespace Zanis.Ostad.Core.Contracts
{
    public interface IOrderPaymentProviderFactory
    {
        IPaymentProvider GetProvider(string paymentStrategy);
    }
}