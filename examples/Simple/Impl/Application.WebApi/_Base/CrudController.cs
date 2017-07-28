using System;
using Simple.Application.Services;

namespace Application.WebApi.Controllers
{
    public class CrudController<TDto> : Core.Application.WebApi.Controllers.CrudController<Guid, TDto>
    {
        public CrudController(ICrudService<TDto> service)
            : base(service)
        {
        }
    }
}