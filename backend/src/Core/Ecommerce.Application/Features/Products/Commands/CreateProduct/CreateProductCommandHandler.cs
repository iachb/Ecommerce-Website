using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);
            await _unitOfWork.Repository<Product>().AddAsync(productEntity);

            await _unitOfWork.Complete();

            if (request.ImageUrls is not null && request.ImageUrls.Count > 0)
            {
                // Set the ProductId for each image before saving
                request.ImageUrls.Select(c => { c.ProductId = productEntity.Id; return c; }).ToList();

                // Map the CreateProductImageCommand list to a list of Image entities and save them
                var images = _mapper.Map<List<Domain.Image>>(request.ImageUrls);

                // Save the images to the database
                _unitOfWork.Repository<Domain.Image>().AddRange(images);

                // Save the changes to the database
                await _unitOfWork.Complete();
            }
            return _mapper.Map<ProductVm>(productEntity);
        }
    }
}
