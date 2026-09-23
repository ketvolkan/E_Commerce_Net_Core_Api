namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.ProductImages;

public class ProductImageManager : IProductImageService
{
    private readonly IProductImageDal _productImageDal;
    private readonly IMapper _mapper;

    public ProductImageManager(IProductImageDal productImageDal, IMapper mapper)
    {
        _productImageDal = productImageDal;
        _mapper = mapper;
    }

    public IDataResult<List<UpdateProductImageDto>> GetAll()
    {
        var list = _productImageDal.GetList();
        return new SuccessDataResult<List<UpdateProductImageDto>>(_mapper.Map<List<UpdateProductImageDto>>(list));
    }

    public IDataResult<UpdateProductImageDto> GetById(int id)
    {
        var entity = _productImageDal.Get(pi => pi.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateProductImageDto>(Messages.ProductImageNotFound);
        return new SuccessDataResult<UpdateProductImageDto>(_mapper.Map<UpdateProductImageDto>(entity));
    }

    public IResult Add(CreateProductImageDto createProductImageDto)
    {
        var entity = _mapper.Map<ProductImage>(createProductImageDto);
        _productImageDal.Add(entity);
        return new SuccessResult(Messages.ProductImageAdded);
    }

    public IResult Update(UpdateProductImageDto updateProductImageDto)
    {
        var existing = _productImageDal.Get(pi => pi.Id == updateProductImageDto.Id);
        if (existing == null) return new ErrorResult(Messages.ProductImageNotFound);
        _mapper.Map(updateProductImageDto, existing);
        _productImageDal.Update(existing);
        return new SuccessResult(Messages.ProductImageUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _productImageDal.Get(pi => pi.Id == id);
        if (existing == null) return new ErrorResult(Messages.ProductImageNotFound);
        _productImageDal.Delete(existing);
        return new SuccessResult(Messages.ProductImageDeleted);
    }
}
