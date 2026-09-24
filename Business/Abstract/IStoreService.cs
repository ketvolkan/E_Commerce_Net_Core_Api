namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Stores;

using Core.Utilities.Paging;

public interface IStoreService
{
    IDataResult<List<StoreListDto>> GetAll();
    IDataResult<PagedResult<StoreListDto>> GetPaged(PageRequest pageRequest);
    IDataResult<StoreListDto> GetById(int id);
    IResult Add(CreateStoreDto createStoreDto);
    IResult Update(UpdateStoreDto updateStoreDto);
    IResult Delete(int id);
}
