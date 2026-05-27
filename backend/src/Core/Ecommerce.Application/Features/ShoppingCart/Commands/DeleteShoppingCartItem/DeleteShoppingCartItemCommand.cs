using Ecommerce.Application.Features.ShoppingCart.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCart.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommand : IRequest<ShoppingCartVm>
    {
        public int Id { get; set; }
    }
}
