using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application;
using Domain.Data;
using Domain.Model;

namespace Application.WebApi.Controllers
{
    public class SimpleCrudController<TEntity, TDto, TQuery> : Core.Application.WebApi.Controllers.SimpleCrudController<TEntity, Guid, TDto, TQuery>
        where TEntity : IEntity
        where TQuery : class
    {
        public SimpleCrudController(
            IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(mapper, unitOfWork, repository, includes)
        {
        }
    }

    public class SimpleCrudController<TEntity, TDto> : SimpleCrudController<TEntity, TDto, Core.Application.SimpleQuery>
        where TEntity : IEntity
    {
        public SimpleCrudController(
            IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(mapper, unitOfWork, repository, includes)
        {
        }
    }
}