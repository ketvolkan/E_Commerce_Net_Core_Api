namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.CartItems;
using global::AutoMapper;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, UpdateCartItemDto>();
        CreateMap<CreateCartItemDto, CartItem>();
        CreateMap<UpdateCartItemDto, CartItem>();
    }
}
