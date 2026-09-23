namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Addresses;

public class AddressManager : IAddressService
{
    private readonly IAddressDal _addressDal;
    private readonly IMapper _mapper;

    public AddressManager(IAddressDal addressDal, IMapper mapper)
    {
        _addressDal = addressDal;
        _mapper = mapper;
    }

    public IDataResult<List<UpdateAddressDto>> GetAll()
    {
        var list = _addressDal.GetList();
        return new SuccessDataResult<List<UpdateAddressDto>>(_mapper.Map<List<UpdateAddressDto>>(list));
    }

    public IDataResult<UpdateAddressDto> GetById(int id)
    {
        var entity = _addressDal.Get(a => a.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateAddressDto>("Adres bulunamadı.");
        return new SuccessDataResult<UpdateAddressDto>(_mapper.Map<UpdateAddressDto>(entity));
    }

    public IResult Add(CreateAddressDto createAddressDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Address>(createAddressDto);
        entity.UserId = userId;
        _addressDal.Add(entity);
        return new SuccessResult(Messages.AddressAdded);
    }

    public IResult Update(UpdateAddressDto updateAddressDto)
    {
        var existing = _addressDal.Get(a => a.Id == updateAddressDto.Id);
        if (existing == null) return new ErrorResult(Messages.AddressNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateAddressDto, existing);
        _addressDal.Update(existing);
        return new SuccessResult(Messages.AddressUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _addressDal.Get(a => a.Id == id);
        if (existing == null) return new ErrorResult(Messages.AddressNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _addressDal.Delete(existing);
        return new SuccessResult(Messages.AddressDeleted);
    }
}
