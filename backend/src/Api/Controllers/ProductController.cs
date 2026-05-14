using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Features.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        private IManageImageService _manageImageService;

        public ProductController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
        {
            var query = new GetProductListQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationProduct")]
        [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductsQuery query)
        {
            query.Status = Domain.ProductStatus.Active;
            var paginationProduct = await _mediator.Send(query);
            return Ok(paginationProduct);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPost("create", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
        {
            var listPhotoUrls = new List<CreateProductImageCommand>();

            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {
                    var uploadResult = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    if (uploadResult != null)
                    {
                        var photoCommand = new CreateProductImageCommand
                        {
                            Url = uploadResult?.Url
                        };
                        listPhotoUrls.Add(photoCommand);
                    }
                }
            }

            request.ImageUrls = listPhotoUrls;

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("update", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
        {
            var listPhotoUrls = new List<CreateProductImageCommand>();

            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {
                    var uploadResult = await _manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = photo.OpenReadStream(),
                        Name = photo.Name
                    });

                    if (uploadResult != null)
                    {
                        var photoCommand = new CreateProductImageCommand
                        {
                            Url = uploadResult?.Url
                        };
                        listPhotoUrls.Add(photoCommand);
                    }
                }
            }

            request.ImageUrls = listPhotoUrls;

            return await _mediator.Send(request);
        }
    }
}
