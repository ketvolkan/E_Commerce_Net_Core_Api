namespace Business.Abstract;

using Core.Utilities.Results;
using Entities.Dtos.UserOperationClaims;

public interface IUserOperationClaimService
{
    IDataResult<List<UserOperationClaimDto>> GetAll();
    IDataResult<UserOperationClaimDto> GetById(int id);
    IResult Add(UserOperationClaimDto dto);
    IResult Update(UserOperationClaimDto dto);
    IResult Delete(int id);
}
