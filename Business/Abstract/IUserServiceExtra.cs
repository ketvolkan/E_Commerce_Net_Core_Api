namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Users;

public interface IUserServiceExtra
{
    IDataResult<List<UserListDto>> GetAll();
    IDataResult<UserListDto> GetById(int id);
    IResult Add(CreateUserDto createUserDto);
    IResult Update(UpdateUserDto updateUserDto);
    IResult Delete(int id);
}
