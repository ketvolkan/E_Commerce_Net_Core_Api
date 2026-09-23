namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Entities.Concrete;
using Entities.Dtos.UserOperationClaims;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;
    private readonly IMapper _mapper;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper)
    {
        _userOperationClaimDal = userOperationClaimDal;
        _mapper = mapper;
    }

    public IDataResult<List<UserOperationClaimDto>> GetAll()
    {
        var list = _userOperationClaimDal.GetList();
        return new SuccessDataResult<List<UserOperationClaimDto>>(_mapper.Map<List<UserOperationClaimDto>>(list));
    }

    public IDataResult<UserOperationClaimDto> GetById(int id)
    {
        var entity = _userOperationClaimDal.Get(u => u.Id == id);
        if (entity == null) return new ErrorDataResult<UserOperationClaimDto>(Messages.UserOperationClaimNotFound);
        return new SuccessDataResult<UserOperationClaimDto>(_mapper.Map<UserOperationClaimDto>(entity));
    }

    public IResult Add(UserOperationClaimDto dto)
    {
        var entity = _mapper.Map<UserOperationClaim>(dto);
        _userOperationClaimDal.Add(entity);
        return new SuccessResult(Messages.UserOperationClaimAdded);
    }

    public IResult Update(UserOperationClaimDto dto)
    {
        var existing = _userOperationClaimDal.Get(u => u.Id == dto.Id);
        if (existing == null) return new ErrorResult(Messages.UserOperationClaimNotFound);
        _mapper.Map(dto, existing);
        _userOperationClaimDal.Update(existing);
        return new SuccessResult(Messages.UserOperationClaimUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _userOperationClaimDal.Get(u => u.Id == id);
        if (existing == null) return new ErrorResult(Messages.UserOperationClaimNotFound);
        _userOperationClaimDal.Delete(existing);
        return new SuccessResult(Messages.UserOperationClaimDeleted);
    }
}
