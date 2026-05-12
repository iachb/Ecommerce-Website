using Ecommerce.Application.Features.Categories.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {
    }
}
