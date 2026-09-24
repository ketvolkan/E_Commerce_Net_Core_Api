namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.ProductImages;
using Entities.Dtos.ProductVariants;
using Entities.Dtos.Products;
using Business.BusinessAspects.Autofac;
public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IMapper _mapper;

    public ProductManager(IProductDal productDal, IMapper mapper)
    {
        _productDal = productDal;
        _mapper = mapper;
    }

    public IDataResult<List<ProductDetailDto>> GetAll()
    {
        var products = _productDal.GetListWithDetails();
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);

        return new SuccessDataResult<List<ProductDetailDto>>(productDtos, "Ürünler başarıyla listelendi.");
    }

    [SecuredOperation("product.getall")]
    public IDataResult<ProductDetailDto> GetById(int id)
    {
        var product = _productDal.GetWithDetails(p => p.Id == id);
        if (product == null)
        {
            return new ErrorDataResult<ProductDetailDto>(Messages.ProductNotFound);
        }

        var productDto = _mapper.Map<ProductDetailDto>(product);
        return new SuccessDataResult<ProductDetailDto>(productDto);
    }

    [SecuredOperation("product.getall")]
    public IDataResult<List<ProductDetailDto>> GetListByCategoryId(int categoryId)
    {
        var products = _productDal.GetListWithDetails(p => p.CategoryId == categoryId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);

        return new SuccessDataResult<List<ProductDetailDto>>(productDtos);
    }

    public IDataResult<List<ProductDetailDto>> GetListByUserId(int userId)
    {
        var products = _productDal.GetListWithDetails(p => p.UserId == userId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);

        return new SuccessDataResult<List<ProductDetailDto>>(productDtos);
    }

    public IDataResult<List<ProductDetailDto>> GetMyProducts()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        return GetListByUserId(userId);
    }

    [ValidationAspect(typeof(CreateProductDtoValidator))]
    [SecuredOperation("product.add")]
    public IResult Add(CreateProductDto createProductDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var product = _mapper.Map<Product>(createProductDto);
        product.UserId = userId;
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    [ValidationAspect(typeof(UpdateProductDtoValidator))]
    [SecuredOperation("product.update")]
    public IResult Update(UpdateProductDto updateProductDto)
    {
        var existingProduct = _productDal.GetWithDetails(p => p.Id == updateProductDto.Id);
        if (existingProduct == null)
        {
            return new ErrorResult(Messages.ProductNotFoundForUpdate);
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existingProduct.UserId != userId)
        {
            return new ErrorResult(Messages.AuthorizationDenied);
        }

        _mapper.Map(updateProductDto, existingProduct);
        existingProduct.UserId = userId;
        _productDal.Update(existingProduct);
        return new SuccessResult(Messages.ProductUpdated);
    }

    public IResult Delete(int id)
    {
        var product = _productDal.Get(p => p.Id == id);
        if (product == null)
        {
            return new ErrorResult(Messages.ProductNotFoundForDelete);
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && product.UserId != userId)
        {
            return new ErrorResult(Messages.AuthorizationDenied);
        }

        _productDal.Delete(product);

        return new SuccessResult(Messages.ProductDeleted);
    }
}