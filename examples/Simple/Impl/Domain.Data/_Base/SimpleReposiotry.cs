using System;
using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Domain.Data
{
    public abstract class SimpleReposiotry<TDbContext, TEntity> : Core.Domain.Data.EntityFramework.SimpleReposiotry<TDbContext, TEntity, Guid>, IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        public SimpleReposiotry(TDbContext context)
            : base(context)
        {
            
        }
    }

    public class SimpleReposiotry<TEntity> : SimpleReposiotry<DefaultDbContext, TEntity>
        where TEntity : class, IEntity
    {
        public SimpleReposiotry(DefaultDbContext context)
            : base(context)
        {
            
        }
    }
}