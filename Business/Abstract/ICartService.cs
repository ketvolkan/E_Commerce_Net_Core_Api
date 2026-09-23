namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Carts;

public interface ICartService
{
    IDataResult<List<UpdateCartDto>> GetAll();
    IDataResult<UpdateCartDto> GetById(int id);
    IResult Add(CreateCartDto createCartDto);
    IResult Update(UpdateCartDto updateCartDto);
    IResult Delete(int id);
}
