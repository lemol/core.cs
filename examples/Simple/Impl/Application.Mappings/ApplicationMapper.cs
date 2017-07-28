using IM = Core.Infrastructure.Mappings.AutoMapper;

namespace Simple.Application.Mappings
{
    public class ApplicationMapper : IM.DefaultMapper, IApplicationMapper
    {
        public ApplicationMapper()
            : base(typeof(ApplicationMapper))
        {
        }
    }
}