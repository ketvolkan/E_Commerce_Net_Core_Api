namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Carts;
using global::AutoMapper;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, UpdateCartDto>();
        CreateMap<CreateCartDto, Cart>();
        CreateMap<UpdateCartDto, Cart>();
    }
}
