namespace Business.ValidationRules.FluentValidation;

using Entities.Dtos.Products; 
using global::FluentValidation;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş olamaz.");
        RuleFor(p => p.Name).MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır.");
        RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("Geçerli bir kategori seçiniz.");
        RuleFor(p => p.BrandId).GreaterThan(0).WithMessage("Geçerli bir marka seçiniz.");

        // Görsel ve Varyant alt liste kuralları
        RuleForEach(p => p.Variants).ChildRules(variant =>
        {
            variant.RuleFor(v => v.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");
            variant.RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");
            variant.RuleFor(v => v.StoreId).GreaterThan(0).WithMessage("Geçerli bir mağaza seçiniz.");
        });
    }
}