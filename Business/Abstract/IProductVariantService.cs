namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.ProductVariants;

public interface IProductVariantService
{
    IDataResult<List<UpdateProductVariantDto>> GetAll();
    IDataResult<UpdateProductVariantDto> GetById(int id);
    IResult Add(CreateProductVariantDto createProductVariantDto);
    IResult Update(UpdateProductVariantDto updateProductVariantDto);
    IResult Delete(int id);
}
