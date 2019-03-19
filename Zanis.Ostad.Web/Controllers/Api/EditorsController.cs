using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Admin.Edits.Queries.GetEditorEditAssignments;
using Zains.Ostad.Application.Admin.Edits.Queries.GetEditorsList;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    public class EditorsController : BaseController
    {
        private readonly IMediator _mediator;

        public EditorsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<PagenatedList<UserDto>>> Get(GetEditorsListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("edit_assignments")]
        public async Task<ActionResult<PagenatedList<UserDto>>> GetAssginments(GetEditorEditAssignmentQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
