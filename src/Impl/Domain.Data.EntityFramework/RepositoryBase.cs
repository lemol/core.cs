using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Data;
using Core.Domain.Model;
using System.Linq.Expressions;

namespace Core.Domain.Data.EntityFramework
{
    public abstract class RepositoryBase<TDbContext, TEntity, TIdentity> : IRepository<TEntity, TIdentity>
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
            _dbSet.Update(entity);
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