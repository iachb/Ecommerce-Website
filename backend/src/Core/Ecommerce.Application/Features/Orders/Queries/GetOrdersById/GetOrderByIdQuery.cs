using Ecommerce.Application.Features.Orders.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrdersById
{
    public class GetOrderByIdQuery : IRequest<OrderVm>
    {
        public int OrderId { get; set; }
        public GetOrderByIdQuery(int orderId)
        {
            this.OrderId = (orderId == 0) ? throw new ArgumentException(null, nameof(orderId)) : orderId;
        }
    }
}
