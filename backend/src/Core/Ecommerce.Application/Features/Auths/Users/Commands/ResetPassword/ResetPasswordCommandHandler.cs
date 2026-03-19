using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService authService;
        public ResetPasswordCommandHandler(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            this.authService = authService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByNameAsync(authService.GetSessionUser());
            if (updateUser == null)
            {
                throw new BadRequestException("User not found");
            }

            var resultValidateOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(updateUser, updateUser.PasswordHash!, request.OldPassword!);
            if (resultValidateOldPassword != PasswordVerificationResult.Success)
            {
                throw new BadRequestException("Old password is incorrect");
            }

            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUser, request.NewPassword!);
            updateUser.PasswordHash = hashedNewPassword;

            var resultUpdate = await _userManager.UpdateAsync(updateUser);
            if (!resultUpdate.Succeeded)
            {
                throw new Exception("Failed to reset password");
            }

            return Unit.Value;
        }
    }
}
