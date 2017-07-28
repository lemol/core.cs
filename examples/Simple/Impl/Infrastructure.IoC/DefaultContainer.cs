using Simple.Domain.Data;
using Simple.Application;
using Simple.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using Simple.Application.Services;

namespace Simple.Infrastructure.IoC
{
    public class DefaultContainer : Core.Infrastructure.IoC.SimpleInjector.SimpleInjectorContainer
    {
        public DefaultContainer()
            : base()
        {
        }

        public override void Setup(IServiceCollection services)
        {
            AddScoped<SimpleDbContext>(() =>
                services
                    .BuildServiceProvider()
                    .GetService<SimpleDbContext>()
            );
            Register<IUnitOfWork, SimpleUnitOfWork>();
            Register<IApplicationMapper, ApplicationMapper>();
            Register(typeof(IRepository<>), typeof(SimpleReposiotry<>));

            Register<IPersonService, PersonService>();
        }
    }
}