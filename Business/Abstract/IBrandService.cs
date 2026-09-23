namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Brands;

public interface IBrandService
{
    IDataResult<List<BrandListDto>> GetAll();
    IDataResult<BrandDetailDto> GetById(int id);
    IResult Add(CreateBrandDto createBrandDto);
    IResult Update(UpdateBrandDto updateBrandDto);
    IResult Delete(int id);
}
