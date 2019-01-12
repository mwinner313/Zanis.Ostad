using System;
using Microsoft.Extensions.DependencyInjection;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Payment.Refah;

namespace Zanis.Ostad.Payment
{
    public class OrderPaymentProviderFactory : IOrderPaymentProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderPaymentProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProvider GetProvider(string paymentStrategy)
        {
            switch (paymentStrategy)
            {
                case PaymentProviders.Refah:
                    return _serviceProvider.GetRequiredService<RefahPaymentProvider>();
            }
            throw new ArgumentOutOfRangeException($"no payment privder registered for requested strategy => '{paymentStrategy}'" );
        }
    }

    public class PaymentProviders
    {
        public const string Refah = nameof(Refah);
    }
}