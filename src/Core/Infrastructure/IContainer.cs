namespace Core.Infrastructure
{
    public interface IContainer
    {
        void AddSingleton<T>(T instance) where T : class;
        void AddTransient<T>(T instance) where T : class;
        void AddScoped<T>(T instance) where T : class;
        void Register<IT, T>() where T : class, IT where IT : class;
        void Setup();
    }
}