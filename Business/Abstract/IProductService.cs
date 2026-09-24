namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Products;
using Entities.DTOs;

public interface IProductService
{
    IDataResult<List<ProductDetailDto>> GetAll();
    IDataResult<ProductDetailDto> GetById(int id);
    IDataResult<List<ProductDetailDto>> GetListByCategoryId(int categoryId);
    IDataResult<List<ProductDetailDto>> GetListByUserId(int userId);
    IDataResult<List<ProductDetailDto>> GetMyProducts();
    IResult Add(CreateProductDto createProductDto);
    IResult Update(UpdateProductDto updateProductDto);
    IResult Delete(int id);
}