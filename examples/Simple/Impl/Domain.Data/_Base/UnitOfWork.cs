using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        #region IUnitOfWork
        void Core.Domain.Data.IUnitOfWork.Commit()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}