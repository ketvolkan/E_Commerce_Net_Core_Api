using FluentValidation;
using Entities.Dtos.Addresses;

namespace Business.ValidationRules.FluentValidation.Addresses
{
    public class UpdateAddressDtoValidator : AbstractValidator<UpdateAddressDto>
    {
        public UpdateAddressDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        }
    }
}
