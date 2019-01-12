using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos.Orders;

namespace Zanis.Ostad.Payment.Refah
{
    public class RefahPaymentRequestResult:OrderPayRequestResponse
    {
        public string MID { get; set; }
        public long Amount { get; set; }
        public string ReturnRedirectUrl { get; set; }
        public long ResNumOrOrderId { get; set; }
    }
}