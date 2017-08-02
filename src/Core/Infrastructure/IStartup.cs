using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public interface IStartup
    {
        void ConfigureServices(Action<IContainer> cfg);
        void ConfigureServices(IServiceCollection services);
        void RunConfiguration(IServiceCollection services = null);
        void Run(Action<IContainer> act);
    }
}