namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.SubOrders;

public interface ISubOrderService
{
    IDataResult<List<UpdateSubOrderDto>> GetAll();
    IDataResult<UpdateSubOrderDto> GetById(int id);
    IResult Add(CreateSubOrderDto createSubOrderDto);
    IResult Update(UpdateSubOrderDto updateSubOrderDto);
    IResult Delete(int id);
}
