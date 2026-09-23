using FluentValidation;
using Entities.Dtos.ProductVariants;

namespace Business.ValidationRules.FluentValidation.ProductVariants
{
    public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
    {
        public CreateProductVariantDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.StoreId).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
