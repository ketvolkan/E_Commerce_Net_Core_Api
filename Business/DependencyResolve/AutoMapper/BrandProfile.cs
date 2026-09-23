namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Brands;
using global::AutoMapper;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandListDto>();
        CreateMap<Brand, BrandDetailDto>();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
    }
}
