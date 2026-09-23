namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.OrderItems;

public class OrderItemManager : IOrderItemService
{
    private readonly IOrderItemDal _orderItemDal;
    private readonly IMapper _mapper;

    public OrderItemManager(IOrderItemDal orderItemDal, IMapper mapper)
    {
        _orderItemDal = orderItemDal;
        _mapper = mapper;
    }

    [SecuredOperation("order.get")]
    public IDataResult<List<UpdateOrderItemDto>> GetAll()
    {
        var list = _orderItemDal.GetList();
        return new SuccessDataResult<List<UpdateOrderItemDto>>(_mapper.Map<List<UpdateOrderItemDto>>(list));
    }

    public IDataResult<UpdateOrderItemDto> GetById(int id)
    {
        var entity = _orderItemDal.Get(oi => oi.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateOrderItemDto>(Messages.OrderItemNotFound);
        return new SuccessDataResult<UpdateOrderItemDto>(_mapper.Map<UpdateOrderItemDto>(entity));
    }

    [SecuredOperation("order.add")]
    public IResult Add(CreateOrderItemDto createOrderItemDto)
    {
        var entity = _mapper.Map<OrderItem>(createOrderItemDto);
        _orderItemDal.Add(entity);
        return new SuccessResult(Messages.OrderItemAdded);
    }

    public IResult Update(UpdateOrderItemDto updateOrderItemDto)
    {
        var existing = _orderItemDal.Get(oi => oi.Id == updateOrderItemDto.Id);
        if (existing == null) return new ErrorResult(Messages.OrderItemNotFound);
        _mapper.Map(updateOrderItemDto, existing);
        _orderItemDal.Update(existing);
        return new SuccessResult(Messages.OrderItemUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _orderItemDal.Get(oi => oi.Id == id);
        if (existing == null) return new ErrorResult(Messages.OrderItemNotFound);
        _orderItemDal.Delete(existing);
        return new SuccessResult(Messages.OrderItemDeleted);
    }
}
