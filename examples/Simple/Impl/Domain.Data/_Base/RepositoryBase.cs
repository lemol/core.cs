using System;
using Core.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Domain.Data
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

    public abstract class RepositoryBase<TEntity> : RepositoryBase<DefaultDbContext, TEntity>
        where TEntity : class, IEntity
    {
        public RepositoryBase(DefaultDbContext context)
            : base(context)
        {
        }
    }
}