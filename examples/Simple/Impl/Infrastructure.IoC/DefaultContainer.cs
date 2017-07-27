using Simple.Domain.Data;

namespace Simple.Infrastructure.IoC
{
    public class DefaultContainer : Core.Infrastructure.IoC.SimpleInjector.DefaultContainer
    {
        public DefaultContainer()
            : base()
        {
        }

        public override void Setup()
        {
            Register<IUnitOfWork, UnitOfWork>();
        }
    }
}