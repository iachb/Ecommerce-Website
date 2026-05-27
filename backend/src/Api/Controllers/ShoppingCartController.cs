using Ecommerce.Application.Features.ShoppingCart.Commands.DeleteShoppingCartItem;
using Ecommerce.Application.Features.ShoppingCart.Commands.UpdateShoppingCart;
using Ecommerce.Application.Features.ShoppingCart.Queries.GetShoppingCartById;
using Ecommerce.Application.Features.ShoppingCart.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetShoppingCartById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartVm))]
        public async Task<ActionResult<ShoppingCartVm>> GetShoppingCartById(Guid id)
        {
            var shopppingCartId = id == Guid.Empty ? Guid.NewGuid() : id;
            var query = new GetShoppingCartByIdQuery(shopppingCartId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("{id}", Name = "UpdateShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShoppingCartVm>> UpdateShoppingCart(Guid id, [FromBody] UpdateShoppingCartCommand request)
        {
            request.ShoppingCartId = id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpDelete("item/{id}", Name = "DeleteShoppingCartItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShoppingCartVm>> DeleteShoppingCartItem(int id)
        {
            var request = new DeleteShoppingCartItemCommand() { Id = id };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
