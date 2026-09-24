namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Products;
using Entities.DTOs;

using Core.Utilities.Paging;

public interface IProductService
{
    IDataResult<List<ProductDetailDto>> GetAll();
    IDataResult<PagedResult<ProductDetailDto>> GetPaged(PageRequest pageRequest);
    IDataResult<ProductDetailDto> GetById(int id);
    IDataResult<List<ProductDetailDto>> GetListByCategoryId(int categoryId);
    IDataResult<PagedResult<ProductDetailDto>> GetPagedByCategoryId(int categoryId, PageRequest pageRequest);
    IDataResult<List<ProductDetailDto>> GetListByUserId(int userId);
    IDataResult<PagedResult<ProductDetailDto>> GetPagedByUserId(int userId, PageRequest pageRequest);
    IDataResult<List<ProductDetailDto>> GetMyProducts();
    IDataResult<PagedResult<ProductDetailDto>> GetMyProductsPaged(PageRequest pageRequest);
    IDataResult<List<ProductDetailDto>> GetFeaturedProducts();
    IDataResult<PagedResult<ProductDetailDto>> Search(ProductFilterDto filter);
    IResult Add(CreateProductDto createProductDto);
    IResult Update(UpdateProductDto updateProductDto);
    IResult Delete(int id);
}