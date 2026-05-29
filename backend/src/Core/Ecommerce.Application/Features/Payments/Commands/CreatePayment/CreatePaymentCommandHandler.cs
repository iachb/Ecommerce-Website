using AutoMapper;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OrderVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderVm> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var orderToPay = await _unitOfWork.Repository<Order>().GetEntityAsync(
                predicate: x => x.Id == request.OrderId,
                includes: null,
                disableTracking: false
                );

            orderToPay.Status = OrderStatus.Completed;
            _unitOfWork.Repository<Order>().UpdateEntity(orderToPay);

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                predicate: x => x.ShoppingCartMasterId == request.ShoppingCartMasterId
                );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);
            await _unitOfWork.Complete();

            return _mapper.Map<OrderVm>(orderToPay);
        }
    }
}
