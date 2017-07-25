using System;
using Microsoft.EntityFrameworkCore;
using Lemolsoft.Framework.Domain.Data;

namespace Lemolsoft.Framework.Data.EntityFramework
{
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        public UnitOfWork(TDbContext db)
        {
            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}