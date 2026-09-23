namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Products;
using global::AutoMapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

        CreateMap<ProductImage, ProductImageDto>();

        CreateMap<ProductVariant, ProductVariantDto>()
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name));

        CreateMap<CreateProductDto, Product>();
        CreateMap<CreateProductImageDto, ProductImage>();
        CreateMap<CreateProductVariantDto, ProductVariant>();

        CreateMap<UpdateProductDto, Product>();
        CreateMap<UpdateProductImageDto, ProductImage>();
        CreateMap<UpdateProductVariantDto, ProductVariant>();
    }
}