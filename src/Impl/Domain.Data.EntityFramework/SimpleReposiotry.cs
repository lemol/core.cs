using Core.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Data.EntityFramework
{
    public class SimpleReposiotry<TDbContext, TEntity, TIdentity> : RepositoryBase<TDbContext, TEntity, TIdentity>
        where TEntity : class, IEntity<TIdentity>
        where TDbContext : DbContext
    {
        public SimpleReposiotry(TDbContext db)
            : base(db)
        {
        }
    }
}