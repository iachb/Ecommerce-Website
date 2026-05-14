using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using Ecommerce.Application.Exceptions;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));
            await _unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            if (request.ImageUrls is not null && request.ImageUrls.Count > 0)
            {
                // Remove existing images
                var imagesToRemove = await _unitOfWork.Repository<Domain.Image>().GetAsync(i => i.ProductId == request.Id);
                _unitOfWork.Repository<Domain.Image>().DeleteRange(imagesToRemove);

                // Add new images
                request.ImageUrls.Select(c => { c.ProductId = request.Id; return c; }).ToList();
                var images = _mapper.Map<List<Domain.Image>>(request.ImageUrls);
                _unitOfWork.Repository<Domain.Image>().AddRange(images);
                // Save changes after updating images
                await _unitOfWork.Complete();
            }

            return _mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
