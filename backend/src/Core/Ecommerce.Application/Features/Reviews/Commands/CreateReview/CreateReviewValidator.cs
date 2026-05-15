using FluentValidation;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name is required.");
            RuleFor(p => p.Comment)
                .NotNull().WithMessage("Comment is required.");
            RuleFor(p => p.Rating)
               .NotEmpty().WithMessage("Rating is required.");
        }
    }
}
