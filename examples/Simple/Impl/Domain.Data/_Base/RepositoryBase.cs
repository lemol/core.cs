using System;
using Core.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Simple.Domain.Model;

namespace Simple.Domain.Data
{
    public class RepositoryBase<TDbContext, TEntity> : Core.Domain.Data.EntityFramework.RepositoryBase<TDbContext, TEntity, Guid>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        public RepositoryBase(TDbContext context)
            : base(context)
        {
        }
    }

    public abstract class RepositoryBase<TEntity> : Core.Domain.Data.EntityFramework.RepositoryBase<SimpleDbContext, TEntity, Guid>
        where TEntity : class, IEntity
    {
        public RepositoryBase(SimpleDbContext context)
            : base(context)
        {
        }
    }
}