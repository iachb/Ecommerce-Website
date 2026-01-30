using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductList()
        {
            var query = new GetProductListQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }
    }
}
