namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.SubOrders;
using global::AutoMapper;

public class SubOrderProfile : Profile
{
    public SubOrderProfile()
    {
        CreateMap<SubOrder, UpdateSubOrderDto>();
        CreateMap<CreateSubOrderDto, SubOrder>();
        CreateMap<UpdateSubOrderDto, SubOrder>();
    }
}
