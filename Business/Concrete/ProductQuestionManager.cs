namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.ProductQuestions;
using Business.BusinessAspects.Autofac;

public class ProductQuestionManager : IProductQuestionService
{
    private readonly IProductQuestionDal _productQuestionDal;
    private readonly IMapper _mapper;

    public ProductQuestionManager(IProductQuestionDal productQuestionDal, IMapper mapper)
    {
        _productQuestionDal = productQuestionDal;
        _mapper = mapper;
    }

    [SecuredOperation("product.getall")]
    public IDataResult<List<CreateProductQuestionDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var allQuestions = _productQuestionDal.GetList();
            return new SuccessDataResult<List<CreateProductQuestionDto>>(_mapper.Map<List<CreateProductQuestionDto>>(allQuestions));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var userQuestions = _productQuestionDal.GetList(pq => pq.UserId == userId);
        return new SuccessDataResult<List<CreateProductQuestionDto>>(_mapper.Map<List<CreateProductQuestionDto>>(userQuestions));
    }

    [SecuredOperation("product.getall")]
    public IDataResult<CreateProductQuestionDto> GetById(int id)
    {
        var entity = _productQuestionDal.Get(pq => pq.Id == id);
        if (entity == null) return new ErrorDataResult<CreateProductQuestionDto>(Messages.ProductQuestionNotFound);
        return new SuccessDataResult<CreateProductQuestionDto>(_mapper.Map<CreateProductQuestionDto>(entity));
    }

    public IResult Add(CreateProductQuestionDto createProductQuestionDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<ProductQuestion>(createProductQuestionDto);
        entity.UserId = userId;
        _productQuestionDal.Add(entity);
        return new SuccessResult(Messages.ProductQuestionAdded);
    }

    public IResult Update(UpdateProductQuestionDto updateProductQuestionDto)
    {
        var existing = _productQuestionDal.Get(pq => pq.Id == updateProductQuestionDto.Id);
        if (existing == null) return new ErrorResult(Messages.ProductQuestionNotFoundForUpdate);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateProductQuestionDto, existing);
        _productQuestionDal.Update(existing);
        return new SuccessResult(Messages.ProductQuestionUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _productQuestionDal.Get(pq => pq.Id == id);
        if (existing == null) return new ErrorResult(Messages.ProductQuestionNotFoundForDelete);
        _productQuestionDal.Delete(existing);
        return new SuccessResult(Messages.ProductQuestionDeleted);
    }
}
