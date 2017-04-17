using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lemolsoft.Framework.Domain.Data;
using Lemolsoft.Framework.Domain;

namespace Lemolsoft.Framework.Data.EF
{
    public class RepositoryBase<TDbContext, T> : IRepository<T>
        where T : class, IEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        protected readonly DbSet<T> _dbSet;
        protected RepositoryBase(TDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public T Find(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public void Update(T item)
        {
        }
    }
}