namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductQuestions;

public interface IProductQuestionService
{
    IDataResult<List<CreateProductQuestionDto>> GetAll();
    IDataResult<CreateProductQuestionDto> GetById(int id);
    IResult Add(CreateProductQuestionDto createProductQuestionDto);
    IResult Update(UpdateProductQuestionDto updateProductQuestionDto);
    IResult Delete(int id);
}
