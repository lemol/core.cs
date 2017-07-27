using Microsoft.EntityFrameworkCore;
using Simple.Domain.Model;

namespace Simple.Domain.Data
{
    public class SimpleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}