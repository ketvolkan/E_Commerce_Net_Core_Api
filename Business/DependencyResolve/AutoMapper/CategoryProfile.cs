namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Categories;
using global::AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryListDto>();
        CreateMap<Category, CategoryDetailDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}
