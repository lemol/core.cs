using System;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Data;

namespace Core.Domain.Data.EntityFramework
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