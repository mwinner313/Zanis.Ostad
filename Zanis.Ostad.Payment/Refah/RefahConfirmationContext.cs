using Zanis.Ostad.Core.Dtos.Orders;

namespace Zanis.Ostad.Payment.Refah
{
    public class RefahConfirmationContext:PaymentConfirmationContext
    {
        public string State { get; set; }
        public string StateCode { get; set; }
        public string RefNum { get; set; }
        public long ResNum { get; set; }
        public string TraceNo { get; set; }
        public override long OrderId
        {
            get => ResNum;
            set => ResNum = value;
        }
    }
}