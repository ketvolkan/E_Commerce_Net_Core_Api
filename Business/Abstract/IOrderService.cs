namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Orders;

public interface IOrderService
{
    IDataResult<List<OrderListDto>> GetAll();
    IDataResult<OrderListDto> GetById(int id);
    IResult Add(CreateOrderDto createOrderDto);
    IResult Update(UpdateOrderDto updateOrderDto);
    IResult Delete(int id);
}
