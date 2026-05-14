using FluentValidation;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");
            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("Stock is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be a non-negative integer.");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Product price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Product price must be a non-negative value.");
        }
    }
}
