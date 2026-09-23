namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.CartItems;

public interface ICartItemService
{
    IDataResult<List<UpdateCartItemDto>> GetAll();
    IDataResult<UpdateCartItemDto> GetById(int id);
    IResult Add(CreateCartItemDto createCartItemDto);
    IResult Update(UpdateCartItemDto updateCartItemDto);
    IResult Delete(int id);
}
