
using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using Ecommerce.Application.Features.Categories.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("get-all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<CategoryVm>))]
        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetAll()
        {
            var query = new GetCategoryListQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
