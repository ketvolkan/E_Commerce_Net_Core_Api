namespace DataAccess.Concrete.EntityFramework;

using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

public class EfProductDal : EfEntityRepositoryBase<Product, ECommerceDbContext>, IProductDal
{

    public List<Product> GetListWithDetails(Expression<Func<Product, bool>>? filter = null)
    {
        using (var context = new ECommerceDbContext())
        {
            IQueryable<Product> query = context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Store);

            return filter == null
                ? query.ToList()
                : query.Where(filter).ToList();
        }
    }

    public Core.Utilities.Paging.PagedResult<Product> GetPagedListWithDetails(int pageNumber, int pageSize, Expression<Func<Product, bool>>? filter = null)
    {
        using (var context = new ECommerceDbContext())
        {
            var safePageNumber = pageNumber < 1 ? 1 : pageNumber;
            var safePageSize = pageSize < 1 ? 10 : pageSize;

            IQueryable<Product> query = context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Store);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = query.Count();
            var items = query.OrderByDescending(p => p.Id).Skip((safePageNumber - 1) * safePageSize).Take(safePageSize).ToList();

            return new Core.Utilities.Paging.PagedResult<Product>(items, totalCount, safePageNumber, safePageSize);
        }
    }

    public Product? GetWithDetails(Expression<Func<Product, bool>> filter)
    {
        using (var context = new ECommerceDbContext())
        {
            return context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages) 
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Store)
                .FirstOrDefault(filter);
        }
    }
}