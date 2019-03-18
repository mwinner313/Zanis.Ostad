using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Edits.Commands;
using Zains.Ostad.Application.Edits.Queries.GetAllEditAssigns;
using Zains.Ostad.Application.Infrastucture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class EditAssignmentController : Controller
    {
        private readonly IMediator _mediator;

        public EditAssignmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagenatedList<EditAssignmentViewModel>>> Get(GetEditAssignsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("by_course_item")]
        public async Task<ActionResult<Response>> PostByCourseItem(AddEditAssignmentByCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPost("by_course")]
        public async Task<ActionResult<Response>> PostByCourse(AddEditAssignmentByCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
