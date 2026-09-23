namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Favorites;

public interface IFavoriteService
{
    IDataResult<List<UpdateFavoriteDto>> GetAll();
    IDataResult<UpdateFavoriteDto> GetById(int id);
    IResult Add(CreateFavoriteDto createFavoriteDto);
    IResult Update(UpdateFavoriteDto updateFavoriteDto);
    IResult Delete(int id);
}
