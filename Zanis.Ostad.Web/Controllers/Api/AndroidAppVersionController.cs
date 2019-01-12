using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.AndroidAppVersions.Dtos;
using Zains.Ostad.Application.AndroidAppVersions.Queries.GetCurrentVersion;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class AndroidAppVersionController:Controller
    {
        private readonly IMediator _mediator;

        public AndroidAppVersionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<AppVersionViewModel>> Get()
        {
            return Ok(await _mediator.Send(new GetCurrentVersionQuery()));
        }
    }
}
