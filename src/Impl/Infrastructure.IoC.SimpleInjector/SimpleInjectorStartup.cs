using Core.Infrastructure;
using Core.Infrastructure.IoC.SimpleInjector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System;

namespace Core.Infrastructure.IoC.SimpleInjector
{
    public class SimpleInjectorStartup<TSimpleInjectorContainer> : IStartup
        where TSimpleInjectorContainer : SimpleInjectorContainer, new()
    {
        #region Fields
        protected readonly TSimpleInjectorContainer _wrappedContainer;
        protected readonly Container _container;
        protected readonly IConfigurationRoot _configuration;
        #endregion

        public SimpleInjectorStartup(IConfigurationRoot configuration)
        {
            _wrappedContainer = new TSimpleInjectorContainer();
            _container = _wrappedContainer.InnerContainer;

            _configuration = configuration;
        }

        public virtual void ConfigureServices(Action<IContainer> cfg) => cfg(_wrappedContainer);

        public virtual void ConfigureServices(IServiceCollection services)
        {
            _wrappedContainer.Setup(services ?? new ServiceCollection());
            _container.Verify();
        }

        public virtual void RunConfiguration(IServiceCollection services = null) => ConfigureServices(services);

        public virtual void Run(Action<IContainer> act)
        {
            act(_wrappedContainer);
        }
    }
}
