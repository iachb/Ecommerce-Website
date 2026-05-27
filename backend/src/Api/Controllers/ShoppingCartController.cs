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
    }
}
