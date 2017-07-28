namespace Simple.Domain.Data
{
    public class SimpleUnitOfWork : Core.Domain.Data.EntityFramework.UnitOfWork<SimpleDbContext>, IUnitOfWork
    {
        public SimpleUnitOfWork(SimpleDbContext context)
            : base(context)
        {
        }
    }
}