using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Teachers.Queries.GetNotifications;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class NotificationController : BaseController
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<PagenatedList<NotificationViewModel>>> Get([FromQuery]GetNotificationsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
