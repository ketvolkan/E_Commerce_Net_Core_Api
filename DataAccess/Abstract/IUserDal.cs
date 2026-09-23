namespace DataAccess.Abstract;

using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

public interface IUserDal : IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}
