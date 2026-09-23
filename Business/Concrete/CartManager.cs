namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Business.BusinessAspects.Autofac;
using Entities.Dtos.Carts;

public class CartManager : ICartService
{
    private readonly ICartDal _cartDal;
    private readonly IMapper _mapper;

    public CartManager(ICartDal cartDal, IMapper mapper)
    {
        _cartDal = cartDal;
        _mapper = mapper;
    }

    [SecuredOperation("cart.get")]
    public IDataResult<List<UpdateCartDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var allCarts = _cartDal.GetList();
            return new SuccessDataResult<List<UpdateCartDto>>(_mapper.Map<List<UpdateCartDto>>(allCarts));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var userCarts = _cartDal.GetList(c => c.UserId == userId);
        return new SuccessDataResult<List<UpdateCartDto>>(_mapper.Map<List<UpdateCartDto>>(userCarts));
    }

    public IDataResult<UpdateCartDto> GetById(int id)
    {
        var entity = _cartDal.Get(c => c.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateCartDto>(Messages.CartNotFound);
        return new SuccessDataResult<UpdateCartDto>(_mapper.Map<UpdateCartDto>(entity));
    }

    [SecuredOperation("cart.add")]
    public IResult Add(CreateCartDto createCartDto)
    {
        // Ignore UserId from DTO; use authenticated user id
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Cart>(createCartDto);
        entity.UserId = userId;
        _cartDal.Add(entity);
        return new SuccessResult(Messages.CartAdded);
    }

    public IResult Update(UpdateCartDto updateCartDto)
    {
        var existing = _cartDal.Get(c => c.Id == updateCartDto.Id);
        if (existing == null) return new ErrorResult(Messages.CartNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateCartDto, existing);
        _cartDal.Update(existing);
        return new SuccessResult(Messages.CartUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _cartDal.Get(c => c.Id == id);
        if (existing == null) return new ErrorResult(Messages.CartNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _cartDal.Delete(existing);
        return new SuccessResult(Messages.CartDeleted);
    }
}
