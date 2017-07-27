using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Core.Infrastructure.IoC.SimpleInjector
{
    public abstract class DefaultContainer : IContainer
    {
        private readonly Container _container;
        public Container ContainerWrapped
        {
            get { return _container; }
        }
        
        public DefaultContainer()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        #region IContainer
        public void AddSingleton<T>(T instance) where T : class => _container.RegisterSingleton(instance);
        public void AddTransient<T>(T instance) where T : class => _container.Register<T>(() => instance, Lifestyle.Transient);
        public void AddScoped<T>(T instance) where T : class => _container.Register<T>(() => instance, Lifestyle.Scoped);       
        public void Register<IT, T>() where T : class, IT where IT : class => _container.Register<IT, T>();
        public abstract void Setup();
        #endregion
    }
}