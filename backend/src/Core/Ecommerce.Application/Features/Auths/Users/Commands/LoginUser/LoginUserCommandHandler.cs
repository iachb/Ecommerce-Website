using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!) ?? throw new UnauthorizedException("Email not found.");

            if (!user.IsActive) throw new Exception("Your account is not active. Please contact support."); 

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

            if (!result.Succeeded) throw new UnauthorizedException("Password is incorrect.");

            var mailingAddress = await _unitOfWork.Repository<Address>().GetEntityAsync(
                predicate: x => x.Username == user.UserName
            );

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
                MailingAddress = _mapper.Map<AddressVm>(mailingAddress),
                Roles = roles,
                Token = _authService.CreateToken(user, roles),
            };

        }
    }
}
