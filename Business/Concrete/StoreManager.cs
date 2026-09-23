namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Stores;
using Business.BusinessAspects.Autofac;
public class StoreManager : IStoreService
{
    private readonly IStoreDal _storeDal;
    private readonly IMapper _mapper;

    public StoreManager(IStoreDal storeDal, IMapper mapper)
    {
        _storeDal = storeDal;
        _mapper = mapper;
    }

    [SecuredOperation("store.getall")]
    public IDataResult<List<StoreListDto>> GetAll()
    {
        var list = _storeDal.GetList();
        return new SuccessDataResult<List<StoreListDto>>(_mapper.Map<List<StoreListDto>>(list));
    }

    public IDataResult<StoreListDto> GetById(int id)
    {
        var entity = _storeDal.Get(s => s.Id == id);
        if (entity == null) return new ErrorDataResult<StoreListDto>(Messages.StoreNotFound);
        return new SuccessDataResult<StoreListDto>(_mapper.Map<StoreListDto>(entity));
    }

    [SecuredOperation("store.add")]
    public IResult Add(CreateStoreDto createStoreDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Store>(createStoreDto);
        entity.UserId = userId;
        _storeDal.Add(entity);
        return new SuccessResult(Messages.StoreAdded);
    }

    public IResult Update(UpdateStoreDto updateStoreDto)
    {
        var existing = _storeDal.Get(s => s.Id == updateStoreDto.Id);
        if (existing == null) return new ErrorResult(Messages.StoreNotFoundForUpdate);
        _mapper.Map(updateStoreDto, existing);
        _storeDal.Update(existing);
        return new SuccessResult(Messages.StoreUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _storeDal.Get(s => s.Id == id);
        if (existing == null) return new ErrorResult(Messages.StoreNotFoundForDelete);
        _storeDal.Delete(existing);
        return new SuccessResult(Messages.StoreDeleted);
    }
}
