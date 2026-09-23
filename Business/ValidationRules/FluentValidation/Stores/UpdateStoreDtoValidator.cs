using FluentValidation;
using Entities.Dtos.Stores;

namespace Business.ValidationRules.FluentValidation.Stores
{
    public class UpdateStoreDtoValidator : AbstractValidator<UpdateStoreDto>
    {
        public UpdateStoreDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        }
    }
}
