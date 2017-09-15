using Core.Infrastructure.Mappings.AutoMapper;
using Application.Dto.Auditoria;
using Domain.Model.Auditoria;

namespace Application.Auditoria.Mappings
{
    public class AuditoriaMappings : AutoMapper.Profile
    {
        public AuditoriaMappings()
        {
            CreateMap<Utilizador, UtilizadorDto>();
            CreateMap<UtilizadorDto, Utilizador>();
        }
    }
}