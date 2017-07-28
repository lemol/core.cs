using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.IoC.SimpleInjector
{
    public abstract class SimpleInjectorContainer : IContainer
    {
        private readonly Container _container;
        public Container InnerContainer
        {
            get { return _container; }
        }
        
        public SimpleInjectorContainer()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        #region IContainer
        public void AddSingleton<T>(T instance) where T : class => _container.RegisterSingleton(instance);
        public void AddTransient<T>(Func<T> f) where T : class => _container.Register<T>(f, Lifestyle.Transient);
        public void AddScoped<T>(Func<T> f) where T : class => _container.Register<T>(f, Lifestyle.Scoped);       
        public void Register<IT, T>() where T : class, IT where IT : class => _container.Register<IT, T>();
        public void Register(Type abstractType, Type concreteType) => _container.Register(abstractType, concreteType);
        public abstract void Setup(IServiceCollection services);
        #endregion
    }
}