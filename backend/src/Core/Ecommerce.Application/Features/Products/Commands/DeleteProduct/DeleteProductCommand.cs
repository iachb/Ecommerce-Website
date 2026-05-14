using Ecommerce.Application.Features.Products.Queries.Vms;
using MediatR;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<ProductVm>
    {
        public int ProductId { get; set; }
        public DeleteProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException("ProductId cannot be zero.", nameof(productId)) : productId;
        }
    }
}
