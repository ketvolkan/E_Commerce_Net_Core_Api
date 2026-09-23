namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Categories;
using Business.Constants;
using Business.BusinessAspects.Autofac;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
    }

    [SecuredOperation("category.getall")]
    public IDataResult<List<CategoryListDto>> GetAll()
    {
        var categories = _categoryDal.GetList();
        var dtos = _mapper.Map<List<CategoryListDto>>(categories);
        return new SuccessDataResult<List<CategoryListDto>>(dtos);
    }

    [SecuredOperation("category.getall")]
    public IDataResult<CategoryDetailDto> GetById(int id)
    {
        var category = _categoryDal.Get(c => c.Id == id);
        if (category == null) return new ErrorDataResult<CategoryDetailDto>(Messages.CategoryNotFound);
        return new SuccessDataResult<CategoryDetailDto>(_mapper.Map<CategoryDetailDto>(category));
    }

    [SecuredOperation("category.add")]
    public IResult Add(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        _categoryDal.Add(category);
        return new SuccessResult(Messages.CategoryAdded);
    }

    [SecuredOperation("category.update")]
    public IResult Update(UpdateCategoryDto updateCategoryDto)
    {
        var existing = _categoryDal.Get(c => c.Id == updateCategoryDto.Id);
        if (existing == null) return new ErrorResult(Messages.CategoryNotFoundForUpdate);
        _mapper.Map(updateCategoryDto, existing);
        _categoryDal.Update(existing);
        return new SuccessResult(Messages.CategoryUpdated);
    }

    [SecuredOperation("category.delete")]
    public IResult Delete(int id)
    {
        var existing = _categoryDal.Get(c => c.Id == id);
        if (existing == null) return new ErrorResult(Messages.CategoryNotFoundForDelete);
        _categoryDal.Delete(existing);
        return new SuccessResult(Messages.CategoryDeleted);
    }
}
