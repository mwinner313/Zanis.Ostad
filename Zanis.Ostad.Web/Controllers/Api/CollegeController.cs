using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Colleges.Queries.GetCollegeList;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CollegesController : BaseController
    {
        private readonly IMediator _mediator;

        public CollegesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CollegeListViewModel>>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCollegeDetailsQuery {CollegeId = id}));
        }

        [HttpGet]
        public async Task<ActionResult<List<CollegeListViewModel>>> Get(GetCollegesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
