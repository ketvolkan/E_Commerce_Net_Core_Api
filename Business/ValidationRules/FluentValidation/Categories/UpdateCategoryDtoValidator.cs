using FluentValidation;
using Entities.Dtos.Categories;

namespace Business.ValidationRules.FluentValidation.Categories
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(150);
        }
    }
}
