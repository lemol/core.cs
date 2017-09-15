using Microsoft.AspNetCore.Mvc;
using Application.WebApi.Controllers;
using CoreWebApi = Core.Application.WebApi.Controllers;
using Application.Services.Auditoria;
using Application.Dto.Auditoria;

namespace Application.WebApi.Auditoria.Controllers
{
    [Route("api/[controller]")]
    public class UtilizadorController : CrudController<IUtilizadorService, UtilizadorDto, UtilizadorQuery>
    {
        public UtilizadorController(IUtilizadorService service)
            : base(service)
        {
        }

        public override CoreWebApi.ApiResult Get([FromQuery] UtilizadorQuery query = null)
        {
            return RunApi(
                () => _service.GetQuery<UtilizadorDto>(query)
            );
        }
    }
}