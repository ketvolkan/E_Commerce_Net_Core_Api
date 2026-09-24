namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework; 
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class EfOrderDal : EfEntityRepositoryBase<Order, ECommerceDbContext>, IOrderDal
{
    public Order? GetOrderWithDetails(int orderId)
    {
        using (var context = new ECommerceDbContext())
        {
            return context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .Include(o => o.SubOrders)
                    .ThenInclude(so => so.Store)
                .Include(o => o.SubOrders)
                    .ThenInclude(so => so.OrderItems)
                        .ThenInclude(oi => oi.ProductVariant)
                .FirstOrDefault(o => o.Id == orderId);
        }
    }

    public List<Order> GetOrdersWithDetails(int userId)
    {
        using (var context = new ECommerceDbContext())
        {
            return context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .Include(o => o.SubOrders)
                    .ThenInclude(so => so.Store)
                .Include(o => o.SubOrders)
                    .ThenInclude(so => so.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedDate)
                .ToList();
        }
    }
}
