using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Domain.Data
{
    public class DefaultDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        public DefaultDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var provider = configuration["Data:DefaultConnection:Provider"];
            var connectionString = configuration["Data:DefaultConnection:ConnectionString"];

            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            switch (provider)
            {
                case "Sqlite":
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                case "PostgreSql":
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                case "SqlServer":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                default:
                    throw new Exception("Erro de provider");
            }
            
            return new DefaultDbContext(optionsBuilder.Options);
        }
    }
}