using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly StripeSettings _stripeSettings;
        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IAuthService authService, 
            UserManager<User> userManager, 
            IOptions<StripeSettings> stripeSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var pendingOrder = await _unitOfWork.Repository<Order>().GetEntityAsync(
                predicate: x => x.BuyerUsername == _authService.GetSessionUser() && x.Status == OrderStatus.Pending,
                includes: null,
                disableTracking: true
                );

            // If there is a pending order for the user, delete it before creating a new one
            if (pendingOrder != null)
            {
                await _unitOfWork.Repository<Order>().DeleteAsync(pendingOrder);
            }

            var includes = new List<Expression<Func<Domain.ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Product));

            // Get the shopping cart with the specified ID, including its items and the associated products
            var shoppingCart = await _unitOfWork.Repository<Domain.ShoppingCart>().GetEntityAsync(
                predicate: x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes: includes,
                disableTracking: false
                );

            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser()) ?? throw new Exception("The user is not authenticated");

            var address = await _unitOfWork.Repository<Domain.Address>().GetEntityAsync(
                predicate: x => x.Username == user.UserName,
                includes: null,
                disableTracking: false
                ) ?? throw new Exception("The user does not have a default address");

            // Create a new order address based on the user's default address and add it to the database
            var orderAddress = new OrderAddress
            {
                FullAddress = address.FullAddress,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                State = address.State,
                Username = address.Username
            };

            await _unitOfWork.Repository<OrderAddress>().AddAsync(orderAddress);

            // Calculate the subtotal, tax, shipping price, and total for the order
            var subtotal = Math.Round(shoppingCart.ShoppingCartItems!.Sum(x => x.Quantity * x.Price));
            var tax = Math.Round(subtotal * Convert.ToDecimal(0.18));
            var shippingPrice = subtotal < 100 ? 10 : 25;
            var total = subtotal + tax + shippingPrice;
            var buyerName = $"{user.Name} {user.Surname}";
            var order = new Order(buyerName, user.UserName!, orderAddress, subtotal, total, tax, shippingPrice);

            await _unitOfWork.Repository<Order>().AddAsync(order);

            // Map the shopping cart items to order items and add them to the order
            var items = new List<OrderItem>();
            foreach (var item in shoppingCart.ShoppingCartItems!)
            {
                var orderItem = new OrderItem
                {
                    ProductName = item.Product,
                    ProductId = item.ProductId,
                    ImageUrl = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                };
                items.Add(orderItem);
            }

            _unitOfWork.Repository<OrderItem>().AddRange(items);

            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception("Failed to create order");
            }

            // Configure Stripe API key and create a payment intent for the order
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long) order.Total,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                intent = await service.CreateAsync(options);
                order.PaymentIntentId = intent.Id;
                order.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) order.Total,
                };
                intent = await service.UpdateAsync(order.PaymentIntentId, options, cancellationToken: cancellationToken);
            }

            _unitOfWork.Repository<Order>().UpdateEntity(order);

            var resultOrder = await _unitOfWork.Complete();
            if (resultOrder <= 0)
            {
                throw new Exception("Failed to update order with payment intent");
            }

            return _mapper.Map<OrderVm>(order);
        }
    }
}
