using Ecommerce.Application.Features.Addresses.Vms;
using MediatR;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Orders.Commands.CreateOrder;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Features.Orders.Queries.GetOrdersById;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("address", Name = "CreateAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        public async Task<ActionResult<AddressVm>> CreateAddress([FromBody] CreateAddressCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderVm))]
        public async Task<ActionResult<OrderVm>> CreateOrder([FromBody] CreateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderVm))]
        public async Task<ActionResult<OrderVm>> UpdateOrder([FromBody] UpdateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderVm))]
        public async Task<ActionResult<OrderVm>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
