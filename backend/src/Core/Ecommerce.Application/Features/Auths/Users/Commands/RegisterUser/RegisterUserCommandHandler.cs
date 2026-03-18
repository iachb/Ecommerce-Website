using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
         
            _authService = authService;
        }
        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;
            if (existingUserByEmail)
            {
                throw new BadRequestException("Email is already in use.");
            }

            var existingUserByName = await _userManager.FindByNameAsync(request.Username!) is null ? false : true;
            if (existingUserByName)
            {
                throw new BadRequestException("Username is already in use.");
            }

            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Username,
                AvatarUrl = request.PhotoUrl
            };

            var result = await _userManager.CreateAsync(user, request.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRole.GenericUser);
                var roles = await _userManager.GetRolesAsync(user);
                return new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Username = user.UserName,
                    Avatar = user.AvatarUrl,
                    Token = _authService.CreateToken(user, roles),
                    Roles = roles
                };

            }

            throw new Exception(string.Join(" ", result.Errors.Select(e => e.Description)));
        }
    }
}
