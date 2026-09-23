namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Orders;
using Business.BusinessAspects.Autofac;

public class OrderManager : IOrderService
{
    private readonly IOrderDal _orderDal;
    private readonly IMapper _mapper;

    public OrderManager(IOrderDal orderDal, IMapper mapper)
    {
        _orderDal = orderDal;
        _mapper = mapper;
    }

    public IDataResult<List<OrderListDto>> GetAll()
    {
        // If admin, return all orders. If store owner, return orders related to their store(s) via SubOrders. Otherwise return only orders of current user.
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var list = _orderDal.GetList();
            return new SuccessDataResult<List<OrderListDto>>(_mapper.Map<List<OrderListDto>>(list));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var listByUser = _orderDal.GetList(o => o.UserId == userId);
        return new SuccessDataResult<List<OrderListDto>>(_mapper.Map<List<OrderListDto>>(listByUser));
    }

    public IDataResult<OrderListDto> GetById(int id)
    {
        var entity = _orderDal.Get(o => o.Id == id);
        if (entity == null) return new ErrorDataResult<OrderListDto>(Messages.OrderNotFound);
        return new SuccessDataResult<OrderListDto>(_mapper.Map<OrderListDto>(entity));
    }

    [SecuredOperation("order.add")]
    public IResult Add(CreateOrderDto createOrderDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Order>(createOrderDto);
        entity.UserId = userId;
        _orderDal.Add(entity);
        return new SuccessResult(Messages.OrderAdded);
    }

    public IResult Update(UpdateOrderDto updateOrderDto)
    {
        var existing = _orderDal.Get(o => o.Id == updateOrderDto.Id);
        if (existing == null) return new ErrorResult(Messages.OrderNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        // Admins can update any order; store owners may update order status for their stores via SubOrders handled elsewhere
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateOrderDto, existing);
        _orderDal.Update(existing);
        return new SuccessResult(Messages.OrderUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _orderDal.Get(o => o.Id == id);
        if (existing == null) return new ErrorResult(Messages.OrderNotFound);
        _orderDal.Delete(existing);
        return new SuccessResult(Messages.OrderDeleted);
    }
}
