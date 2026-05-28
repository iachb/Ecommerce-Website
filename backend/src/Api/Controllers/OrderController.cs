using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using MediatR;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<AddressVm>> CreateAddress(CreateAddressCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
