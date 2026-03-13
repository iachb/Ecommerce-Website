using Ecommerce.Application.Features.Addresses.Vms;

namespace Ecommerce.Application.Features.Auths.Users.Vms
{
    public class AuthResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Avatar { get; set; }
        public AddressVm? MailingAddress { get; set; }
        public ICollection<string>? Roles { get; set; }
    }
}
