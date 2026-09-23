namespace Business.DependencyResolvers.AutoMapper;

using Entities.Concrete;
using Entities.Dtos.Favorites;
using global::AutoMapper;

public class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<Favorite, UpdateFavoriteDto>();
        CreateMap<CreateFavoriteDto, Favorite>();
        CreateMap<UpdateFavoriteDto, Favorite>();
    }
}
