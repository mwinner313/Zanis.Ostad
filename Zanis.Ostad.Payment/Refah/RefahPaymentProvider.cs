using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RefahVerify;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zanis.Ostad.Payment.Refah
{
    public class RefahPaymentProvider:IPaymentProvider
    {
        private string _mid;
        private string _paymentRedirectUrl;

        public RefahPaymentProvider(IConfiguration configuration)
        {
            _mid = configuration["refahMerchantId"];
            _paymentRedirectUrl =configuration["sepSite"];
        }

        public OrderPayRequestResponse CreatePayment(Order order, string returnUrl)
        {
            return new RefahPaymentRequestResult()
            {
                MID = _mid,
                Status = ResponseStatus.Success,
                Amount = order.OrderItems.Sum(x=>x.Price),
                PaymentRedirectUrl = _paymentRedirectUrl,
                ResNumOrOrderId = order.Id,
                ReturnRedirectUrl = GetCallBackUrl(returnUrl,order.Id)
            };
        }
        private static string GetCallBackUrl(string returnUrl,long orderId)
        {
            var callBackUrl = returnUrl;
            callBackUrl = callBackUrl + ((callBackUrl.Contains("?") ? "&" : "?") + "orderId=" + orderId);
            return callBackUrl;
        }
        public Response Confirm(PaymentConfirmationContext context,Order order)
        {
            var ctx = (RefahConfirmationContext) context;
            SaveRefNumAndTraceNo(ctx,order);
            if (ctx.State != "OK")
                return Response.Failed();
            var amountIfSuccessErrorCodeIfError = CheckWithBank(ctx);
            return order.OrderItems.Sum(x=>x.Price) == amountIfSuccessErrorCodeIfError
                ? Response.Success()
                : Response.Failed();
        }

        private void SaveRefNumAndTraceNo(RefahConfirmationContext ctx, Order order)
        {
            order.ExtraData = JsonConvert.SerializeObject(ctx);
        }

        private long CheckWithBank(RefahConfirmationContext ctx)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            var srv = new PaymentIFBindingSoapClient(PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            var result = (long) srv.verifyTransactionAsync(ctx.RefNum, _mid).Result;
            return result;
        }
    }
}