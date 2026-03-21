using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UpdateUserCommandHandler(IAuthService authService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (updateUser == null)
            {
                throw new BadRequestException("User not found.");
            }

            updateUser.Name = request.Name;
            updateUser.Surname = request.Surname;
            updateUser.Email = request.Email;
            updateUser.AvatarUrl = request.ImageUrl ?? updateUser.AvatarUrl;

            var result = await _userManager.UpdateAsync(updateUser);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user.");
            }

           var userByEmail  = await _userManager.FindByEmailAsync(request.Email!);
           var roles = await _userManager.GetRolesAsync(userByEmail!);
            return new AuthResponse
            {
                Id = userByEmail!.Id,
                Name = userByEmail.Name,
                Surname = userByEmail.Surname,
                Email = userByEmail.Email,
                PhoneNumber = userByEmail.PhoneNumber,
                Avatar = userByEmail.AvatarUrl,
                Token = _authService.CreateToken(userByEmail, roles),
                Roles = roles
            };
        }
    }
}
