namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, ECommerceDbContext>, IOperationClaimDal
{
}
