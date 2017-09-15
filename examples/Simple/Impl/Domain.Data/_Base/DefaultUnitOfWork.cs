namespace Domain.Data
{
    public class DefaultUnitOfWork : Core.Domain.Data.EntityFramework.UnitOfWork<DefaultDbContext>, IUnitOfWork
    {
        public DefaultUnitOfWork(DefaultDbContext context)
            : base(context)
        {
        }
    }
}