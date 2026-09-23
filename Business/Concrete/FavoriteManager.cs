namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Favorites;
using Business.BusinessAspects.Autofac;

public class FavoriteManager : IFavoriteService
{
    private readonly IFavoriteDal _favoriteDal;
    private readonly IMapper _mapper;

    public FavoriteManager(IFavoriteDal favoriteDal, IMapper mapper)
    {
        _favoriteDal = favoriteDal;
        _mapper = mapper;
    }

    [SecuredOperation("favorite.get")]
    public IDataResult<List<UpdateFavoriteDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var allFavorites = _favoriteDal.GetList();
            return new SuccessDataResult<List<UpdateFavoriteDto>>(_mapper.Map<List<UpdateFavoriteDto>>(allFavorites));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var userFavorites = _favoriteDal.GetList(f => f.UserId == userId);
        return new SuccessDataResult<List<UpdateFavoriteDto>>(_mapper.Map<List<UpdateFavoriteDto>>(userFavorites));
    }

    public IDataResult<UpdateFavoriteDto> GetById(int id)
    {
        var entity = _favoriteDal.Get(f => f.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateFavoriteDto>(Messages.FavoriteNotFound);
        return new SuccessDataResult<UpdateFavoriteDto>(_mapper.Map<UpdateFavoriteDto>(entity));
    }

    [SecuredOperation("favorite.add")]
    public IResult Add(CreateFavoriteDto createFavoriteDto)
    {
        // Assign ownership from authenticated user, ignore DTO.UserId
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Favorite>(createFavoriteDto);
        entity.UserId = userId;
        _favoriteDal.Add(entity);
        return new SuccessResult(Messages.FavoriteAdded);
    }

    [SecuredOperation("favorite.update")]
    public IResult Update(UpdateFavoriteDto updateFavoriteDto)
    {
        var existing = _favoriteDal.Get(f => f.Id == updateFavoriteDto.Id);
        if (existing == null) return new ErrorResult(Messages.FavoriteNotFoundForUpdate);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateFavoriteDto, existing);
        _favoriteDal.Update(existing);
        return new SuccessResult(Messages.FavoriteUpdated);
    }

    [SecuredOperation("favorite.delete")]
    public IResult Delete(int id)
    {
        var existing = _favoriteDal.Get(f => f.Id == id);
        if (existing == null) return new ErrorResult(Messages.FavoriteNotFoundForDelete);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _favoriteDal.Delete(existing);
        return new SuccessResult(Messages.FavoriteDeleted);
    }
}
