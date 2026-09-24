namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Carts;

public interface ICartService
{
    IDataResult<List<UpdateCartDto>> GetAll();
    IDataResult<UpdateCartDto> GetById(int id);
    IDataResult<CartDetailDto> GetMyCart();
    IResult AddItemToCart(AddToCartDto dto);
    IResult ClearCart();
    IResult Add(CreateCartDto createCartDto);
    IResult Update(UpdateCartDto updateCartDto);
    IResult Delete(int id);
}
