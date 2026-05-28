using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAdressCommandHandler : IRequestHandler<CreateAddressCommand, AddressVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public CreateAdressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<AddressVm> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressRecord = await _unitOfWork.Repository<Address>().GetEntityAsync(
                predicate: x => x.Username == _authService.GetSessionUser(),
                includes: null,
                disableTracking: false
                );

            if (addressRecord == null)
            {
                addressRecord = new Address
                {
                    FullAddress = request.FullAddress,
                    City = request.City,
                    State = request.State,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    Username = _authService.GetSessionUser()
                };
                _unitOfWork.Repository<Address>().AddEntity(addressRecord);
            }
            else
            {
                addressRecord.FullAddress = request.FullAddress;
                addressRecord.City = request.City;
                addressRecord.State = request.State;
                addressRecord.PostalCode = request.PostalCode;
                addressRecord.Country = request.Country;
                _unitOfWork.Repository<Address>().UpdateEntity(addressRecord);
            }

            await _unitOfWork.Complete();

            return _mapper.Map<AddressVm>(addressRecord);
        }
    }
}
