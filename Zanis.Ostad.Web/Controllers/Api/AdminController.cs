using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Admin.Users.Commands.ChangeUserRoles;
using Zains.Ostad.Application.Admin.Users.Queries;

namespace Zanis.Ostad.Web.Controllers.Api
{

    [Authorize(Roles="Admin")]
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

        [HttpPost("change_user_roles")]
        public async Task<IActionResult> PostAssignUserToRole([FromBody]ChangeUserRolesCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
