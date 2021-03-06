using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Admin.Edits.Commands.AddEditAssignment;
using Zains.Ostad.Application.Admin.Edits.Queries.GetAllEditAssigns;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class EditAssignmentsController : Controller
    {
        private readonly IMediator _mediator;

        public EditAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<PagenatedList<EditAssignmentViewModel>>> Get(GetEditAssignsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> PostByCourseItem([FromBody]AddEditAssignmentByCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPatch("change_state")]
        public async Task<ActionResult<Response>> PatchEditState([FromBody]ChangeEditAssignmentStateCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
