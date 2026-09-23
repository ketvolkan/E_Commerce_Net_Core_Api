using FluentValidation;
using Entities.Dtos.Stores;

namespace Business.ValidationRules.FluentValidation.Stores
{
    public class CreateStoreDtoValidator : AbstractValidator<CreateStoreDto>
    {
        public CreateStoreDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        }
    }
}
