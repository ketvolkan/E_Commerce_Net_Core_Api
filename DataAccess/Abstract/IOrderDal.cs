namespace DataAccess.Abstract;

using Core.DataAccess; 
using Entities.Concrete;

public interface IOrderDal : IEntityRepository<Order>
{
    Order? GetOrderWithDetails(int orderId);
    List<Order> GetOrdersWithDetails(int userId);
}
