namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.OrderItems;

public interface IOrderItemService
{
    IDataResult<List<UpdateOrderItemDto>> GetAll();
    IDataResult<UpdateOrderItemDto> GetById(int id);
    IResult Add(CreateOrderItemDto createOrderItemDto);
    IResult Update(UpdateOrderItemDto updateOrderItemDto);
    IResult Delete(int id);
}
