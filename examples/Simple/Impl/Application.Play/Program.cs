using System;
using Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Core.Infrastructure.IoC.SimpleInjector;
using Application.Play.InitialSeed;

namespace Application.Play
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Seeder");

            var startup = new SimpleInjectorStartup<DefaultContainer>(null);
            var contextFactory = new DefaultDbContextFactory();

            Console.WriteLine("Configuring dependencies...");
            startup.ConfigureServices(container =>
            {
                container.AddSingleton<DefaultDbContext>(contextFactory.CreateDbContext(new string[]{}));
                container.Register<UtilizadorSeeder, UtilizadorSeeder>();
            });

            startup.RunConfiguration();

            Console.WriteLine("Running seeds...");
            startup.Run(container =>
            {
                var unitOfWork = container.Resolve<IUnitOfWork>();

                container.Resolve<UtilizadorSeeder>().Seed();
                unitOfWork.Commit();
            });

            Console.WriteLine("Done.");
        }
    }
}
