namespace Business.ValidationRules.FluentValidation.Products;

using Entities.Concrete;
using FluentValidation;
using global::FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş olamaz.");
        RuleFor(p => p.Name).MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır.");
        RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("Geçerli bir kategori seçiniz.");
        RuleFor(p => p.BrandId).GreaterThan(0).WithMessage("Geçerli bir marka seçiniz.");
    }
}