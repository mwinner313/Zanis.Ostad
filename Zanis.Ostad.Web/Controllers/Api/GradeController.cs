using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Grades.Queries.GetGrades;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class GradesController : BaseController
    {
        private readonly IMediator _mediator;

        public GradesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<GradeViewModel>>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetGradeQuery(){GradeId=id}));
        }
        [HttpGet]
        public async Task<ActionResult<List<GradeViewModel>>> Get(GetGradesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

    }
}
