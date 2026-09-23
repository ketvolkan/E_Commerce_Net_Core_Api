namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.OrderItems;
using global::AutoMapper;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, UpdateOrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<UpdateOrderItemDto, OrderItem>();
    }
}
