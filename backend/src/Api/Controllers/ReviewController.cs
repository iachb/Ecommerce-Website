using Ecommerce.Application.Features.Reviews.Commands.CreateReview;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create", Name = "CreateReview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<ReviewVm>))]
        public async Task<ActionResult<ReviewVm>> CreateReview(CreateReviewCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
