namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductReviews;

using Core.Utilities.Paging;

public interface IProductReviewService
{
    IDataResult<List<CreateProductReviewDto>> GetAll();
    IDataResult<PagedResult<CreateProductReviewDto>> GetPaged(PageRequest pageRequest);
    IDataResult<PagedResult<CreateProductReviewDto>> GetPagedByProductId(int productId, PageRequest pageRequest);
    IDataResult<CreateProductReviewDto> GetById(int id);
    IResult Add(CreateProductReviewDto createProductReviewDto);
    IResult Update(CreateProductReviewDto updateProductReviewDto);
    IResult Delete(int id);
}
