using AutoMapper;
using Ecommerce.Application.Features.ShoppingCart.Commands.Vms;
using Ecommerce.Application.Persistence;
using System.Linq.Expressions;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQueryHandler : IsdRequestHandler<GetShoppingCartByIdQuery, ShoppingCartVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Domain.ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Product));

            var shoppingCart = await _unitOfWork.Repository<Domain.ShoppingCart>().GetEntityAsync(
                predicate: x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes: includes,
                disableTracking: true
                );

            if (shoppingCart is null)
            {
                shoppingCart = new Domain.ShoppingCart
                {
                    ShoppingCartMasterId = request.ShoppingCartId,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };

                await _unitOfWork.Repository<Domain.ShoppingCart>().AddAsync(shoppingCart);
                await _unitOfWork.Complete();
            }
            return _mapper.Map<ShoppingCartVm>(shoppingCart);
        }
    }
}
