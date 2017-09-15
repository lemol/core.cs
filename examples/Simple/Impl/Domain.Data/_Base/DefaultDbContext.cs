using System;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Auditoria;

namespace Domain.Data
{
    public class DefaultDbContext : DbContext
    {
        #region Entities
        public DbSet<Utilizador> Utilizadores { get; set; }
        #endregion

        #region Constructors
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}