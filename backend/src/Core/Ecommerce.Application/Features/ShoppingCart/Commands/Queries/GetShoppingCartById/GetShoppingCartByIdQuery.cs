using Ecommerce.Application.Features.ShoppingCart.Commands.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartVm>
    {
        public Guid? ShoppingCartId { get; set; }
        public GetShoppingCartByIdQuery(Guid? shoppingCartId)
        {
            ShoppingCartId = shoppingCartId ?? throw new ArgumentNullException(nameof(shoppingCartId));
        }
    }
}
