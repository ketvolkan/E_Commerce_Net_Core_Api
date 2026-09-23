using FluentValidation;
using Entities.Dtos.Addresses;

namespace Business.ValidationRules.FluentValidation.Addresses
{
    public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
            RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        }
    }
}
