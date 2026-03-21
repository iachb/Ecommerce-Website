using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateAdminUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<User> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(request.Id!);
            if (updateUser == null)
            {
                throw new BadRequestException("The user does not exist.");
            }
            updateUser.Name = request.Name;
            updateUser.Surname = request.Surname;
            updateUser.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(updateUser);
            if (!result.Succeeded)
            {
                throw new Exception("There was an unexpected error updating the user.");
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);
            if (role == null)
            {
                throw new Exception("The assigned role does not exist.");
            }

            await _userManager.AddToRoleAsync(updateUser, role.Name!);

            return updateUser;
        }
    }
}
