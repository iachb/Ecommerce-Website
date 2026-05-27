using Ecommerce.Application.Features.ShoppingCart.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommand : IRequest<ShoppingCartVm>
    {
        public Guid? ShoppingCartId { get; set; }
        public List<ShoppingCartItemVm>? ShoppingCartItems { get; set; }
    }
}
