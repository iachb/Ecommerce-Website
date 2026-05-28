using AutoMapper;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Image.Queries;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Commands.CreateReview;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Features.ShoppingCart.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Review
            CreateMap<Review, ReviewVm>();
            CreateMap<CreateReviewCommand, Review>();

            // Product
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.Name))
                .ForMember(p => p.TotalReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageCommand, Image>();
            CreateMap<UpdateProductCommand, Product>();

            // Shopping Cart
            CreateMap<ShoppingCart, ShoppingCartVm>().ForMember(p => p.ShoppingCartId, x => x.MapFrom(a => a.ShoppingCartMasterId));
            CreateMap<ShoppingCartItem, ShoppingCartItemVm>();
            CreateMap<ShoppingCartItemVm, ShoppingCartItem>();

            // Address
            CreateMap<Address, AddressVm>();

            // Order
            CreateMap<Order, OrderVm>();
            CreateMap<OrderItem, OrderItemVm>();
            CreateMap<OrderAddress, AddressVm>();

            // Misc
            CreateMap<Category, CategoryVm>();
            CreateMap<Image, ImageVm>();
            CreateMap<Country, CountryVm>();
        }
    }
}
