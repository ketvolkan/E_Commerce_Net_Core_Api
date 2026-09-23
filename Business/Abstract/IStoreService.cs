namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Stores;

public interface IStoreService
{
    IDataResult<List<StoreListDto>> GetAll();
    IDataResult<StoreListDto> GetById(int id);
    IResult Add(CreateStoreDto createStoreDto);
    IResult Update(UpdateStoreDto updateStoreDto);
    IResult Delete(int id);
}
