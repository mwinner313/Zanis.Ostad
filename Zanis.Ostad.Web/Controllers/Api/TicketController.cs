using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Tickets.Dtos;

namespace Zanis.Ostad.Web.Controllers.Api
{
    public class TicketController:Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<TicketViewModel>>> Get()
        {
            return Ok();
        }
    }
}
