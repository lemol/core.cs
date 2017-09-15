using IM = Core.Infrastructure.Mappings.AutoMapper;

namespace Application.Mappings
{
    public class ApplicationMapper : IM.DefaultMapper, IApplicationMapper
    {
        public ApplicationMapper()
            : base(typeof(ApplicationMapper))
        {
        }
    }
}