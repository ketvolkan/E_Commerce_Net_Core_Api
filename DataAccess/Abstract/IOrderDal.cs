namespace DataAccess.Abstract;

using Core.DataAccess; 
using Entities.Concrete;

public interface IOrderDal : IEntityRepository<Order>
{
}
