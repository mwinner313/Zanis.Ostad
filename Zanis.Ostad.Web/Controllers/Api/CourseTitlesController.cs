using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.CourseTitles.Dtos;
using Zains.Ostad.Application.CourseTitles.Queries;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CourseTitlesController : BaseController
    {
        private readonly IMediator _mediator;
        public CourseTitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<CourseTitleViewModel>>> Get(GetCourseTitlesQuery query)
        {
            return Ok(await     _mediator.Send(query));
        }
    }
}
