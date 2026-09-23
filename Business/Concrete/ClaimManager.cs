namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Entities.Concrete;
using Entities.Dtos.Claims;

public class ClaimManager : IClaimService
{
    private readonly IOperationClaimDal _operationClaimDal;
    private readonly IMapper _mapper;

    public ClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
    {
        _operationClaimDal = operationClaimDal;
        _mapper = mapper;
    }

    public IDataResult<List<OperationClaimDto>> GetAll()
    {
        var list = _operationClaimDal.GetList();
        return new SuccessDataResult<List<OperationClaimDto>>(_mapper.Map<List<OperationClaimDto>>(list));
    }

    public IDataResult<OperationClaimDto> GetById(int id)
    {
        var entity = _operationClaimDal.Get(c => c.Id == id);
        if (entity == null) return new ErrorDataResult<OperationClaimDto>(Messages.ClaimNotFound);
        return new SuccessDataResult<OperationClaimDto>(_mapper.Map<OperationClaimDto>(entity));
    }

    public IResult Add(OperationClaimDto operationClaimDto)
    {
        var entity = _mapper.Map<OperationClaim>(operationClaimDto);
        _operationClaimDal.Add(entity);
        return new SuccessResult(Messages.ClaimAdded);
    }

    public IResult Update(OperationClaimDto operationClaimDto)
    {
        var existing = _operationClaimDal.Get(c => c.Id == operationClaimDto.Id);
        if (existing == null) return new ErrorResult(Messages.ClaimNotFound);
        _mapper.Map(operationClaimDto, existing);
        _operationClaimDal.Update(existing);
        return new SuccessResult(Messages.ClaimUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _operationClaimDal.Get(c => c.Id == id);
        if (existing == null) return new ErrorResult(Messages.ClaimNotFound);
        _operationClaimDal.Delete(existing);
        return new SuccessResult(Messages.ClaimDeleted);
    }
}
