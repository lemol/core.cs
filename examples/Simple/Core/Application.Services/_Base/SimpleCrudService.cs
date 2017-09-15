using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Data;
using Domain.Model;

namespace Application.Services
{
    public class SimpleCrudService<TEntity, TEditDto> : Core.Application.Services.SimpleCrudService<TEntity, Guid, TEditDto>
        where TEntity : IEntity
    {
        public SimpleCrudService(
            IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(mapper, unitOfWork, repository, includes)
        {
        }
    }

    public class SimpleCrudService<TEntity, TEditDto, TQuery> : Core.Application.Services.SimpleCrudService<TEntity, Guid, TEditDto, TQuery>
        where TEntity : IEntity
    {
        public SimpleCrudService(
            IApplicationMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(mapper, unitOfWork, repository, includes)
        {
        }
    }
}