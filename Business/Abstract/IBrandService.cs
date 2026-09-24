namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Brands;

public interface IBrandService
{
    IDataResult<List<BrandListDto>> GetAll();
    IDataResult<BrandDetailDto> GetById(int id);
    IDataResult<List<BrandListDto>> GetListByUserId(int userId);
    IDataResult<List<BrandListDto>> GetMyBrands();
    IResult Add(CreateBrandDto createBrandDto);
    IResult Update(UpdateBrandDto updateBrandDto);
    IResult Delete(int id);
}
