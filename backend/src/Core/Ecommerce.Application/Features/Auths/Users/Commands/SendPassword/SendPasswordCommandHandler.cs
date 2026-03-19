using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Models.Email;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Ecommerce.Application.Features.Auths.Users.Commands.SendPassword
{
    public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public SendPasswordCommandHandler(IEmailService emailService, UserManager<User> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
            {
                throw new BadRequestException("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            var emailMessage = new EmailMessage
            {
                To = request.Email!,
                Body = "To reset your password, please click here:",
                Subject = "Password Reset"
            };

            var result = await _emailService.SendEmailAsync(emailMessage, token);
            if (!result)
            {
                throw new Exception("Email coudln't be sent");
            }

            return $"Email was sent to account: {request.Email}";
        }
    }
}
