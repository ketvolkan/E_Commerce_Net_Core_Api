using FluentValidation;
using Entities.Dtos.Brands;

namespace Business.ValidationRules.FluentValidation.Brands
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
