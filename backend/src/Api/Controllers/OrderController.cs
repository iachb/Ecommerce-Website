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
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Features.Orders.Queries.PaginationOrders;
using Ecommerce.Application.Contracts.Identity;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public OrderController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpGet("pagination-by-username", Name = "PaginationOrdersByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationVm<OrderVm>))]
        public async Task<ActionResult<PaginationVm<OrderVm>>> PaginationOrdersByUsername([FromQuery] PaginationOrdersQuery paginationOrderParams)
        {
            paginationOrderParams.Username = _authService.GetSessionUser();
            var pagination = await _mediator.Send(paginationOrderParams);
            return Ok(pagination);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("pagination-admin", Name = "PaginationOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationVm<OrderVm>))]
        public async Task<ActionResult<PaginationVm<OrderVm>>> PaginationOrder([FromQuery] PaginationOrdersQuery paginationOrderParams)
        {
            var pagination = await _mediator.Send(paginationOrderParams);
            return Ok(pagination);
        }
    }
}
