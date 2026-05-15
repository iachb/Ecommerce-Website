using AutoMapper;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Image.Queries;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Commands.CreateReview;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Category, CategoryVm>();
            CreateMap<Image, ImageVm>();

            // Review
            CreateMap<Review, ReviewVm>();
            CreateMap<CreateReviewCommand, Review>();

            CreateMap<Country, CountryVm>();

            // Product
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.Name))
                .ForMember(p => p.TotalReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageCommand, Image>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
