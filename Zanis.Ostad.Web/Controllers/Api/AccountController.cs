using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Tickets.Dtos;
using Zains.Ostad.Application.Users.Commands.AddTicket;
using Zains.Ostad.Application.Users.Commands.AddTicketItem;
using Zains.Ostad.Application.Users.Commands.ConfirmChangePassword;
using Zains.Ostad.Application.Users.Commands.Login;
using Zains.Ostad.Application.Users.Commands.MarkTicketAsSeen;
using Zains.Ostad.Application.Users.Commands.Register;
using Zains.Ostad.Application.Users.Commands.RequestChangePassword;
using Zains.Ostad.Application.Users.Dto;
using Zains.Ostad.Application.Users.Queries.GetBoughtCourses;
using Zains.Ostad.Application.Users.Queries.GetBoughtLessonExamSamples;
using Zains.Ostad.Application.Users.Queries.GetTicketDetails;
using Zains.Ostad.Application.Users.Queries.GetTicketes;
using Zains.Ostad.Application.Users.Queries.GetUserTicketsSummery;
using Zanis.Ostad.Core.Dtos;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRegisterResponse>> Register([FromBody] RegisterCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPost("RegisterAsTeacher")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRegisterResponse>> RegisterAsTeacher([FromBody] RegisterAsTeacherCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRegisterResponse>> Login([FromBody] LoginCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
        [HttpPost("RequestChangePassword")]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> RequestChangePassword([FromBody] RequestChangePasswordCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPost("ConfirmChangePassword")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginRegisterResponse>> ConfirmChangePassword([FromBody] ConfirmChangePasswordCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }


        [HttpGet("LessonExams")]
        public async Task<ActionResult<PagenatedList<LessonExamDto>>> LessonExams(GetBoughtLessonExamSamplesQuery cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpGet("Courses")]
        public async Task<ActionResult<PagenatedList<UserCourseDto>>> Courses(GetBoughtCoursesQuery cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpGet("Tickets")]
        public async Task<ActionResult<TicketDto>> GetTickets(GetTicketsQuery query)
        {
            return Ok(new TicketDto
            {
                Items = await _mediator.Send(query),
                MetaData = await _mediator.Send(new GetUserTicketsSummeryQuery())
            });
        }

        [HttpPost("Tickets")]
        public async Task<ActionResult<Response<TicketItemViewModel>>> AddTicket([FromBody] AddTicketCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpGet("Tickets/{id}")]
        public async Task<ActionResult<TicketViewModel>> GetTicket(long id)
        {
            await _mediator.Send(new MarkTicketAsSeenCommand
            {
                TicketId = id
            });
            return Ok(await _mediator.Send(new GetTicketDetailsQuery
            {
                TicketId = id
            }));
        }

        [HttpPost("TicketItems")]
        public async Task<ActionResult<Response<TicketItemViewModel>>> AddTicketItem(
            [FromBody] AddTicketItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
