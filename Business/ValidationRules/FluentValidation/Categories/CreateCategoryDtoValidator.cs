using FluentValidation;
using Entities.Dtos.Categories;

namespace Business.ValidationRules.FluentValidation.Categories
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(150);
        }
    }
}
