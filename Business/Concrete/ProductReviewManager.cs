namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.ProductReviews;

using Core.Utilities.Paging;

public class ProductReviewManager : IProductReviewService
{
    private readonly IProductReviewDal _productReviewDal;
    private readonly IMapper _mapper;

    public ProductReviewManager(IProductReviewDal productReviewDal, IMapper mapper)
    {
        _productReviewDal = productReviewDal;
        _mapper = mapper;
    }

    public IDataResult<List<CreateProductReviewDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var allReviews = _productReviewDal.GetList();
            return new SuccessDataResult<List<CreateProductReviewDto>>(_mapper.Map<List<CreateProductReviewDto>>(allReviews));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var userReviews = _productReviewDal.GetList(pr => pr.UserId == userId);
        return new SuccessDataResult<List<CreateProductReviewDto>>(_mapper.Map<List<CreateProductReviewDto>>(userReviews));
    }

    public IDataResult<PagedResult<CreateProductReviewDto>> GetPaged(PageRequest pageRequest)
    {
        PagedResult<ProductReview> paged;
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            paged = _productReviewDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize);
        }
        else
        {
            var userId = Core.Utilities.Security.CurrentUser.GetUserId();
            paged = _productReviewDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize, pr => pr.UserId == userId);
        }

        var dtos = _mapper.Map<List<CreateProductReviewDto>>(paged.Items);
        return new PagedDataResult<CreateProductReviewDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
    }

    public IDataResult<PagedResult<CreateProductReviewDto>> GetPagedByProductId(int productId, PageRequest pageRequest)
    {
        var paged = _productReviewDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize, pr => pr.ProductId == productId);
        var dtos = _mapper.Map<List<CreateProductReviewDto>>(paged.Items);
        return new PagedDataResult<CreateProductReviewDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
    }

    public IDataResult<CreateProductReviewDto> GetById(int id)
    {
        var entity = _productReviewDal.Get(pr => pr.Id == id);
        if (entity == null) return new ErrorDataResult<CreateProductReviewDto>(Messages.ProductReviewNotFound);
        return new SuccessDataResult<CreateProductReviewDto>(_mapper.Map<CreateProductReviewDto>(entity));
    }

    public IResult Add(CreateProductReviewDto createProductReviewDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<ProductReview>(createProductReviewDto);
        entity.UserId = userId;
        _productReviewDal.Add(entity);
        return new SuccessResult(Messages.ProductReviewAdded);
    }

    public IResult Update(CreateProductReviewDto updateProductReviewDto)
    {
        var existing = _productReviewDal.Get(pr => pr.Id == updateProductReviewDto.ProductId);
        if (existing == null) return new ErrorResult(Messages.ProductReviewNotFoundForUpdate);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateProductReviewDto, existing);
        _productReviewDal.Update(existing);
        return new SuccessResult(Messages.ProductReviewUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _productReviewDal.Get(pr => pr.Id == id);
        if (existing == null) return new ErrorResult(Messages.ProductReviewNotFoundForDelete);
        _productReviewDal.Delete(existing);
        return new SuccessResult(Messages.ProductReviewDeleted);
    }
}
