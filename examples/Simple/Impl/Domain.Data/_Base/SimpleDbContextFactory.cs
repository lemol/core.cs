using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Simple.Domain.Data
{
    public class SimpleDbContextFactory : IDesignTimeDbContextFactory<SimpleDbContext>
    {
        public SimpleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimpleDbContext>();
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\e000130\\lemol\\labs\\files\\simple.db");
            return new SimpleDbContext(optionsBuilder.Options);
        }
    }
}