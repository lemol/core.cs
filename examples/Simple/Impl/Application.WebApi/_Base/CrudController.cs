using System;
using Application.Services;

namespace Application.WebApi.Controllers
{
    public class CrudController<TService, TDto, TQuery> : Core.Application.WebApi.Controllers.CrudController<TService, Guid, TDto, TDto, TQuery>
        where TService : ICrudService<TDto, TQuery>
        where TQuery : class
    {
        #region Constructors
        protected CrudController(TService service)
            : base(service)
        {
        }
        #endregion
    }
    public class CrudController<TDto, TQuery> : CrudController<ICrudService<TDto, TQuery>, TDto, TQuery>
        where TQuery : class
    {
        public CrudController(ICrudService<TDto, TQuery> service)
            : base(service)
        {
        }
    }
}