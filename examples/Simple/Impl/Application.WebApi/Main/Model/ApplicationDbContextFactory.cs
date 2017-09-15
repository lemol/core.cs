using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Application.WebApi.Main.Model
{
    public class DefaultDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var provider = configuration["Data:DefaultConnection:Provider"];
            var connectionString = configuration["Data:DefaultConnection:ConnectionString"];

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            switch (provider)
            {
                case "Sqlite":
                    builder.UseSqlite(connectionString);
                    break;
                case "PostgreSql":
                    builder.UseNpgsql(connectionString);
                    break;
                case "SqlServer":
                    builder.UseSqlServer(connectionString);
                    break;
                default:
                    throw new Exception("Erro de provider");
            }

            return new ApplicationDbContext(builder.Options);
        }
    }
}