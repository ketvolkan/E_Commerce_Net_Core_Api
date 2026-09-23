namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Products;

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

    public IDataResult<ProductDetailDto> GetById(int id)
    {
        var product = _productDal.GetWithDetails(p => p.Id == id);
        if (product == null)
        {
            return new ErrorDataResult<ProductDetailDto>("Ürün bulunamadı.");
        }

        var productDto = _mapper.Map<ProductDetailDto>(product);
        return new SuccessDataResult<ProductDetailDto>(productDto);
    }

    public IDataResult<List<ProductDetailDto>> GetListByCategoryId(int categoryId)
    {
        var products = _productDal.GetListWithDetails(p => p.CategoryId == categoryId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);

        return new SuccessDataResult<List<ProductDetailDto>>(productDtos);
    }

    [ValidationAspect(typeof(CreateProductDtoValidator))]
    public IResult Add(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        _productDal.Add(product);
        return new SuccessResult("Ürün başarıyla eklendi.");
    }

    [ValidationAspect(typeof(UpdateProductDtoValidator))]
    public IResult Update(UpdateProductDto updateProductDto)
    {
        var existingProduct = _productDal.GetWithDetails(p => p.Id == updateProductDto.Id);
        if (existingProduct == null)
        {
            return new ErrorResult("Güncellenecek ürün bulunamadı.");
        }

        _mapper.Map(updateProductDto, existingProduct);
        _productDal.Update(existingProduct);
        return new SuccessResult("Ürün başarıyla güncellendi.");
    }
    public IResult Delete(int id)
    {
        var product = _productDal.Get(p => p.Id == id);
        if (product == null)
        {
            return new ErrorResult("Silinecek ürün bulunamadı.");
        }

        _productDal.Delete(product);

        return new SuccessResult("Ürün başarıyla silindi.");
    }
}