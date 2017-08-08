using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Application.Services;
using System;

namespace Core.Application.WebApi.Controllers
{
    public class CrudController<TService, TIdentity, TReadDto, TEditDto> : Controller
        where TService : ICrudService<TIdentity, TEditDto>
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

        #region API
        [HttpGet]
        public virtual ApiResult Get()
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
                return new SuccessResult<object>(new { Id = id }, "Actualizado com sucesso");
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
    }

    public class CrudController<TService, TIdentity, TDto> : CrudController<TService, TIdentity, TDto, TDto>
        where TService : ICrudService<TIdentity, TDto>
    {
        #region Constructors
        protected CrudController(TService service)
            : base(service)
        {
        }
        #endregion
    }

    public class CrudController<TIdentity, TDto> : CrudController<ICrudService<TIdentity, TDto>, TIdentity, TDto, TDto>
    {
        #region Constructors
        protected CrudController(ICrudService<TIdentity, TDto> service)
            : base(service)
        {
        }
        #endregion
    }
}