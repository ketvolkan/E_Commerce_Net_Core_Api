namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Orders;
using global::AutoMapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderListDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
    }
}
