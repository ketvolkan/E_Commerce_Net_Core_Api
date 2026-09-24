using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context=new TContext())
            {
               var addedEntity = context.Entry(entity);
               addedEntity.State = EntityState.Added;
               context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public Core.Utilities.Paging.PagedResult<TEntity> GetPagedList(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var safePageNumber = pageNumber < 1 ? 1 : pageNumber;
                var safePageSize = pageSize < 1 ? 10 : pageSize;

                IQueryable<TEntity> query = context.Set<TEntity>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                var totalCount = query.Count();
                var items = query.Skip((safePageNumber - 1) * safePageSize).Take(safePageSize).ToList();

                return new Core.Utilities.Paging.PagedResult<TEntity>(items, totalCount, safePageNumber, safePageSize);
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
