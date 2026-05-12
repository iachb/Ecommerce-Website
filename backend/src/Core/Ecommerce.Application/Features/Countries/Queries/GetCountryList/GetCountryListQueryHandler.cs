using AutoMapper;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, IReadOnlyList<CountryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCountryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CountryVm>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countries = await _unitOfWork.Repository<Country>().GetAsync(
                predicate: null,
                orderBy: x => x.OrderBy(y => y.Name),
                includeString: string.Empty,
                disableTracking: false
                );
            return _mapper.Map<IReadOnlyList<Country>, IReadOnlyList<CountryVm>>(countries);
        }
    }
}
