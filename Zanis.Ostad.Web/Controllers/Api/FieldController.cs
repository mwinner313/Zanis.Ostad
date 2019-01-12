using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Fields.Queries;
using Zains.Ostad.Application.Fields.Queries.GetFieldsList;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class FieldsController:BaseController
    {
        private readonly IMediator _mediator;

        public FieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<FieldListViewModel>>> Get(GetFieldsListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
