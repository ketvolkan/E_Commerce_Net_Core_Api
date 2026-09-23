namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.ProductQuestions;
using global::AutoMapper;

public class ProductQuestionProfile : Profile
{
    public ProductQuestionProfile()
    {
        CreateMap<ProductQuestion, CreateProductQuestionDto>();
        CreateMap<CreateProductQuestionDto, ProductQuestion>();
        CreateMap<UpdateProductQuestionDto, ProductQuestion>();
    }
}
