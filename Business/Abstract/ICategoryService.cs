namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Categories;

public interface ICategoryService
{
    IDataResult<List<CategoryListDto>> GetAll();
    IDataResult<CategoryDetailDto> GetById(int id);
    IResult Add(CreateCategoryDto createCategoryDto);
    IResult Update(UpdateCategoryDto updateCategoryDto);
    IResult Delete(int id);
}
