using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-payment", Name = "CreatePayment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderVm))]
        public async Task<ActionResult<OrderVm>> CreatePayment([FromBody] CreatePaymentCommand request)
        {
            var order = await _mediator.Send(request);
            return Ok(order);
        }
    }
}
