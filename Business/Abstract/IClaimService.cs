namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.Claims;

public interface IClaimService
{
    IDataResult<List<OperationClaimDto>> GetAll();
    IDataResult<OperationClaimDto> GetById(int id);
    IResult Add(OperationClaimDto operationClaimDto);
    IResult Update(OperationClaimDto operationClaimDto);
    IResult Delete(int id);
}
