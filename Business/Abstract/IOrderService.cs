namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Orders;

using Core.Utilities.Paging;

public interface IOrderService
{
    IDataResult<List<OrderListDto>> GetAll();
    IDataResult<PagedResult<OrderListDto>> GetPaged(PageRequest pageRequest);
    IDataResult<OrderListDto> GetById(int id);
    IDataResult<OrderDetailDto> Checkout(CheckoutDto checkoutDto);
    IDataResult<List<OrderDetailDto>> GetMyOrders();
    IDataResult<PagedResult<OrderDetailDto>> GetMyOrdersPaged(PageRequest pageRequest);
    IDataResult<OrderDetailDto> GetOrderDetail(int id);
    IResult Add(CreateOrderDto createOrderDto);
    IResult Update(UpdateOrderDto updateOrderDto);
    IResult Delete(int id);
}
