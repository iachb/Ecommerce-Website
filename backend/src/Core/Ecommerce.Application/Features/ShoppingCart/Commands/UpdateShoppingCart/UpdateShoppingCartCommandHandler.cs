using AutoMapper;
using Ecommerce.Application.Features.ShoppingCart.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Exceptions;
using MediatR;
using Ecommerce.Domain;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartToUpdate = await _unitOfWork.Repository<Domain.ShoppingCart>().GetEntityAsync(p => p.ShoppingCartMasterId == request.ShoppingCartId ) 
                ?? throw new NotFoundException(nameof(Domain.ShoppingCart), request.ShoppingCartId!);

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId
                );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);

            var shoppingCartItemsToAdd = _mapper.Map<List<ShoppingCartItem>>(shoppingCartItems);
            shoppingCartItemsToAdd.ForEach( x => {
                x.ShoppingCartId = shoppingCartToUpdate.Id;
                x.ShoppingCartMasterId = request.ShoppingCartId;
                });

            _unitOfWork.Repository<ShoppingCartItem>().AddRange(shoppingCartItemsToAdd);

            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception("Failed to update the shopping cart.");
            }

            // Retrieve the updated shopping cart with its items to return the updated state
            var includes = new List<Expression<Func<Domain.ShoppingCart, object>>>();
            includes.Add(x => x.ShoppingCartItems!);

            var shoppingCart = await _unitOfWork.Repository<Domain.ShoppingCart>().GetEntityAsync(
                predicate: x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes: includes,
                disableTracking: true
                ) ?? throw new NotFoundException(nameof(Domain.ShoppingCart), request.ShoppingCartId!);

            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
