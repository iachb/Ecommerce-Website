using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {
        private readonly UserManager<User> _userManager;
        public ResetPasswordByTokenCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            if(!string.Equals(request.Password, request.ConfirmPassword))
                throw new BadRequestException("Passwords do not match.");
            var updateUser = await _userManager.FindByEmailAsync(request.Email!);
            if (updateUser == null)
                throw new BadRequestException("User not found.");

            var tokenResult = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(request.Token!));

            var result = await _userManager.ResetPasswordAsync(updateUser, tokenResult, request.Password!);
            if (!result.Succeeded)
                throw new Exception("Error resetting password.");

            return "Password reset successful.";
        }
    }
}
