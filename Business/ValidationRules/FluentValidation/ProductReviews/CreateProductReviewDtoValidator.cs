using FluentValidation;
using Entities.Dtos.ProductReviews;

namespace Business.ValidationRules.FluentValidation.ProductReviews
{
    public class CreateProductReviewDtoValidator : AbstractValidator<CreateProductReviewDto>
    {
        public CreateProductReviewDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Rating).InclusiveBetween(1,5);
        }
    }
}
