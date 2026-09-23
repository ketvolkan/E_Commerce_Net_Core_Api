namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework; 
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

public class EfProductQuestionDal : EfEntityRepositoryBase<ProductQuestion, ECommerceDbContext>, IProductQuestionDal
{
}
