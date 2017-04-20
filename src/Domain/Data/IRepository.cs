using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lemolsoft.Framework.Domain.Data
{
    public interface IRepository<TEntity>  where TEntity : IEntity
    {
        TEntity Find(Guid id);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] inculde);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}