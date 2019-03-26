using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Admin.Users.Queries;

namespace Zanis.Ostad.Web.Controllers.Api
{

    [Authorize]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _mediator.Send(new GetRolesListQuery()));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(GetUsersListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("assign_role_to_user")]
        public async Task<IActionResult> PostAssignUserToRole(GetUsersListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

    }
}
