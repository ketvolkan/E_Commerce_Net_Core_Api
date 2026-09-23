using FluentValidation;
using Entities.Dtos.ProductImages;

namespace Business.ValidationRules.FluentValidation.ProductImages
{
    public class CreateProductImageDtoValidator : AbstractValidator<CreateProductImageDto>
    {
        public CreateProductImageDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.ImageUrl).NotEmpty();
        }
    }
}
