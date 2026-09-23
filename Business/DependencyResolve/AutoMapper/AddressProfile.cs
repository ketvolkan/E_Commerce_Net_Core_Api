namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Addresses;
using global::AutoMapper;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, UpdateAddressDto>();
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();
    }
}
