namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductQuestions;

using Core.Utilities.Paging;

public interface IProductQuestionService
{
    IDataResult<List<CreateProductQuestionDto>> GetAll();
    IDataResult<PagedResult<CreateProductQuestionDto>> GetPaged(PageRequest pageRequest);
    IDataResult<PagedResult<CreateProductQuestionDto>> GetPagedByProductId(int productId, PageRequest pageRequest);
    IDataResult<CreateProductQuestionDto> GetById(int id);
    IResult Add(CreateProductQuestionDto createProductQuestionDto);
    IResult Update(UpdateProductQuestionDto updateProductQuestionDto);
    IResult Delete(int id);
}
