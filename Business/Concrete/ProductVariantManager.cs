namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.ProductVariants;
using Business.BusinessAspects.Autofac;

public class ProductVariantManager : IProductVariantService
{
    private readonly IProductVariantDal _productVariantDal;
    private readonly IMapper _mapper;

    public ProductVariantManager(IProductVariantDal productVariantDal, IMapper mapper)
    {
        _productVariantDal = productVariantDal;
        _mapper = mapper;
    }

    [SecuredOperation("product.getall")]
    public IDataResult<List<UpdateProductVariantDto>> GetAll()
    {
        var list = _productVariantDal.GetList();
        return new SuccessDataResult<List<UpdateProductVariantDto>>(_mapper.Map<List<UpdateProductVariantDto>>(list));
    }

    public IDataResult<UpdateProductVariantDto> GetById(int id)
    {
        var entity = _productVariantDal.Get(pv => pv.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateProductVariantDto>(Messages.ProductVariantNotFound);
        return new SuccessDataResult<UpdateProductVariantDto>(_mapper.Map<UpdateProductVariantDto>(entity));
    }

    [SecuredOperation("product.add")]
    public IResult Add(CreateProductVariantDto createProductVariantDto)
    {
        var entity = _mapper.Map<ProductVariant>(createProductVariantDto);
        _productVariantDal.Add(entity);
        return new SuccessResult(Messages.ProductVariantAdded);
    }

    [SecuredOperation("product.update")]
    public IResult Update(UpdateProductVariantDto updateProductVariantDto)
    {
        var existing = _productVariantDal.Get(pv => pv.Id == updateProductVariantDto.Id);
        if (existing == null) return new ErrorResult(Messages.ProductVariantNotFoundForUpdate);
        _mapper.Map(updateProductVariantDto, existing);
        _productVariantDal.Update(existing);
        return new SuccessResult(Messages.ProductVariantUpdated);
    }

    [SecuredOperation("product.delete")]
    public IResult Delete(int id)
    {
        var existing = _productVariantDal.Get(pv => pv.Id == id);
        if (existing == null) return new ErrorResult(Messages.ProductVariantNotFoundForDelete);
        _productVariantDal.Delete(existing);
        return new SuccessResult(Messages.ProductVariantDeleted);
    }
}
