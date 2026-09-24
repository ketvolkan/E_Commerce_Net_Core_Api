namespace DataAccess.Concrete.EntityFramework;

using Core.DataAccess.EntityFramework; 
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

using Microsoft.EntityFrameworkCore;

public class EfCartDal : EfEntityRepositoryBase<Cart, ECommerceDbContext>, ICartDal
{
    public Cart? GetCartWithDetails(int userId)
    {
        using (var context = new ECommerceDbContext())
        {
            return context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                            .ThenInclude(p => p.ProductImages)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Store)
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
}
