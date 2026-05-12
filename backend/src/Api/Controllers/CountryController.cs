using Ecommerce.Application.Features.Countries.Queries.GetCountryList;
using Ecommerce.Application.Features.Countries.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("get-all", Name = "GetAllCountries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<CountryVm>))]
        public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetAll()
        {
            var query = new GetCountryListQuery();
            return Ok(await _mediator.Send(query));
        }
    }
}
