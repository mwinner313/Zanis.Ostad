using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Tickets.Commands.AnswerTicket;
using Zains.Ostad.Application.Tickets.Commands.ChangeTicketCategory;
using Zains.Ostad.Application.Tickets.Commands.ChangeTicketState;
using Zains.Ostad.Application.Tickets.Commands.EditAnswer;
using Zains.Ostad.Application.Tickets.Commands.MarkUserSendedTicketITemsAsSeen;
using Zains.Ostad.Application.Tickets.Dtos;
using Zains.Ostad.Application.Tickets.Queries.GetUsersSendedTickets;
using Zains.Ostad.Application.Tickets.Queries.GetUserTicketDetails;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Authorize(Roles = "Admin,Operator")]
    [Route("api/tickets")]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TicketDto>> Get([FromQuery] GetUsersSendedTicketsQuery query) =>
            Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketViewModel>> GetById(long id)
        {
            await _mediator.Send(new MarkTicketITemsAsReadCommand
            {
                TicketId = id
            });
            return Ok(await _mediator.Send(new GetUserTicketDetailsQuery
            {
                TicketId = id
            }));
        }

        [HttpPatch("changestate")]
        public async Task<ActionResult<TicketItemViewModel>> PatchState([FromBody] ChangeTicektStateCommand cmd) =>
            Ok(await _mediator.Send(cmd));

        [HttpPatch("changecategory")]
        public async Task<ActionResult<TicketItemViewModel>> PatchCategory([FromBody] ChangeTicketCategoryCommand cmd) =>
            Ok(await _mediator.Send(cmd));

        [HttpPost("ticketItems")]
        public async Task<ActionResult<TicketItemViewModel>> PostItem([FromBody] AnswerTicketCommand cmd) =>
            Ok(await _mediator.Send(cmd));

        [HttpPatch("ticketItems")]
        public async Task<ActionResult<TicketItemViewModel>> PatchItem([FromBody] EditAnswerCommand cmd) =>
            Ok(await _mediator.Send(cmd));
    }
}
