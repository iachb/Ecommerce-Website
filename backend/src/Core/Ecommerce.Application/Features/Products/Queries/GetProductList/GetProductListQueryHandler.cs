using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>
            {
                p => p.Images!,
                p => p.Reviews!
            };

            var products = await _unitOfWork.Repository<Product>().GetAsync(
                predicate: null,
                orderBy: x => x.OrderBy(y => y.Name),
                includes: includes,
                disableTracking: false
            );

            return new List<Product>(products);
        }
    }
}
