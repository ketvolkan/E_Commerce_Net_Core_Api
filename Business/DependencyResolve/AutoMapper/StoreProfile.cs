namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Stores;
using global::AutoMapper;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreListDto>();
        CreateMap<CreateStoreDto, Store>();
        CreateMap<UpdateStoreDto, Store>();
    }
}
