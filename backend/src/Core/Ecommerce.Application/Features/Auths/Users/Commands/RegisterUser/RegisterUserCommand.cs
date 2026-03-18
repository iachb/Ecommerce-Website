using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AuthResponse>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoId { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
