namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.CartItems;
using Business.BusinessAspects.Autofac;

public class CartItemManager : ICartItemService
{
    private readonly ICartItemDal _cartItemDal;
    private readonly IMapper _mapper;

    public CartItemManager(ICartItemDal cartItemDal, IMapper mapper)
    {
        _cartItemDal = cartItemDal;
        _mapper = mapper;
    }

    [SecuredOperation("cart.get")]
    public IDataResult<List<UpdateCartItemDto>> GetAll()
    {
        var list = _cartItemDal.GetList();
        return new SuccessDataResult<List<UpdateCartItemDto>>(_mapper.Map<List<UpdateCartItemDto>>(list));
    }

    public IDataResult<UpdateCartItemDto> GetById(int id)
    {
        var entity = _cartItemDal.Get(ci => ci.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateCartItemDto>(Messages.CartItemNotFound);
        return new SuccessDataResult<UpdateCartItemDto>(_mapper.Map<UpdateCartItemDto>(entity));
    }

    [SecuredOperation("cart.add")]
    public IResult Add(CreateCartItemDto createCartItemDto)
    {
        var entity = _mapper.Map<CartItem>(createCartItemDto);
        _cartItemDal.Add(entity);
        return new SuccessResult(Messages.CartItemAdded);
    }

    public IResult Update(UpdateCartItemDto updateCartItemDto)
    {
        var existing = _cartItemDal.Get(ci => ci.Id == updateCartItemDto.Id);
        if (existing == null) return new ErrorResult(Messages.CartItemNotFound);
        _mapper.Map(updateCartItemDto, existing);
        _cartItemDal.Update(existing);
        return new SuccessResult(Messages.CartItemUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _cartItemDal.Get(ci => ci.Id == id);
        if (existing == null) return new ErrorResult(Messages.CartItemNotFound);
        _cartItemDal.Delete(existing);
        return new SuccessResult(Messages.CartItemDeleted);
    }
}
