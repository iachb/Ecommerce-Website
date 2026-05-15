using Ecommerce.Application.Features.Reviews.Commands.CreateReview;
using Ecommerce.Application.Features.Reviews.Commands.DeleteReview;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete("delete/{id}", Name = "DeleteReview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<Unit>))]
        public async Task<ActionResult<Unit>> DeleteReview(int id)
        {
            var result = await _mediator.Send(new DeleteReviewCommand(id));
            return Ok(result);
        }
    }
}
