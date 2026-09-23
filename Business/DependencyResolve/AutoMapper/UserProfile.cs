namespace Business.DependencyResolvers.AutoMapper;

using Core.Entities.Concrete;
using Entities.Dtos.Users;
using global::AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserListDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
