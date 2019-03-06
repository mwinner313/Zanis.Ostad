using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zains.Ostad.Application.Carts.Commands;
using Zains.Ostad.Application.Carts.Dto;
using Zains.Ostad.Application.Carts.Queries;
using Zanis.Ostad.Core.Dtos;

namespace Zanis.Ostad.Web.Controllers.Api
{
    [Route("api/cart")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartItemDto>>> Get()
        {
            return Ok(await _mediator.Send(new GetCartItemsQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<List<Response>>> Post([FromBody]AddCartItemCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Response>>> Delete(Guid id)
        {
            var cmd = new RemoveCartItemCommand
            {
                CartItemId = id
            };
            return Ok(await _mediator.Send(cmd));
        }
    }
}
