namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.SubOrders;

using Core.Utilities.Paging;

public interface ISubOrderService
{
    IDataResult<List<UpdateSubOrderDto>> GetAll();
    IDataResult<PagedResult<UpdateSubOrderDto>> GetPaged(PageRequest pageRequest);
    IDataResult<UpdateSubOrderDto> GetById(int id);
    IDataResult<List<Entities.Dtos.Orders.SubOrderDetailDto>> GetMyStoreOrders();
    IDataResult<PagedResult<Entities.Dtos.Orders.SubOrderDetailDto>> GetMyStoreOrdersPaged(PageRequest pageRequest);
    IResult Add(CreateSubOrderDto createSubOrderDto);
    IResult Update(UpdateSubOrderDto updateSubOrderDto);
    IResult Delete(int id);
}
