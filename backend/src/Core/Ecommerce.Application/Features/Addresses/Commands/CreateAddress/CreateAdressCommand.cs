using Ecommerce.Application.Features.Addresses.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<AddressVm>
    {
        public string? FullAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
