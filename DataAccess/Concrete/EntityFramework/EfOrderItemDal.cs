namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework; 
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

public class EfOrderItemDal : EfEntityRepositoryBase<OrderItem, ECommerceDbContext>, IOrderItemDal
{
}
