namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Brands;

using Core.Utilities.Paging;

public interface IBrandService
{
    IDataResult<List<BrandListDto>> GetAll();
    IDataResult<PagedResult<BrandListDto>> GetPaged(PageRequest pageRequest);
    IDataResult<BrandDetailDto> GetById(int id);
    IDataResult<List<BrandListDto>> GetListByUserId(int userId);
    IDataResult<List<BrandListDto>> GetMyBrands();
    IDataResult<PagedResult<BrandListDto>> GetMyBrandsPaged(PageRequest pageRequest);
    IResult Add(CreateBrandDto createBrandDto);
    IResult Update(UpdateBrandDto updateBrandDto);
    IResult Delete(int id);
}
