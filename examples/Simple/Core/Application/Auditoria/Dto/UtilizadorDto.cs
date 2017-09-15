using System;

namespace Application.Dto.Auditoria
{
    public class UtilizadorDto : EntityDtoBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}