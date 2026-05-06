using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {

        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByTokenQueryHandler(UserManager<User> userManager, IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (user == null)
            {
                throw new Exception("User is not authenticated");
            }
            if (!user.IsActive)
            {
                throw new Exception("User is not active");
            }

            var address = await _unitOfWork.Repository<Address>().GetEntityAsync(x => x.Username == user.UserName);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                Email = user.Email,
                Avatar = user.AvatarUrl,
                MailingAddress = _mapper.Map<AddressVm>(address),
                Token = _authService.CreateToken(user, roles),
                Roles = roles
            };
        }
    }
}
