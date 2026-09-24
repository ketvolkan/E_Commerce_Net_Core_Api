namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Addresses;

public interface IAddressService
{
    IDataResult<List<UpdateAddressDto>> GetAll();
    IDataResult<UpdateAddressDto> GetById(int id);
    IDataResult<List<UpdateAddressDto>> GetMyAddresses();
    IResult Add(CreateAddressDto createAddressDto);
    IResult Update(UpdateAddressDto updateAddressDto);
    IResult Delete(int id);
}
