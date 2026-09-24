namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Business.BusinessAspects.Autofac;
using Entities.Dtos.Carts;

using System.Linq;

public class CartManager : ICartService
{
    private readonly ICartDal _cartDal;
    private readonly ICartItemDal _cartItemDal;
    private readonly IProductVariantDal _productVariantDal;
    private readonly IMapper _mapper;

    public CartManager(ICartDal cartDal, ICartItemDal cartItemDal, IProductVariantDal productVariantDal, IMapper mapper)
    {
        _cartDal = cartDal;
        _cartItemDal = cartItemDal;
        _productVariantDal = productVariantDal;
        _mapper = mapper;
    }

    public IDataResult<CartDetailDto> GetMyCart()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<CartDetailDto>("Kullanıcı oturumu bulunamadı.");

        var cart = _cartDal.GetCartWithDetails(userId);
        if (cart == null)
        {
            // Auto create cart for user
            cart = new Cart { UserId = userId };
            _cartDal.Add(cart);
            cart = _cartDal.GetCartWithDetails(userId) ?? cart;
        }

        var detail = new CartDetailDto
        {
            CartId = cart.Id,
            UserId = cart.UserId,
            Items = cart.CartItems.Select(ci => new CartItemDetailDto
            {
                CartItemId = ci.Id,
                ProductVariantId = ci.ProductVariantId,
                ProductId = ci.ProductVariant?.ProductId ?? 0,
                ProductName = ci.ProductVariant?.Product?.Name ?? "Ürün",
                ImageUrl = ci.ProductVariant?.Product?.ProductImages?.OrderBy(pi => pi.DisplayOrder).FirstOrDefault()?.ImageUrl ?? string.Empty,
                StoreId = ci.ProductVariant?.StoreId ?? 0,
                StoreName = ci.ProductVariant?.Store?.Name ?? "Satıcı",
                Color = ci.ProductVariant?.Color ?? string.Empty,
                Size = ci.ProductVariant?.Size ?? string.Empty,
                Price = (ci.ProductVariant != null && ci.ProductVariant.DiscountPrice > 0) ? ci.ProductVariant.DiscountPrice : (ci.ProductVariant?.Price ?? 0),
                Quantity = ci.Quantity
            }).ToList()
        };

        detail.TotalPrice = detail.Items.Sum(i => i.ItemTotal);
        detail.TotalItems = detail.Items.Sum(i => i.Quantity);

        return new SuccessDataResult<CartDetailDto>(detail);
    }

    public IResult AddItemToCart(AddToCartDto dto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorResult("Kullanıcı oturumu bulunamadı.");

        var variant = _productVariantDal.Get(pv => pv.Id == dto.ProductVariantId);
        if (variant == null) return new ErrorResult("Ürün varyantı bulunamadı.");

        if (variant.StockQuantity < dto.Quantity)
        {
            return new ErrorResult($"Yetersiz stok! Mevcut stok: {variant.StockQuantity}");
        }

        var cart = _cartDal.Get(c => c.UserId == userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _cartDal.Add(cart);
            cart = _cartDal.Get(c => c.UserId == userId);
        }

        var existingItem = _cartItemDal.Get(ci => ci.CartId == cart!.Id && ci.ProductVariantId == dto.ProductVariantId);
        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
            _cartItemDal.Update(existingItem);
        }
        else
        {
            var newItem = new CartItem
            {
                CartId = cart!.Id,
                ProductVariantId = dto.ProductVariantId,
                Quantity = dto.Quantity
            };
            _cartItemDal.Add(newItem);
        }

        return new SuccessResult("Ürün sepete eklendi.");
    }

    public IResult ClearCart()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorResult("Kullanıcı oturumu bulunamadı.");

        var cart = _cartDal.Get(c => c.UserId == userId);
        if (cart != null)
        {
            var items = _cartItemDal.GetList(ci => ci.CartId == cart.Id);
            foreach (var item in items)
            {
                _cartItemDal.Delete(item);
            }
        }

        return new SuccessResult("Sepet temizlendi.");
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
