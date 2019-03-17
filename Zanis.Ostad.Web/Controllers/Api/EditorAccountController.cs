using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Editors.Commands.UploadEditedItem;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Dtos;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Authorize(Roles = "Admin,Editor")]
    [Route("api/[controller]")]
    public class EditorAccountController : Controller
    {
        private readonly IMediator _mediator;

        public EditorAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagenatedList<EditAssignmentViewModel>>> Get(GetEditAssignmentsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task<ActionResult<Response>> Post(UploadEditedItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
