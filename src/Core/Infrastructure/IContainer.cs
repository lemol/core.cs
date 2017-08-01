using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public interface IContainer
    {
        void AddSingleton<T>(T instance) where T : class;
        void AddTransient<T>(Func<T> f) where T : class;
        void AddScoped<T>(Func<T> f) where T : class;
        void Register<IT, T>() where T : class, IT where IT : class;
        void Register(Type abstractType, Type concreteType);
        T Resolve<T>() where T : class;
        void Setup(IServiceCollection services);
    }
}