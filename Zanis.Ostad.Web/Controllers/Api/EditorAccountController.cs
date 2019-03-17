using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Editors.Queries.GetEditAssignments;
using Zains.Ostad.Application.Infrastucture;

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
            return await _mediator.Send(query);
        }
    }
}
