using Domain.Data;
using Application;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using Application.Services.Auditoria;
using Domain.Data.Repositories.Auditoria;

namespace Infrastructure.IoC
{
    public class DefaultContainer : Core.Infrastructure.IoC.SimpleInjector.SimpleInjectorContainer
    {
        public DefaultContainer()
            : base()
        {
        }

        public override void Setup(IServiceCollection services)
        {
            if(services.Any(x => x.ServiceType == typeof(DefaultDbContext)))
            {
                AddScoped<DefaultDbContext>(() =>
                    services
                        .BuildServiceProvider()
                        .GetService<DefaultDbContext>()
                );
            }

            Register<IUnitOfWork, DefaultUnitOfWork>();
            Register<IApplicationMapper, ApplicationMapper>();
            Register(typeof(SimpleCrudService<,>), typeof(SimpleCrudService<,>));
            Register(typeof(IRepository<>), typeof(SimpleReposiotry<>));

            Register<IUtilizadorRepository, UtilizadorRepository>();

            Register<IUtilizadorService, UtilizadorService>();
        }
    }
}