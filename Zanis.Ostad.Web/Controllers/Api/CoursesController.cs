using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Courses.Commands.AddCourseItem;
using Zains.Ostad.Application.Courses.Commands.ChangeApprovalStatus;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Courses.Queries.GetCourseDetails;
using Zains.Ostad.Application.Courses.Queries.GetCourseList;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class CoursesController:Controller
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById (long id)
        {
            return Ok(await _mediator.Send(new GetCourseDetailsQuery
            {
                CourseId = id,
                CurrentUserId =  User.GetId()
            }));
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpGet]
        public async Task<ActionResult<PagenatedList<CourseDto>>> Get(GetCourseListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpPost("courseItem")]
        public async Task<ActionResult> PostCourseItem([FromForm]AddCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpPut("courseItem")]
        public async Task<ActionResult> PutCourseItem([FromForm] EditCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpGet("overview")]
        public async Task<ActionResult<AllCoursesOverViewQuery>> GetOverview(GetAllCoursesOverView query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpPatch("change_approval_status")]
        public async Task<ActionResult<Response>> ChangeStatus([FromBody]ChangeCourseApprovalStatusCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
