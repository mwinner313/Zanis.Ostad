using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.TicketCategories.Dtos;
using Zains.Ostad.Application.TicketCategories.Queries.GetTicketCategories;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class TicketCategoryController:Controller
    {
        private readonly IMediator _mediator;

        public TicketCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketCategoryViewModel>>> Get(GetTicketCategoriesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
