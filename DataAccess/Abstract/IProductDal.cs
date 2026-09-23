namespace DataAccess.Abstract;

using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;

public interface IProductDal : IEntityRepository<Product>
{
    List<Product> GetListWithDetails(Expression<Func<Product, bool>>? filter = null);
    Product? GetWithDetails(Expression<Func<Product, bool>> filter);
}