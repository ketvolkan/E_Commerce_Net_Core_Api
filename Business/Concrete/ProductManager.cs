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
using Core.Utilities.Paging;
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

    public IDataResult<PagedResult<ProductDetailDto>> GetPaged(PageRequest pageRequest)
    {
        var pagedProducts = _productDal.GetPagedListWithDetails(pageRequest.PageNumber, pageRequest.PageSize, p => p.IsActive);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(pagedProducts.Items);

        return new PagedDataResult<ProductDetailDto>(productDtos, pagedProducts.TotalCount, pagedProducts.PageNumber, pagedProducts.PageSize, "Ürünler sayfalanmış olarak listelendi.");
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

    public IDataResult<PagedResult<ProductDetailDto>> GetPagedByCategoryId(int categoryId, PageRequest pageRequest)
    {
        var pagedProducts = _productDal.GetPagedListWithDetails(pageRequest.PageNumber, pageRequest.PageSize, p => p.IsActive && p.CategoryId == categoryId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(pagedProducts.Items);

        return new PagedDataResult<ProductDetailDto>(productDtos, pagedProducts.TotalCount, pagedProducts.PageNumber, pagedProducts.PageSize);
    }

    public IDataResult<List<ProductDetailDto>> GetListByUserId(int userId)
    {
        var products = _productDal.GetListWithDetails(p => p.UserId == userId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);

        return new SuccessDataResult<List<ProductDetailDto>>(productDtos);
    }

    public IDataResult<PagedResult<ProductDetailDto>> GetPagedByUserId(int userId, PageRequest pageRequest)
    {
        var pagedProducts = _productDal.GetPagedListWithDetails(pageRequest.PageNumber, pageRequest.PageSize, p => p.UserId == userId);
        var productDtos = _mapper.Map<List<ProductDetailDto>>(pagedProducts.Items);

        return new PagedDataResult<ProductDetailDto>(productDtos, pagedProducts.TotalCount, pagedProducts.PageNumber, pagedProducts.PageSize);
    }

    public IDataResult<List<ProductDetailDto>> GetMyProducts()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        return GetListByUserId(userId);
    }

    public IDataResult<PagedResult<ProductDetailDto>> GetMyProductsPaged(PageRequest pageRequest)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        return GetPagedByUserId(userId, pageRequest);
    }

    public IDataResult<List<ProductDetailDto>> GetFeaturedProducts()
    {
        var products = _productDal.GetListWithDetails(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .Take(12)
            .ToList();
        var productDtos = _mapper.Map<List<ProductDetailDto>>(products);
        return new SuccessDataResult<List<ProductDetailDto>>(productDtos);
    }

    public IDataResult<PagedResult<ProductDetailDto>> Search(ProductFilterDto filter)
    {
        var products = _productDal.GetListWithDetails(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var keyword = filter.Keyword.Trim().ToLower();
            products = products.Where(p =>
                (p.Name != null && p.Name.ToLower().Contains(keyword)) ||
                (p.Description != null && p.Description.ToLower().Contains(keyword))
            ).ToList();
        }

        if (filter.CategoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == filter.CategoryId.Value).ToList();
        }

        if (filter.BrandId.HasValue)
        {
            products = products.Where(p => p.BrandId == filter.BrandId.Value).ToList();
        }

        if (filter.StoreId.HasValue)
        {
            products = products.Where(p => p.ProductVariants.Any(v => v.StoreId == filter.StoreId.Value)).ToList();
        }

        if (filter.MinPrice.HasValue)
        {
            products = products.Where(p => p.ProductVariants.Any(v => v.Price >= filter.MinPrice.Value)).ToList();
        }

        if (filter.MaxPrice.HasValue)
        {
            products = products.Where(p => p.ProductVariants.Any(v => v.Price <= filter.MaxPrice.Value)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            products = filter.SortBy.ToLower() switch
            {
                "price_asc" => products.OrderBy(p => p.ProductVariants.Any() ? p.ProductVariants.Min(v => v.Price) : 0).ToList(),
                "price_desc" => products.OrderByDescending(p => p.ProductVariants.Any() ? p.ProductVariants.Max(v => v.Price) : 0).ToList(),
                "newest" => products.OrderByDescending(p => p.CreatedAt).ToList(),
                "name" => products.OrderBy(p => p.Name).ToList(),
                _ => products
            };
        }

        var totalCount = products.Count();
        var safePageNumber = filter.PageNumber < 1 ? 1 : filter.PageNumber;
        var safePageSize = filter.PageSize < 1 ? 12 : filter.PageSize;
        var pagedItems = products.Skip((safePageNumber - 1) * safePageSize).Take(safePageSize).ToList();

        var productDtos = _mapper.Map<List<ProductDetailDto>>(pagedItems);
        return new PagedDataResult<ProductDetailDto>(productDtos, totalCount, safePageNumber, safePageSize);
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