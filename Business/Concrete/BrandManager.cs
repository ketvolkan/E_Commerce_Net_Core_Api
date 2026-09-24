namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Brands;
using Business.Constants;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Paging;

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

    public IDataResult<PagedResult<BrandListDto>> GetPaged(PageRequest pageRequest)
    {
        var paged = _brandDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize);
        var dtos = _mapper.Map<List<BrandListDto>>(paged.Items);
        return new PagedDataResult<BrandListDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
    }

    [SecuredOperation("brand.getall")]
    public IDataResult<BrandDetailDto> GetById(int id)
    {
        var entity = _brandDal.Get(b => b.Id == id);
        if (entity == null) return new ErrorDataResult<BrandDetailDto>(Messages.BrandNotFound);
        return new SuccessDataResult<BrandDetailDto>(_mapper.Map<BrandDetailDto>(entity));
    }

    public IDataResult<List<BrandListDto>> GetListByUserId(int userId)
    {
        var list = _brandDal.GetList(b => b.UserId == userId);
        return new SuccessDataResult<List<BrandListDto>>(_mapper.Map<List<BrandListDto>>(list));
    }

    public IDataResult<List<BrandListDto>> GetMyBrands()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        return GetListByUserId(userId);
    }

    public IDataResult<PagedResult<BrandListDto>> GetMyBrandsPaged(PageRequest pageRequest)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var paged = _brandDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize, b => b.UserId == userId);
        var dtos = _mapper.Map<List<BrandListDto>>(paged.Items);
        return new PagedDataResult<BrandListDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
    }

    [SecuredOperation("brand.add")]
    public IResult Add(CreateBrandDto createBrandDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Brand>(createBrandDto);
        entity.UserId = userId;
        _brandDal.Add(entity);
        return new SuccessResult(Messages.BrandAdded);
    }

    [SecuredOperation("brand.update")]
    public IResult Update(UpdateBrandDto updateBrandDto)
    {
        var existing = _brandDal.Get(b => b.Id == updateBrandDto.Id);
        if (existing == null) return new ErrorResult(Messages.BrandNotFoundForUpdate);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateBrandDto, existing);
        existing.UserId = userId;
        _brandDal.Update(existing);
        return new SuccessResult(Messages.BrandUpdated);
    }

    [SecuredOperation("brand.delete")]
    public IResult Delete(int id)
    {
        var existing = _brandDal.Get(b => b.Id == id);
        if (existing == null) return new ErrorResult(Messages.BrandNotFoundForDelete);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _brandDal.Delete(existing);
        return new SuccessResult(Messages.BrandDeleted);
    }
}
