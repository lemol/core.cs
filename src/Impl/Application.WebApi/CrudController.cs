using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Lemolsoft.Framework.Application.Services;
using System;

namespace Lemolsoft.Framework.Application.WebApi
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
        public virtual IEnumerable<TReadDto> Get()
        {
            return _service.GetAll<TReadDto>();
        }

        [HttpGet("{id}")]
        public virtual TReadDto Get(TIdentity id)
        {
            return _service.Find<TReadDto>(id);
        }

        [HttpPost]
        //[Authorize]
        public virtual void Post([FromBody]TEditDto value)
        {
            _service.Create(value);
        }

        [HttpPut("{id}")]
        public virtual void Put(TIdentity id, [FromBody]TEditDto value)
        {
            _service.Update(id, value);
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
}