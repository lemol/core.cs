using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Application.Services;
using System;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.WebApi.Controllers
{
    public class CrudController<TService, TIdentity, TReadDto, TEditDto, TQuery> : Controller
        where TService : ICrudService<TIdentity, TEditDto, TQuery>
        where TQuery : class
    {
        #region Fields
        protected readonly TService _service;
        #endregion

        #region Constructors
        protected CrudController(TService service)
        {
            _service = service;
        }
        #endregion

        protected ApiResult RunApi(Action func) =>
            RunApi<int>(() => { func(); return 0; }, "API Error");
        protected ApiResult RunApi<TResult>(Func<TResult> func) =>
            RunApi<TResult>(func, "API Error");

        protected ApiResult RunApi<TResult>(Func<TResult> func, string errorMessage)
        {
            try
            {
                var result = func();
                return new SuccessResult<TResult>(result);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message, errorMessage);
            }
        }

        #region API
        [HttpGet]
        public virtual ApiResult Get([FromQuery] TQuery query = null)
        {
            try
            {
                var result = _service.GetAll<TReadDto>();
                return new SuccessResult<IEnumerable<TReadDto>>(result);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message, "Erro na pesquisa");
            }
        }

        [HttpGet("{id}")]
        public virtual ApiResult Get(TIdentity id)
        {
            try
            {
                var result = _service.Find<TReadDto>(id);
                return new SuccessResult<TReadDto>(result);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message, "Erro na pesquisa");
            }
        }

        [HttpPost]
        public virtual ApiResult Post([FromBody]TEditDto value)
        {
            try
            {
                var id = _service.Create(value);
                return new SuccessResult<object>(new { Id = id }, "Criado com sucesso");
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message, "Erro na pesquisa");
            }
        }

        [HttpPut("{id}")]
        public virtual ApiResult Put(TIdentity id, [FromBody]TEditDto value)
        {
            try
            {
                _service.Update(id, value);
                var updated = _service.Find<TEditDto>(id);
                return new SuccessResult<TEditDto>(updated, "Actualizado com sucesso");
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message, "Erro na pesquisa");
            }
        }

        [HttpDelete("{id}")]
        public virtual void Delete(TIdentity id)
        {
            _service.Delete(id);
        }
        #endregion

        #region Methods
        protected Guid GetUserId()
        {
            var principal = (ClaimsPrincipal) User;
            var cll = new IdentityOptions().ClaimsIdentity.UserIdClaimType;
            var userId = principal.FindFirstValue(cll);
            // var userId = userManager.GetUserId(User);
            return Guid.Parse(userId);
        }
        #endregion
    }

    public class CrudController<TService, TIdentity, TDto, TQuery> : CrudController<TService, TIdentity, TDto, TDto, TQuery>
        where TService : ICrudService<TIdentity, TDto, TQuery>
        where TQuery : class
    {
        #region Constructors
        protected CrudController(TService service)
            : base(service)
        {
        }
        #endregion
    }

    public class CrudController<TIdentity, TDto, TQuery> : CrudController<ICrudService<TIdentity, TDto, TQuery>, TIdentity, TDto, TDto, TQuery>
        where TQuery : class
    {
        #region Constructors
        protected CrudController(ICrudService<TIdentity, TDto, TQuery> service)
            : base(service)
        {
        }
        #endregion
    }
}