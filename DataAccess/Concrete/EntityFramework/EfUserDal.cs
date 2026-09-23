namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

public class EfUserDal : EfEntityRepositoryBase<User, ECommerceDbContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new ECommerceDbContext())
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };

            return result.ToList();
        }
    }
}
