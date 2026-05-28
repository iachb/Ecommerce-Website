using FluentValidation;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.FullAddress).NotEmpty().WithMessage("Full address is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
        }
    }
}
