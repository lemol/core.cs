using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain.Model;

namespace Core.Domain.Data
{
    public interface IRepository<TEntity, TIdentity>
        where TEntity : IEntity<TIdentity>
    {
        TEntity Find(TIdentity id);
        IQueryable<TEntity> Query();
        [Obsolete]
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] include);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}