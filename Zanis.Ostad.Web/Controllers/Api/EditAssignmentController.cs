using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Edits.Commands.AddEditAssignment;
using Zains.Ostad.Application.Edits.Queries.GetAllEditAssigns;
using Zains.Ostad.Application.Edits.Queries.GetEditorsList;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Core.Dtos;

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

        [HttpGet("editors")]
        public async Task<ActionResult<PagenatedList<UserDto>>> GetEditors(GetEditorsListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        public async Task<ActionResult<PagenatedList<EditAssignmentViewModel>>> Get(GetEditAssignsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("bycourseitem")]
        public async Task<ActionResult<Response>> PostByCourseItem([FromBody]AddEditAssignmentByCourseItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }


    }
}
