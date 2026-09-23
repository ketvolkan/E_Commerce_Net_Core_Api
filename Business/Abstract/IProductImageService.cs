namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductImages;

public interface IProductImageService
{
    IDataResult<List<UpdateProductImageDto>> GetAll();
    IDataResult<UpdateProductImageDto> GetById(int id);
    IResult Add(CreateProductImageDto createProductImageDto);
    IResult Update(UpdateProductImageDto updateProductImageDto);
    IResult Delete(int id);
}
