namespace DataAccess.Abstract;

using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;

using Core.Utilities.Paging;

public interface IProductDal : IEntityRepository<Product>
{
    List<Product> GetListWithDetails(Expression<Func<Product, bool>>? filter = null);
    PagedResult<Product> GetPagedListWithDetails(int pageNumber, int pageSize, Expression<Func<Product, bool>>? filter = null);
    Product? GetWithDetails(Expression<Func<Product, bool>> filter);
}