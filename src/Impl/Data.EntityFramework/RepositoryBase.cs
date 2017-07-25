using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lemolsoft.Framework.Domain.Data;
using Lemolsoft.Framework.Domain;
using System.Linq.Expressions;

namespace Lemolsoft.Framework.Data.EntityFramework
{
    public class RepositoryBase<TDbContext, TEntity, TIdentity> : IRepository<TEntity, TIdentity>
        where TEntity : class, IEntity<TIdentity>
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        protected RepositoryBase(TDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            var dbEntityEntry = _db.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
                _dbSet.Add(entity);
        }

        public TEntity Find(TIdentity id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] include)
        {
            return include.Aggregate(_dbSet.AsQueryable(), (acc, act) => acc.Include(act));
        }

        public void Update(TEntity entity)
        {
            var dbEntityEntry = _db.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var dbEntityEntry = _db.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }
    }
}