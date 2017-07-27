using System;
using Microsoft.EntityFrameworkCore;
using Simple.Domain.Model;

namespace Simple.Domain.Data
{
    public abstract class SimpleReposiotry<TDbContext, TEntity> : Core.Domain.Data.EntityFramework.SimpleReposiotry<TDbContext, TEntity, Guid>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        public SimpleReposiotry(TDbContext context)
            : base(context)
        {
            
        }
    }

    public class SimpleReposiotry<TEntity> : Core.Domain.Data.EntityFramework.SimpleReposiotry<SimpleDbContext, TEntity, Guid>
        where TEntity : class, IEntity
    {
        public SimpleReposiotry(SimpleDbContext context)
            : base(context)
        {
            
        }
    }
}