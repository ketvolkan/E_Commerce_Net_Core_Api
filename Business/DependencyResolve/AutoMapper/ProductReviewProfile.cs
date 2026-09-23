namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.ProductReviews;
using global::AutoMapper;

public class ProductReviewProfile : Profile
{
    public ProductReviewProfile()
    {
        CreateMap<ProductReview, CreateProductReviewDto>();
        CreateMap<CreateProductReviewDto, ProductReview>();
    }
}
