using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams productParams)
            : base
            (
                 x =>
                    (string.IsNullOrEmpty(productParams.Search) || x.Name!.Contains(productParams.Search)
                        || x.Description!.Contains(productParams.Search)
                    ) &&
                    (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
                    (!productParams.MinPrice.HasValue || x.Price >= productParams.MinPrice) &&
                    (!productParams.MaxPrice.HasValue || x.Price <= productParams.MaxPrice) &&
                    (!productParams.Status.HasValue || x.Status == productParams.Status) &&
                    (!productParams.Rating.HasValue || x.Rating == productParams.Rating)
            )
        {
            AddInclude(x => x.Reviews!);
            AddInclude(x => x.Images!);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name!);
                        break;
                    case "priceAsc":
                        AddOrderBy(p => p.Price!);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price!);
                        break;
                    case "ratingAsc":
                        AddOrderBy(p => p.Rating!);
                        break;
                    case "ratingDesc":
                        AddOrderByDescending(p => p.Rating!);
                        break;
                    default: 
                        AddOrderBy(n => n.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(n => n.CreatedDate!);
            }
        }
    }
}
