using FluentValidation;
using Entities.Dtos.Brands;

namespace Business.ValidationRules.FluentValidation.Brands
{
    public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
