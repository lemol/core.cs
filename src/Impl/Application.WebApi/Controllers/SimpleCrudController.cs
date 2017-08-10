using Core.Application.Services;
using Core.Domain.Model;
using Core.Domain.Data;
using Core.Infrastructure;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Core.Application.WebApi.Controllers
{
    public class SimpleCrudController<TEntity, TIdentity, TDto, TQuery> : CrudController<TIdentity, TDto, TQuery>
        where TEntity : IEntity<TIdentity>
        where TQuery : class
    {
        public SimpleCrudController(
            IMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity, TIdentity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(new SimpleCrudService<TEntity, TIdentity, TDto, TQuery>(mapper, unitOfWork, repository, includes))
        {
        }
    }
}