namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductReviews;

public interface IProductReviewService
{
    IDataResult<List<CreateProductReviewDto>> GetAll();
    IDataResult<CreateProductReviewDto> GetById(int id);
    IResult Add(CreateProductReviewDto createProductReviewDto);
    IResult Update(CreateProductReviewDto updateProductReviewDto);
    IResult Delete(int id);
}
