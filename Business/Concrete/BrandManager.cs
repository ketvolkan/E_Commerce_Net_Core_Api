namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Brands;
using Business.Constants;
using Business.BusinessAspects.Autofac;

public class BrandManager : IBrandService
{
    private readonly IBrandDal _brandDal;
    private readonly IMapper _mapper;

    public BrandManager(IBrandDal brandDal, IMapper mapper)
    {
        _brandDal = brandDal;
        _mapper = mapper;
    }

    [SecuredOperation("brand.getall")]
    public IDataResult<List<BrandListDto>> GetAll()
    {
        var list = _brandDal.GetList();
        return new SuccessDataResult<List<BrandListDto>>(_mapper.Map<List<BrandListDto>>(list));
    }

    [SecuredOperation("brand.getall")]
    public IDataResult<BrandDetailDto> GetById(int id)
    {
        var entity = _brandDal.Get(b => b.Id == id);
        if (entity == null) return new ErrorDataResult<BrandDetailDto>(Messages.BrandNotFound);
        return new SuccessDataResult<BrandDetailDto>(_mapper.Map<BrandDetailDto>(entity));
    }

    [SecuredOperation("brand.add")]
    public IResult Add(CreateBrandDto createBrandDto)
    {
        var entity = _mapper.Map<Brand>(createBrandDto);
        _brandDal.Add(entity);
        return new SuccessResult(Messages.BrandAdded);
    }

    [SecuredOperation("brand.update")]
    public IResult Update(UpdateBrandDto updateBrandDto)
    {
        var existing = _brandDal.Get(b => b.Id == updateBrandDto.Id);
        if (existing == null) return new ErrorResult(Messages.BrandNotFoundForUpdate);
        _mapper.Map(updateBrandDto, existing);
        _brandDal.Update(existing);
        return new SuccessResult(Messages.BrandUpdated);
    }

    [SecuredOperation("brand.delete")]
    public IResult Delete(int id)
    {
        var existing = _brandDal.Get(b => b.Id == id);
        if (existing == null) return new ErrorResult(Messages.BrandNotFoundForDelete);
        _brandDal.Delete(existing);
        return new SuccessResult(Messages.BrandDeleted);
    }
}
