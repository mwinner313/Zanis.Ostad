using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Zains.Ostad.Application.Orders.Commands;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Dtos.Orders;
using Zanis.Ostad.Payment;
using Zanis.Ostad.Payment.Refah;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
            /// <summary>
            /// used by android device
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public async Task<ActionResult<OrderPayRequestResponse>> Create(long id)
        {
            return View(await _mediator.Send(new CreateOrderCommand
            {
                UserId = id,
                PaymentStrategy = PaymentProviders.Refah,
                ReturnUrl = GetDomain() +"/Order/PaymentCompleteAndroid"
            }) as RefahPaymentRequestResult);
        }
        [HttpPost("Order/PaymentCompleteAndroid")]
        public async Task<ActionResult<OrderPayRequestResponse>> PaymentCompleteAndroid([FromForm] RefahConfirmationContext res)
        {
            //redirect to client with querystring
            return Redirect($"zanis://confirm?resNum={res.ResNum}&state={res.State}&stateCode={res.StateCode}&refNum={res.ResNum}");
        }
        [Route("api/Order/Create")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderPayRequestResponse>> Create()
        {
            var domain =  GetDomain();
            return Ok(await _mediator.Send(new CreateOrderCommand
            {
                UserId = User.GetId(),
                PaymentStrategy = PaymentProviders.Refah,
                ReturnUrl = domain + "/Order/PaymentComplete"
            }));
        }
        /// <summary>
        /// client should send request to confirm here
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        [Route("api/Order/Confirm")]
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Response>> Confirm([FromBody]RefahConfirmationContext res)
        {
            return Ok(await _mediator.Send(new ConfirmOrderPaymentCommand
            {
                context = res,
                PaymentStrategy = PaymentProviders.Refah
            }));
        }
        /// <summary>
        /// bank will redirect here
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        [HttpPost("Order/PaymentComplete")]

        public async Task<ActionResult<OrderPayRequestResponse>> PaymentComplete([FromForm] RefahConfirmationContext res)
        {
            //redirect to client with querystring
            return RedirectToAction("PaymentConfirm",res);
        }
        [HttpGet("PaymentComplete")]
        public async Task<ActionResult<OrderPayRequestResponse>> PaymentConfirm()
        {
            return View();
        }

        private string GetDomain()
        {
            var domain = Request.Host.Value;
            if (!domain.StartsWith(Request.Scheme))
                domain =  Request.Scheme + "://" + domain;
            return domain;
        }
    }
}
