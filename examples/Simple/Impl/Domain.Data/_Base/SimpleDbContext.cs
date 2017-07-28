using System;
using Microsoft.EntityFrameworkCore;
using Simple.Domain.Model;

namespace Simple.Domain.Data
{
    public class SimpleDbContext : DbContext
    {
        #region Entities
        public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        #endregion

        #region Constructors
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
            : base(options)
        {
        }
        #endregion
    }
}