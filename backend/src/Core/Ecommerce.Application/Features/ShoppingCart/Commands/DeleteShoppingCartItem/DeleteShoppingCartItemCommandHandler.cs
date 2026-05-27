using AutoMapper;
using Ecommerce.Application.Features.ShoppingCart.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommandHandler : IRequestHandler<DeleteShoppingCartItemCommand, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteShoppingCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ShoppingCartVm> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartItem = await _unitOfWork.Repository<ShoppingCartItem>().GetEntityAsync(
                x => x.Id == request.Id
                );

            await _unitOfWork.Repository<ShoppingCartItem>().DeleteAsync(shoppingCartItem);

            var includes = new List<Expression<Func<Domain.ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Product));

            var shoppingCart = await _unitOfWork.Repository<Domain.ShoppingCart>().GetEntityAsync(
                predicate: x => x.ShoppingCartMasterId == shoppingCartItem.ShoppingCartMasterId,
                includes: includes,
                disableTracking: true
                );
            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
